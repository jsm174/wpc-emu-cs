using System;
using System.Linq;
using System.Diagnostics;
using WPCEmu.Boards.Static;

/**
 * Emulates 2x 16-character alphanumeric displays, Each character is comprised of 14 line segments, a comma, and a period.
 * Renders in DMD Display buffer (128 x 32)
 * Segment height is 15 pixel, segment width is 7 pixel:
 * Williams part # D-12793
 */

namespace WPCEmu.Boards.Elements
{
    public class OutputAlphaDisplay
    {
        /*
           ___      SEG_TOP
          |\|/|     SEG_UPPER_LEFT SEG_UPPER_LEFT_DIAGONAL SEG_VERT_TOP SEG_UPPER_RIGHT_DIAGONAL SEG_UPPER_RIGHT
           - -      SEG_MIDDLE_LEFT SEG_MIDDLE_RIGHT
          |/|\|     SEG_LOWER_LEFT SEG_LOWER_LEFT_DIAGONAL SEG_VERT_BOT SEG_LOWER_RIGHT_DIAGONAL SEG_LOWER_RIGHT
           ---      SEG_BOTTOM
          3 3 3 3 3
          3 0 3 0 3
          3 3 3 3 3
          3 3 3 3 3
          3 0 3 0 3
          3 3 3 3 3
          3 0 3 0 3
          3 3 3 3 3
          3 3 3 3 3
          3 0 3 0 3 0 0
          3 3 3 3 3 0 3
        */

        public struct State
        {
            public byte scanline;
            public byte[] dmdShaddedBuffer;
            public byte[] dmdPageMapping;
        };

        const ushort SEG_UPPER_LEFT_DIAGONAL = 0x0001;
        const ushort SEG_VERT_TOP = 0x0002;
        const ushort SEG_UPPER_RIGHT_DIAGONAL = 0x0004;
        const ushort SEG_MIDDLE_RIGHT = 0x0008;
        const ushort SEG_LOWER_RIGHT_DIAGONAL = 0x0010;
        const ushort SEG_VERT_BOT = 0x0020;
        const ushort SEG_LOWER_LEFT_DIAGONAL = 0x0040;
        const ushort SEG_COMMA = 0x0080;
        const ushort SEG_TOP = 0x0100;
        const ushort SEG_UPPER_RIGHT = 0x0200;
        const ushort SEG_LOWER_RIGHT = 0x0400;
        const ushort SEG_BOTTOM = 0x0800;
        const ushort SEG_LOWER_LEFT = 0x1000;
        const ushort SEG_UPPER_LEFT = 0x2000;
        const ushort SEG_MIDDLE_LEFT = 0x4000;
        const ushort SEG_PERIOD = 0x8000;

        const byte CHAR_WITH = 8;
        const byte CHAR_HEIGHT = 11;

        const ushort OFFSET_LINE = 128;
        const ushort OFFSET_BOTTOM = OFFSET_LINE * (CHAR_HEIGHT - 1);
        const ushort OFFSET_MIDDLE = OFFSET_LINE * 5;

        ushort dmdPageSize;
        public byte segmentColumn;
        int ticksUpdateAlpha;

        public ushort[] displayData;
        ushort[] displayDataLatched;

        public static OutputAlphaDisplay GetInstance(ushort dmdPageSize)
        {
            return new OutputAlphaDisplay(dmdPageSize);
        }

        public OutputAlphaDisplay(ushort dmdPageSize)
        {
            this.dmdPageSize = (ushort) (dmdPageSize * 8);
            segmentColumn = 0;
            ticksUpdateAlpha = 0;

            displayData = Enumerable.Repeat((ushort)0x0000, 16*2).ToArray();
            displayDataLatched = Enumerable.Repeat((ushort)0x0000, 16 * 2).ToArray();
        }

        public State getState()
        {
            return new State
            {
                scanline = segmentColumn,
                dmdShaddedBuffer = _getShadedOutputVideoFrame(),
                dmdPageMapping = new byte[] { 0, 0 }
            };
        }

        public void setState(State? displayState)
        {
            if (displayState == null)
            {
                return /*false*/;
            }
            segmentColumn = (byte)(displayState?.scanline);            
        }

        public void setSegmentColumn(byte value)
        {
            segmentColumn = (byte) (value & 0x0F);
            Debug.Print("setSegmentColumn {0}", segmentColumn);
        }

        public void executeCycle(int singleTicks)
        {
            ticksUpdateAlpha += singleTicks;
            if (ticksUpdateAlpha >= Timing.UPDATE_ALPHANUMERIC_DISPLAY_TICKS)
            {
                displayDataLatched = displayData.Take(displayData.Length).ToArray(); 
                Array.Clear(displayData, 0, displayData.Length);
                ticksUpdateAlpha = 0;
            }
        }

        void _update(byte offset, bool isLow, ushort value)
        {
            if (isLow)
            {
                /* Uppermost word of 16-bit reg requires shifting value
                up by 8 bits.  Uppermost 8-bits will be cleared first. */
                displayData[segmentColumn + offset] |= (ushort) ((value << 8) & 0xFF00);
            }
            else
            {
                /* Lower 8-bits need to be cleared first */
                displayData[segmentColumn + offset] |= (ushort) (value & 0xFF);
            }
        }

        public void setRow1(bool isLow, ushort value)
        {
            _update(0, isLow, value);
        }

        public void setRow2(bool isLow, ushort value)
        {
            _update(16, isLow, value);
        }

        void _drawChar(byte[] buffer, ushort offset, ushort value)
        {
            ushort pos;
            if ((value & SEG_TOP) != 0)
            {
                buffer[offset] = 3;
                buffer[offset + 1] = 3;
                buffer[offset + 2] = 3;
                buffer[offset + 3] = 3;
                buffer[offset + 4] = 3;
            }
            if ((value & SEG_BOTTOM) != 0)
            {
                pos = (ushort) (OFFSET_BOTTOM + offset);
                buffer[pos] = 3;
                buffer[pos + 1] = 3;
                buffer[pos + 2] = 3;
                buffer[pos + 3] = 3;
                buffer[pos + 4] = 3;
            }
            if ((value & SEG_PERIOD) != 0)
            {
                buffer[OFFSET_BOTTOM + offset + 6] = 3;
            }
            if ((value & SEG_COMMA) != 0) 
            {
                buffer[OFFSET_LINE + OFFSET_BOTTOM + offset + 6] = 2;
            }
            if ((value & SEG_MIDDLE_LEFT) != 0)
            {
                pos = (ushort) (OFFSET_MIDDLE + offset);
                buffer[pos] = 3;
                buffer[pos + 1] = 3;
                buffer[pos + 2] = 3;
            }
            if ((value & SEG_MIDDLE_RIGHT) != 0)
            {
                pos = (ushort) (OFFSET_MIDDLE + offset + 2);
                buffer[pos] = 3;
                buffer[pos + 1] = 3;
                buffer[pos + 2] = 3;
            }
            if ((value & SEG_VERT_TOP) != 0)
            {
                pos = (ushort) (offset + 2);
                buffer[pos] = 3;
                buffer[pos + 1 * OFFSET_LINE] = 3;
                buffer[pos + 2 * OFFSET_LINE] = 3;
                buffer[pos + 3 * OFFSET_LINE] = 3;
                buffer[pos + 4 * OFFSET_LINE] = 3;
                buffer[pos + 5 * OFFSET_LINE] = 3;
            }
            if ((value & SEG_VERT_BOT) != 0)
            {
                pos = (ushort) (OFFSET_MIDDLE + offset + 2);
                buffer[pos] = 3;
                buffer[pos + 1 * OFFSET_LINE] = 3;
                buffer[pos + 2 * OFFSET_LINE] = 3;
                buffer[pos + 3 * OFFSET_LINE] = 3;
                buffer[pos + 4 * OFFSET_LINE] = 3;
                buffer[pos + 5 * OFFSET_LINE] = 3;
            }
            if ((value & SEG_UPPER_LEFT) != 0)
            {
                buffer[offset] = 3;
                buffer[offset + 1 * OFFSET_LINE] = 3;
                buffer[offset + 2 * OFFSET_LINE] = 3;
                buffer[offset + 3 * OFFSET_LINE] = 3;
                buffer[offset + 4 * OFFSET_LINE] = 3;
                buffer[offset + 5 * OFFSET_LINE] = 3;
            }
            if ((value & SEG_LOWER_LEFT) != 0)
            {
                pos = (ushort) (OFFSET_MIDDLE + offset);
                buffer[pos] = 3;
                buffer[pos + 1 * OFFSET_LINE] = 3;
                buffer[pos + 2 * OFFSET_LINE] = 3;
                buffer[pos + 3 * OFFSET_LINE] = 3;
                buffer[pos + 4 * OFFSET_LINE] = 3;
                buffer[pos + 5 * OFFSET_LINE] = 3;
            }
            if ((value & SEG_UPPER_RIGHT) != 0)
            {
                buffer[4 + offset] = 3;
                buffer[4 + offset + 1 * OFFSET_LINE] = 3;
                buffer[4 + offset + 2 * OFFSET_LINE] = 3;
                buffer[4 + offset + 3 * OFFSET_LINE] = 3;
                buffer[4 + offset + 4 * OFFSET_LINE] = 3;
                buffer[4 + offset + 5 * OFFSET_LINE] = 3;
            }
            if ((value & SEG_LOWER_RIGHT) != 0)
            {
                pos = (ushort) (4 + OFFSET_MIDDLE + offset);
                buffer[pos] = 3;
                buffer[pos + 1 * OFFSET_LINE] = 3;
                buffer[pos + 2 * OFFSET_LINE] = 3;
                buffer[pos + 3 * OFFSET_LINE] = 3;
                buffer[pos + 4 * OFFSET_LINE] = 3;
                buffer[pos + 5 * OFFSET_LINE] = 3;
            }
            if ((value & SEG_UPPER_RIGHT_DIAGONAL) != 0)
            {
                buffer[OFFSET_LINE * 5 + 2 + offset] = 3;
                buffer[OFFSET_LINE * 4 + 2 + offset] = 3;
                buffer[OFFSET_LINE * 3 + 3 + offset] = 3;
                buffer[OFFSET_LINE * 2 + 3 + offset] = 3;
                buffer[OFFSET_LINE * 1 + 4 + offset] = 3;
                buffer[OFFSET_LINE * 0 + 4 + offset] = 3;
            }
            if ((value & SEG_UPPER_LEFT_DIAGONAL) != 0)
            {
                buffer[OFFSET_LINE * 5 + 2 + offset] = 3;
                buffer[OFFSET_LINE * 4 + 2 + offset] = 3;
                buffer[OFFSET_LINE * 3 + 1 + offset] = 3;
                buffer[OFFSET_LINE * 2 + 1 + offset] = 3;
                buffer[OFFSET_LINE * 1 + 0 + offset] = 3;
                buffer[OFFSET_LINE * 0 + 0 + offset] = 3;
            }
            if ((value & SEG_LOWER_RIGHT_DIAGONAL) != 0)
            {
                buffer[OFFSET_LINE * 5 + 2 + offset] = 3;
                buffer[OFFSET_LINE * 6 + 2 + offset] = 3;
                buffer[OFFSET_LINE * 7 + 3 + offset] = 3;
                buffer[OFFSET_LINE * 8 + 3 + offset] = 3;
                buffer[OFFSET_LINE * 9 + 4 + offset] = 3;
                buffer[OFFSET_LINE * 10 + 4 + offset] = 3;
            }
            if ((value & SEG_LOWER_LEFT_DIAGONAL) != 0)
            {
                buffer[OFFSET_LINE * 5 + 2 + offset] = 3;
                buffer[OFFSET_LINE * 6 + 2 + offset] = 3;
                buffer[OFFSET_LINE * 7 + 1 + offset] = 3;
                buffer[OFFSET_LINE * 8 + 1 + offset] = 3;
                buffer[OFFSET_LINE * 9 + 0 + offset] = 3;
                buffer[OFFSET_LINE * 10 + 0 + offset] = 3;
            }
        }

        // a pixel can have 0%/33%/66%/100% Intensity depending on the display time the last 3 frames
        // input: 512 bytes, one pixel uses 1 bit: 0=off, 1=on
        // output: 4096 bytes, one pixel uses 1 byte: 0=off, 1=33%, 2=66%, 3=100%
        byte[] _getShadedOutputVideoFrame()
        {
            byte[] videoBufferShaded = Enumerable.Repeat((byte)0x00, dmdPageSize).ToArray();

            for (int n = 0; n < 16; n++)
            {
                _drawChar(videoBufferShaded, (ushort) (n * CHAR_WITH), displayDataLatched[n]);
            }
            for (int n = 16; n < 32; n++)
            {
                _drawChar(videoBufferShaded, (ushort) (OFFSET_LINE * 16 + n * CHAR_WITH), displayDataLatched[n]);
            }

            return videoBufferShaded;
        }
    }
}