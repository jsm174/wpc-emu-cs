using System.Collections.Generic;
using System.Diagnostics;
using WPCEmu.Boards.Elements;
using dmdMapper = WPCEmu.Boards.Mapper.Dmd;

namespace WPCEmu.Boards
{
    public class DisplayBoard
    {
        public static class OP
        {
            public struct State
            {
                public byte volume;
                public int readDataBytes;
                public int writeDataBytes;
                public int readControlBytes;
                public int writeControlBytes;
            };

            //TODO move me away here
            public const ushort FREEWPC_DEBUG_CONTROL_PORT = 0x3D61;
            public const ushort WPC_SERIAL_CONTROL_PORT = 0x3E66;
            public const ushort WPC95_PRINTER_DATA = 0x3FB0;
            public const ushort WPC95_PRINTER_BAUD = 0x3FB1;
            public const ushort WPC95_PRINTER_ADDR = 0x3FB3;
            public const ushort WPC95_PRINTER_STATUS = 0x3FB5;
            public const ushort WPC95_PRINTER_PRESENCE = 0x3FB7;

            public const ushort WPC95_DMD_PAGE3000 = 0x3FB9;
            public const ushort WPC95_DMD_PAGE3200 = 0x3FB8;
            public const ushort WPC95_DMD_PAGE3400 = 0x3FBB;
            public const ushort WPC95_DMD_PAGE3600 = 0x3FBA;

            //AKA WPC95_DMD_PAGE3A00
            public const ushort WPC_DMD_HIGH_PAGE = 0x3FBC;

            //AKA DMD_FIRQLINE
            public const ushort WPC_DMD_SCANLINE = 0x3FBD;

            //AKA WPC95_DMD_PAGE3800
            public const ushort WPC_DMD_LOW_PAGE = 0x3FBE;
            public const ushort WPC_DMD_ACTIVE_PAGE = 0x3FBF;

            public const ushort WPC_ALPHA_POS = 0x3FEB;
            public const ushort WPC_ALPHA_ROW1_A = 0x3FEC;
            public const ushort WPC_ALPHA_ROW1_B = 0x3FED;
            public const ushort WPC_ALPHA_ROW2_A = 0x3FEE;
            public const ushort WPC_ALPHA_ROW2_B = 0x3FEF;
        }

        // 128 * 32
        ushort DMD_PAGE_SIZE = 0x200;

        Dictionary<ushort, string> REVERSEOP = new Dictionary<ushort, string>();

        InterruptCallbackData interruptCallback;
        public byte[] ram;
        bool hasAlphanumericDisplay;
        OutputDmdDisplay outputDmdDisplay;
        OutputAlphaDisplay outputAlphaDisplay;

        public static DisplayBoard getInstance(WpcCpuBoard.InitObject initObject)
        {
            return new DisplayBoard(initObject);
        }

        public DisplayBoard(WpcCpuBoard.InitObject initObject)
        {
            foreach (var fieldInfo in typeof(OP).GetFields())
            {
                object value = fieldInfo.GetValue(null);

                if (value is ushort)
                {
                    if (!REVERSEOP.ContainsKey((ushort)value))
                    {
                        REVERSEOP.Add((ushort)value, fieldInfo.Name);
                    }
                }
            }

            interruptCallback = initObject.interruptCallback;
            ram = initObject.ram;
            hasAlphanumericDisplay = initObject.hasAlphanumericDisplay;

            outputDmdDisplay = OutputDmdDisplay.getInstance(DMD_PAGE_SIZE);
            outputAlphaDisplay = OutputAlphaDisplay.getInstance(DMD_PAGE_SIZE);
        }

        public void reset()
        {
            Debug.Print("RESET_DISPLAY_BOARD");
        }

        public object getState()
        {
            if (hasAlphanumericDisplay)
            {
                return outputAlphaDisplay.getState();
            }
            return outputDmdDisplay.getState();
        }

        public bool? setState(object displayState)
        {
            if (hasAlphanumericDisplay)
            {
                return outputAlphaDisplay.setState((OutputAlphaDisplay.State?)displayState);
            }
            return outputDmdDisplay.setState((OutputDmdDisplay.State?)displayState);
        }

        public void executeCycle(int singleTicks)
        {
            if (hasAlphanumericDisplay)
            {
                outputAlphaDisplay.executeCycle(singleTicks);
                return;
            }

            var dmdState = outputDmdDisplay.executeCycle(singleTicks);
            // NOTE: if ram[OP.WPC_DMD_SCANLINE] > 0x1F then NO FIRQ call is made. scanline is never bigger than 0x1F.
            if (dmdState != null && dmdState?.requestFIRQ != null && dmdState?.scanline == ram[OP.WPC_DMD_SCANLINE])
            {
                interruptCallback.firqFromDmd();
                outputDmdDisplay.requestFIRQ = false;
            }
        }

        public void write(ushort offset, byte value)
        {
            var address = dmdMapper.getAddress(offset);
            if (address.subsystem == dmdMapper.SUBSYSTEM_DMD_VIDEORAM)
            {
                outputDmdDisplay.writeVideoRam((byte) address.bank, address.offset, value);
                return;
            }

            this.ram[offset] = value;
            switch (offset)
            {
                case OP.WPC95_PRINTER_DATA:
                case OP.WPC95_PRINTER_BAUD:
                case OP.WPC95_PRINTER_ADDR:
                case OP.WPC95_PRINTER_STATUS:
                case OP.WPC95_PRINTER_PRESENCE:
                    Debug.Print("WRITE_IGNORED {0} {1}", REVERSEOP[offset], value);
                    break;

                case OP.WPC_DMD_SCANLINE:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    // The CPU can also write to WPC_DMD_SCANLINE to request an FIRQ interrupt to be generated when the current scanline reaches a certain value.
                    // This is used to implement shading: the active page buffer is rapidly changed between different bit planes at different frequencies to simulate color.
                    // Because there is latency between the time that FIRQ is generated and the CPU can respond to it, this writable register can compensate for that delay
                    // and help to ensure that flipping occurs as fast as possible.
                    outputDmdDisplay.requestFIRQ = true;
                    break;

                case OP.WPC_DMD_ACTIVE_PAGE:
                    Debug.Print("WRITE {0} {1}", REVERSEOP[offset], value);
                    // The visible page register, WPC_DMD_ACTIVE_PAGE, holds the page number of the frame that should
                    // be clocked to the display. Writing to this register does not take effect immediately but instead
                    // at the beginning of the next vertical retrace.
                    outputDmdDisplay.setNextActivePage(value);
                    break;

                //which page is exposed at address 0x3800?
                case OP.WPC_DMD_LOW_PAGE:
                    // AKA PAGE3800
                    outputDmdDisplay.selectDmdPage(0, value);
                    break;
                case OP.WPC_DMD_HIGH_PAGE:
                    // AKA PAGE3A00
                    outputDmdDisplay.selectDmdPage(1, value);
                    break;
                case OP.WPC95_DMD_PAGE3000:
                    outputDmdDisplay.selectDmdPage(2, value);
                    break;
                case OP.WPC95_DMD_PAGE3200:
                    outputDmdDisplay.selectDmdPage(3, value);
                    break;
                case OP.WPC95_DMD_PAGE3400:
                    outputDmdDisplay.selectDmdPage(4, value);
                    break;
                case OP.WPC95_DMD_PAGE3600:
                    outputDmdDisplay.selectDmdPage(5, value);
                    break;

                case OP.WPC_ALPHA_POS:
                    this.outputAlphaDisplay.setSegmentColumn(value);
                    break;
                case OP.WPC_ALPHA_ROW1_A:
                    this.outputAlphaDisplay.setRow1(true, value);
                    break;
                case OP.WPC_ALPHA_ROW1_B:
                    this.outputAlphaDisplay.setRow1(false, value);
                    break;
                case OP.WPC_ALPHA_ROW2_A:
                    this.outputAlphaDisplay.setRow2(true, value);
                    break;
                case OP.WPC_ALPHA_ROW2_B:
                    this.outputAlphaDisplay.setRow2(false, value);
                    break;

                default:
                    Debug.Print("W_NOT_IMPLEMENTED {0} {1}", "0x" + offset.ToString("X4"), value);
                    Debug.Print("DMD W_NOT_IMPLEMENTED {0} {1}", "0x" + offset.ToString("X4"), value);
                    break;
            }
        }

        public byte read(ushort offset)
        {
            var address = dmdMapper.getAddress(offset);
            if (address.subsystem == dmdMapper.SUBSYSTEM_DMD_VIDEORAM)
            {
                return this.outputDmdDisplay.readVideoRam((byte)address.bank, address.offset);
            }

            switch (offset)
            {
                case OP.WPC95_PRINTER_DATA:
                case OP.WPC95_PRINTER_BAUD:
                case OP.WPC95_PRINTER_ADDR:
                case OP.WPC95_PRINTER_STATUS:
                case OP.WPC95_PRINTER_PRESENCE:
                case OP.FREEWPC_DEBUG_CONTROL_PORT:
                case OP.WPC_SERIAL_CONTROL_PORT:
                    Debug.Print("READ {0} {1}", REVERSEOP[offset], 0);
                    return 0x0;

                case OP.WPC_ALPHA_POS:
                case OP.WPC_ALPHA_ROW1_A:
                case OP.WPC_ALPHA_ROW1_B:
                case OP.WPC_ALPHA_ROW2_A:
                case OP.WPC_ALPHA_ROW2_B:
                    Debug.Print("READ {0}", REVERSEOP[offset]);
                    return ram[offset];

                case OP.WPC_DMD_SCANLINE:
                    Debug.Print("READ {0} {1}", REVERSEOP[offset], /*this.scanline*/0);
                    return this.ram[offset];

                default:
                    Debug.Print("R_NOT_IMPLEMENTED {0}", "0x" + offset.ToString("X4"));
                    Debug.Print("DMD R_NOT_IMPLEMENTED {0}", "0x" + offset.ToString("X4"));
                    return 0x0;
            }
        }
    }
}