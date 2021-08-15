using System.Linq;
using System.Diagnostics;

namespace WPCEmu.Boards.Elements
{
    public class OutputGeneralIllumination
    {
        // general illumination supports up to 5 lamp groups, Coin door enable relay (Bit 5), Flipper enable relay (Bit 7)
        // TODO: brightness level 7+8 do not work, no IRQ count for those entries
        const byte MATRIX_COLUMN_SIZE = 8;
        const byte ALL_LAMPS_OFF = 0x00;
        const byte WPC95_TRIAC_MASK = 0xE7;
        const byte COIN_DOOR_ENABLE_BIT = 5;
        const byte UNKNOWN_BIT = 6;
        const byte FLIPPER_RELAY_BIT = 7;

        public byte[] generalIlluminationState;
        bool isWpc95;

        public static OutputGeneralIllumination getInstance(bool isWpc95)
        {
            return new OutputGeneralIllumination(isWpc95);
        }

        public OutputGeneralIllumination(bool isWpc95)
        {
            generalIlluminationState = Enumerable.Repeat(ALL_LAMPS_OFF, MATRIX_COLUMN_SIZE).ToArray();
            this.isWpc95 = isWpc95;
        }

        public void update(byte value, int irqCountGI = 0)
        {
            // WPC-95 only controls 3 of the 5 Triacs, the other 2 are ALWAYS ON, power wired directly
            byte normalizedValue = isWpc95 ? (byte) ((value & WPC95_TRIAC_MASK) | 0x18) : value;
            Debug.Print("UPDATE_TRIAC {0} {1}", normalizedValue, irqCountGI);

            for (byte i = 0; i < 5; i++)
            {
                if ((normalizedValue & (1 << i)) != 0)
                {
                    if (generalIlluminationState[i] < 7)
                    {
                        generalIlluminationState[i] = (byte) (7 - irqCountGI);
                    }
                    else
                    {
                        generalIlluminationState[i] = 7;
                    }
                }
                else if (generalIlluminationState[i] != 0)
                {
                    this.generalIlluminationState[i]--;
                }
            }

            generalIlluminationState[COIN_DOOR_ENABLE_BIT] = (byte) (((normalizedValue & (1 << COIN_DOOR_ENABLE_BIT)) != 0) ? 7 : 0);
            generalIlluminationState[UNKNOWN_BIT] = (byte) (((normalizedValue & (1 << UNKNOWN_BIT)) != 0) ? 7 : 0);
            generalIlluminationState[FLIPPER_RELAY_BIT] = (byte) (((normalizedValue & (1 << FLIPPER_RELAY_BIT)) != 0) ? 7 : 0);
        }

        public byte[] getNormalizedState()
        {
            return generalIlluminationState.Select(value => (byte) (value * 31)).ToArray();
        }

        public byte[] getUint8ArrayFromState(byte[] giState)
        {
            byte[] normalizedGiState = giState.Select((value) => (byte) (value / 31)).ToArray();
            return normalizedGiState;
        }
    }
}
