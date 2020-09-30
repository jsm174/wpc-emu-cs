using System.Diagnostics;
using System.Linq;

namespace WPCEmu.Boards
{
    public class ExternalIo
    {
        const byte EXTERNALIO_MEMORY_SIZE = 32;

        public static class OP
        {
            public const ushort WPC_PARALLEL_STATUS_PORT = 0x3FC0;
            public const ushort WPC_PARALLEL_DATA_PORT = 0x3FC1;
            public const ushort WPC_PARALLEL_STROBE_PORT = 0x3FC2;
            public const ushort WPC_SERIAL_DATA_OUTPUT = 0x3FC3;
            public const ushort WPC_SERIAL_CONTROL_OUTPUT = 0x3FC4;
            public const ushort WPC_SERIAL_BAUD_SELECT = 0x3FC5;
            public const ushort WPC_TICKET_DISPENSE = 0x3FC6;
            //UNUSED
            public const ushort WPC_FLIPTRONICS_FLIPPER_PORT_B = 0x3FD5;
        }

        const byte TICKET_DISPENSE_NOT_AVAILABLE = 0xFF;

        byte[] ram;

        public static ExternalIo GetInstance()
        {
            return new ExternalIo();
        }

        public ExternalIo()
        {
            ram = Enumerable.Repeat((byte)0, EXTERNALIO_MEMORY_SIZE).ToArray();
        }

        public void write(ushort offset, byte value)
        {
            ushort _offset = (ushort) (offset - OP.WPC_PARALLEL_STATUS_PORT);
            ram[_offset] = value;

            switch (offset)
            {
                case OP.WPC_PARALLEL_STATUS_PORT:
                case OP.WPC_PARALLEL_DATA_PORT:
                case OP.WPC_PARALLEL_STROBE_PORT:
                case OP.WPC_SERIAL_DATA_OUTPUT:
                case OP.WPC_SERIAL_CONTROL_OUTPUT:
                case OP.WPC_SERIAL_BAUD_SELECT:
                case OP.WPC_TICKET_DISPENSE:
                case OP.WPC_FLIPTRONICS_FLIPPER_PORT_B:
                    break;

                default:
                    Debug.Print("IO W_NOT_IMPLEMENTED {0} {1}", /*'0x' + */offset/*.toString(16)*/, value);
                    break;
            }
        }

        public byte read(ushort offset)
        {
            ushort _offset = (ushort) (offset - OP.WPC_PARALLEL_STATUS_PORT);

            switch (offset)
            {
                case OP.WPC_TICKET_DISPENSE:
                    return TICKET_DISPENSE_NOT_AVAILABLE;

                case OP.WPC_PARALLEL_STATUS_PORT:
                case OP.WPC_PARALLEL_DATA_PORT:
                case OP.WPC_PARALLEL_STROBE_PORT:
                case OP.WPC_SERIAL_DATA_OUTPUT:
                case OP.WPC_SERIAL_CONTROL_OUTPUT:
                case OP.WPC_SERIAL_BAUD_SELECT:
                case OP.WPC_FLIPTRONICS_FLIPPER_PORT_B:
                    break;

                default:
                    Debug.Print("IO R_NOT_IMPLEMENTED {0}", /*'0x' + */offset/*.toString(16)*/);
                    break;
            }
            return ram[_offset];
        }
    }
}