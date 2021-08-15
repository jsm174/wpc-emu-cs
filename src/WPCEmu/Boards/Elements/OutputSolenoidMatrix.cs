using System;
using System.Linq;
using System.Diagnostics;

/*
All versions of the power driver board support 28 controlled outputs for
solenoids, motors, etc.  These are divided into four groups:
8 high power drivers, 8 low power drivers, 8 flashlamp drivers, and
4 general purpose drivers.  Each bank operates at a different voltage,
somewhere between 20V and 50V.
The CPU board enables/disable a driver by writing a command to the
power driver board.  All values are latched on the driver board and thus
retain their states until the CPU changes them.  The latches are not
readable, so software must maintain the last value written in RAM.
A CPU board reset will assert a blanking signal to reset the latches;
this helps in the event of a software crash.
TODO On WPC-95, 4 additional low voltage outputs running at 5V are added
to the general purpose group, which can be used for miscellaneous I/O
like small DC motors.
$3FE0     Byte     WPC_SOLENOID_GEN_OUTPUT (7-0: W: Enables for solenoids 25-29) or 25-28???
$3FE1     Byte     WPC_SOLENOID_HIGHPOWER_OUTPUT (7-0: W: Enables for solenoids 1-8)
$3FE2     Byte     WPC_SOLENOID_FLASH1_OUTPUT (7-0: W: Enables for solenoids 17-28)
$3FE3     Byte     WPC_SOLENOID_LOWPOWER_OUTPUT (7-0: W: Enables for solenoids 9-16)
*/

namespace WPCEmu.Boards.Elements
{
    public class OutputSolenoidMatrix
    {
        const byte NUMBER_OF_SOLENOIDS = 40;
        const byte ALL_SOLENOID_OFF = 0x00;

        const ushort WPC_SOLENOID_GEN_OUTPUT = 0x3FE0;
        const ushort WPC_SOLENOID_HIGHPOWER_OUTPUT = 0x3FE1;
        const ushort WPC_SOLENOID_FLASH1_OUTPUT = 0x3FE2;
        const ushort WPC_SOLENOID_LOWPOWER_OUTPUT = 0x3FE3;

        const byte OFFSET_SOLENOID_HIGHPOWER = 0;
        const byte OFFSET_SOLENOID_LOWPOWER = 8;
        const byte OFFSET_SOLENOID_FLASHLIGHT = 16;
        const byte OFFSET_SOLENOID_GENERIC = 24;
        const byte OFFSET_SOLENOID_FLIPTRONIC = 32;

        int updateAfterTicks;
        public byte[] solenoidState;
        int ticks;

        public static OutputSolenoidMatrix getInstance(int updateAfterTicks)
        {
            return new OutputSolenoidMatrix(updateAfterTicks);
        }

        public OutputSolenoidMatrix(int updateAfterTicks)
        {
            if (updateAfterTicks == 0)
            {
                throw new Exception("MISSING_UPDATE_AFTER_TICKS");
            }
            this.updateAfterTicks = updateAfterTicks;
            solenoidState = Enumerable.Repeat(ALL_SOLENOID_OFF, NUMBER_OF_SOLENOIDS).ToArray();
            ticks = 0;
        }

        void _updateSolenoidsPacked(byte offset, byte value)
        {
            for (byte i = 0; i < 8; i++)
            {
                if ((value & (1 << i)) != 0)
                {
                    solenoidState[offset + i] = 0xFF;
                }
            }
        }

        public void executeCycle(int ticks)
        {
            this.ticks += ticks;
            // output solenoids state @ 60hz/8 - TODO
            if (this.ticks >= updateAfterTicks)
            {
                Debug.Print("update solenoids state");
                this.ticks -= updateAfterTicks;
                solenoidState = solenoidState.Select((state) => (byte)(state >> 1)).ToArray();
            }
        }

        public void writeFliptronic(byte byteValue)
        {
            Debug.Print("UPDATE_FLIPPER_SOLENOIDS");
            _updateSolenoidsPacked(OFFSET_SOLENOID_FLIPTRONIC, byteValue);
        }

        public void write(ushort sourceAddress, ushort value)
        {
            if (value < 0 || value > 0xFF)
            {
                throw new Exception("SOLENOID_MATRIX_INVALID_VALUE_" + value);
            }
            switch (sourceAddress)
            {
                case WPC_SOLENOID_HIGHPOWER_OUTPUT:
                    _updateSolenoidsPacked(OFFSET_SOLENOID_HIGHPOWER, (byte)value);
                    break;
                case WPC_SOLENOID_LOWPOWER_OUTPUT:
                    _updateSolenoidsPacked(OFFSET_SOLENOID_LOWPOWER, (byte)value);
                    break;
                case WPC_SOLENOID_FLASH1_OUTPUT:
                    _updateSolenoidsPacked(OFFSET_SOLENOID_FLASHLIGHT, (byte)value);
                    break;
                case WPC_SOLENOID_GEN_OUTPUT:
                    _updateSolenoidsPacked(OFFSET_SOLENOID_GENERIC, (byte)value);
                    break;
                default:
                    throw new Exception("SOLENOID_MATRIX_INVALID_OFFSET_0x" + sourceAddress.ToString("X4"));
            }
        }
    }
}
