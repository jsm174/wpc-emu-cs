using System.Linq;
using System.Diagnostics;
using WPCEmu.Boards.Static;

/*
Williams part numbers A-14039
128x32 pixels -> total 4096(0x1000) pixel. packed 512(0x200) bytes
NOTE: ALWAYS test STTNG when this file is changed!
In the second generation, the alphanumerics are replaced by a dot matrix controller/display (DMD),
which has 128x32 pixels. The display expects a serial bitstream and must be continously refreshed.
The controller board stores up to 16 frames in its own RAM and handles the refresh.
It connects to the main CPU board which writes the data. The display refreshes at 122Mhz.
The controller fetches 1 byte (8 pixels) every 32 CPU cycles (16 microseconds). At this rate, it takes
256 microseconds per row and a little more than 8 milliseconds per complete frame. Thus, the refresh
rate is about 122MHz.
$3800-$39FF	    DMD Page 1
$3A00-$3BFF	    DMD Page 2
$3FBC-$3FBF	    DMD display control
                Address	  Format	 Description
                $3FBC     Byte     WPC_DMD_HIGH_PAGE
                                    3-0: W: The page of display RAM mapped into the 2nd (6th on WPC95) region,
                                    from 0x3A00-0x3BFF.
                $3FBD     Byte     WPC_DMD_SCANLINE aka DMD_FIRQLINE
                                    7-0: W: Request an FIRQ after a particular scanline is drawn
                                    5-0: R: The last scanline that was drawn
                $3FBE     Byte     WPC_DMD_LOW_PAGE
                                    3-0: W: The page of display RAM mapped into the 1st (5th on WPC95) region,
                                    from 0x3800-0x39FF.
                $3FBF     Byte     WPC_DMD_ACTIVE_PAGE aka DMD_VISIBLEPAGE
                                    3-0: W: The page of display RAM to be used for refreshing the display.
                                    Writes to this register take effect just prior to drawing scanline 0.
*/

namespace WPCEmu.Boards.Elements
{
    public class OutputDmdDisplay
    {
        public struct State
        {
            public byte scanline;
            public byte activepage;
            public byte? nextActivePage;
            public byte[] dmdPageMapping;
            public byte[] dmdShadedBuffer;
            public bool requestFIRQ;
            public byte[] videoRam;
            public byte[] videoOutputBuffer;
            public int videoOutputPointer;
            public int ticksUpdateDmd;
        };

        public struct ExecuteCycleData
        {
            public bool requestFIRQ;
            public byte scanline;
        }

        const byte DMD_WINDOW_HEIGHT = 32;
        const byte DMD_WINDOW_WIDTH_IN_BYTES = (128 / 8);

        const byte DMD_SCANLINE_SIZE_IN_BYTES = 16;
        const byte DMD_SHADING_FRAMES = 3;
        const byte DMD_MAXIMAL_SCANLINE = 0x1F;
        const byte DMD_NR_OF_PAGES = 16;

        ushort dmdPageSize;

        byte[] shadedVideoBuffer;
        byte[] shadedVideoBufferLatched;
        byte[] videoRam;
        byte[] dmdPageMapping;

        byte scanline;
        byte activePage;
        byte? nextActivePage;
        int videoOutputPointer;
        public bool requestFIRQ;
        int ticksUpdateDmd;

        public static OutputDmdDisplay getInstance(ushort dmdPageSize)
        {
            return new OutputDmdDisplay(dmdPageSize);
        }

        public OutputDmdDisplay(ushort dmdPageSize)
        {
            this.dmdPageSize = dmdPageSize;
            shadedVideoBuffer = Enumerable.Repeat((byte)0x00, dmdPageSize * DMD_SHADING_FRAMES).ToArray();
            shadedVideoBufferLatched = Enumerable.Repeat((byte)0x00, dmdPageSize * DMD_SHADING_FRAMES).ToArray();
            videoRam = Enumerable.Repeat((byte)0x00, dmdPageSize * DMD_NR_OF_PAGES).ToArray();

            dmdPageMapping = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            scanline = 0;
            activePage = 0;
            nextActivePage = null;
            videoOutputPointer = 0;
            requestFIRQ = true;
            ticksUpdateDmd = 0;
        }

        public ExecuteCycleData? executeCycle(int singleTicks)
        {
            ticksUpdateDmd += singleTicks;
            if (ticksUpdateDmd >= Timing.CALL_WPC_UPDATE_DISPLAY_AFTER_TICKS)
            {
                ticksUpdateDmd -= Timing.CALL_WPC_UPDATE_DISPLAY_AFTER_TICKS;
                _copyScanline();

                return new ExecuteCycleData
                {
                    requestFIRQ = requestFIRQ,
                    scanline = scanline
                };
            }
            return null;
        }

        public State getState()
        {
            return new State
            {
                scanline = scanline,
                activepage = activePage,
                nextActivePage = nextActivePage,
                dmdPageMapping = dmdPageMapping,
                dmdShadedBuffer = _getShadedOutputVideoFrame(),
                requestFIRQ = requestFIRQ,
                // NOTE: this might flicker, as video ram is not double buffered
                videoRam = videoRam,
                // use cached image, used to dump dmd frames
                videoOutputBuffer = shadedVideoBuffer,
                videoOutputPointer = videoOutputPointer,
                ticksUpdateDmd = ticksUpdateDmd
            };
        }

        public bool? setState(State? _displayState = null)
        {
            if (_displayState == null)
            {
                return false;
            }
            State displayState = (State)_displayState;
            scanline = displayState.scanline;
            activePage = displayState.activepage;
            dmdPageMapping = displayState.dmdPageMapping;
            requestFIRQ = displayState.requestFIRQ == true;
            videoOutputPointer = displayState.videoOutputPointer;
            ticksUpdateDmd = displayState.ticksUpdateDmd;

            setNextActivePage((byte)displayState.nextActivePage);
            //if (typeof displayState.videoRam === 'object')
            //{
                videoRam = displayState.videoRam.Take(displayState.videoRam.Length).ToArray();
            //}
            return null;
        }

        public void selectDmdPage(byte bank, byte value)
        {
            byte page = (byte) (value & 0x0F);
            Debug.Print("_selectDmdPage {0}, {1}", bank, page);
            dmdPageMapping[bank] = page;
        }

        public void setNextActivePage(byte value)
        {
            nextActivePage = (byte) (value & 0xF);
        }

        ushort _getVideoRamOffset(byte bank, ushort offset)
        {
            byte selectedPage = dmdPageMapping[bank];
            return (ushort) (selectedPage * dmdPageSize + offset);
        }

        public void writeVideoRam(byte bank, ushort offset, byte value)
        {
            ushort videoRamOffset = _getVideoRamOffset(bank, offset);
            videoRam[videoRamOffset] = value;
        }

        public byte readVideoRam(byte bank, ushort offset)
        {
            ushort videoRamOffset = _getVideoRamOffset(bank, offset);
            return videoRam[videoRamOffset];
        }

        void _copyScanline()
        {
            // copy a scanline from the activePage to the output video buffer
            var sourceAddress = (ushort) (activePage * dmdPageSize + scanline * DMD_SCANLINE_SIZE_IN_BYTES);
            var destinationAddress = (ushort) (videoOutputPointer * dmdPageSize + scanline * DMD_SCANLINE_SIZE_IN_BYTES);

            for (var i = 0; i < DMD_SCANLINE_SIZE_IN_BYTES; i++)
            {
                shadedVideoBuffer[destinationAddress + i] = videoRam[sourceAddress + i];
            }

            // select next scanline
            scanline = (byte) ((scanline + 1) & DMD_MAXIMAL_SCANLINE);

            // framebuffer switch
            if (scanline == 0)
            {
                // flip output buffer, needed to calculate shading
                videoOutputPointer = (videoOutputPointer + 1) % DMD_SHADING_FRAMES;
                shadedVideoBufferLatched = shadedVideoBuffer.Take(shadedVideoBuffer.Length).ToArray();

                if (nextActivePage != null)
                {
                    activePage = (byte)nextActivePage;
                    nextActivePage = null;
                }
            }
        }

        // a pixel can have 0%/33%/66%/100% Intensity depending on the display time the last 3 frames
        // input: 512 bytes, one pixel uses 1 bit: 0=off, 1=on
        // output: 4096 bytes, one pixel uses 1 byte: 0=off, 1=33%, 2=66%, 3=100%
        byte[] _getShadedOutputVideoFrame()
        {
            ushort OFFSET_BUFFER0 = 0;
            ushort OFFSET_BUFFER1 = dmdPageSize;
            ushort OFFSET_BUFFER2 = (ushort) (dmdPageSize * 2);
            // output is 8 times larger (1 pixel uses 1 byte)
            byte[] videoBufferShaded = Enumerable.Repeat((byte)0x00, 8 * dmdPageSize).ToArray();

            var outputOffset = 0;
            var inputOffset = 0;
            for (var scanline = 0; scanline < DMD_WINDOW_HEIGHT; scanline++)
            {
                for (var eightPixelsOffset = 0; eightPixelsOffset < DMD_WINDOW_WIDTH_IN_BYTES; eightPixelsOffset++)
                {
                    for (var i = 0; i < 8; i++)
                    {
                        var bitMask = Bitmagic.setMsbBit((byte)i);
                        var intensity = (byte) (((shadedVideoBufferLatched[OFFSET_BUFFER0 + inputOffset] & bitMask) > 0 ? 1 : 0) +
                                                 ((shadedVideoBufferLatched[OFFSET_BUFFER1 + inputOffset] & bitMask) > 0 ? 1 : 0) +
                                                 ((shadedVideoBufferLatched[OFFSET_BUFFER2 + inputOffset] & bitMask) > 0 ? 1 : 0));
                        videoBufferShaded[outputOffset++] = intensity;
                    }
                    inputOffset++;
                }
            }
            return videoBufferShaded;
        }
    }
}
