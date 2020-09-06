using System;
using System.Linq;
using System.Diagnostics;

namespace WPCEmu.Boards.Elements
{
    public class InputSwitchMatrix
    {
        const byte INPUT_ALWAYS_CLOSED = 24;

        const int CABINET_KEY_RELEASE_TIME_MS = 100;
        const byte MATRIX_COLUMN_SIZE = 10;
        const byte ALL_SWITCHES_OFF = 0x00;

        const byte CABINET_COLUMN = 0x00;
        const byte FLIPTRONICS_COLUMN = 0x09;

        byte cabinetKeyState;
        long cabinetKeyAutoreleaseTs;
        public byte[] switchState;
        byte activeColumn;

        public static InputSwitchMatrix GetInstance()
        {
            return new InputSwitchMatrix();
        }

        public InputSwitchMatrix()
        {
            // cabinet input keys (ESCAPE/+/-/ENTER) are wired seperatly
            cabinetKeyState = ALL_SWITCHES_OFF;
            // keys are autoreleased after CABINET_KEY_RELEASE_TIME_MS
            cabinetKeyAutoreleaseTs = 0;
            // row 0 is used for the coin door inputs, so used array start with 1

            switchState = Enumerable.Repeat(ALL_SWITCHES_OFF, MATRIX_COLUMN_SIZE).ToArray();
            setInputKey(INPUT_ALWAYS_CLOSED);
            activeColumn = 0;
        }

        public void setActiveColumn(byte columnBitmask)
        {
            activeColumn = Bitmagic.findMsbBit(columnBitmask);
            Debug.Print("SET ACTIVE_COLUMN {0}", activeColumn);
        }

        public void setCabinetKey(byte keyValue)
        {
            Debug.Print("SET CABINET_KEY {0}", keyValue);
            switchState[CABINET_COLUMN] = keyValue;
            cabinetKeyAutoreleaseTs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public byte getCabinetKey()
        {
            bool cabinetKeyReleased = ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - this.cabinetKeyAutoreleaseTs) > CABINET_KEY_RELEASE_TIME_MS;
            if (cabinetKeyReleased)
            {
                switchState[CABINET_COLUMN] = ALL_SWITCHES_OFF;
            }
            Debug.Print("GET CABINET_KEY {0}", cabinetKeyState);
            return switchState[CABINET_COLUMN];
        }

        /**
         * update Fliptronics switch
         * @param {number} value Valid input numbers range from 11..88
         * @param {boolean} optionalValue if omitted, the switch will be toggled, else set or cleared
         */
        public void setFliptronicsInput(string value, bool? optionalValue = null)
        {
            bool hasValidTypeAndLength = value.Length == 2;

            if (!hasValidTypeAndLength || value[0] != 'F')
            {
                Debug.Print("INVALID_FLIPTRONICS_INPUT_VALUE_{0}" + value);
                return;
            }
            byte column = (byte)((value[1] - '0') - 1);
            Debug.Print("setFliptronicsInput {0} {1}", value, optionalValue);

            if (optionalValue.HasValue && optionalValue.Value)
            {
                Debug.Print("SET_FLIPTRONICS_INPUT_KEY {0}", column);
                switchState[FLIPTRONICS_COLUMN] |= Bitmagic.setMsbBit(column);
            }
            else if (optionalValue.HasValue && !optionalValue.Value)
            {
                Debug.Print("CLEAR_FLIPTRONICS_INPUT_KEY {0}", column);
                switchState[FLIPTRONICS_COLUMN] &= (byte)~(Bitmagic.setMsbBit(column));
            }
            else
            {
                Debug.Print("TOGGLE_FLIPTRONICS_INPUT_KEY {0}", column);
                switchState[FLIPTRONICS_COLUMN] ^= Bitmagic.setMsbBit(column);
            }

        }

        public void setInputKey(byte keyValue, bool? optionalValue = null)
        {
            if (keyValue < 11 || keyValue > 95)
            {
                Debug.Print("INVALID INPUT_KEY {0}", keyValue);
                return;
            }

            byte normalizedKeyValue = (byte)(keyValue - 1);
            byte row = (byte)(normalizedKeyValue / 10); //Number.parseInt(normalizedKeyValue / 10, 10);
            byte column = (byte)(normalizedKeyValue % 10); //Number.parseInt(normalizedKeyValue % 10, 10);

            if (optionalValue.HasValue && optionalValue.Value)
            {
                Debug.Print("SET_INPUT_KEY {0} {1}", row, column);
                switchState[row] |= Bitmagic.setMsbBit(column);
            }
            else if (optionalValue.HasValue && !optionalValue.Value)
            {
                Debug.Print("CLEAR_INPUT_KEY {0} {1}", row, column);
                switchState[row] &= (byte)~(Bitmagic.setMsbBit(column));
            }
            else
            {
                Debug.Print("TOGGLE_INPUT_KEY {0} {1}", row, column);
                switchState[row] ^= Bitmagic.setMsbBit(column);
            }
            // There is ONE switch in all WPC games called "always closed" (always switch 24 on all WPC games).
            switchState[2] |= 0x08;
        }

        byte getActiveRow()
        {
            Debug.Print("GET ACTIVE_ROW_STATE {0}", switchState[activeColumn]);
            return switchState[activeColumn];
        }

        public byte getRow(byte number)
        {
            Debug.Print("GET ROW_STATE {0}", switchState[number]);
            return switchState[number];
        }

        public byte getFliptronicsKeys()
        {
            return (byte) ((~switchState[FLIPTRONICS_COLUMN]) & 0xFF);
        }

        void clearInputKeys()
        {
            cabinetKeyState = ALL_SWITCHES_OFF;
            switchState = Enumerable.Repeat(ALL_SWITCHES_OFF, MATRIX_COLUMN_SIZE).ToArray();
        }
    }
}
