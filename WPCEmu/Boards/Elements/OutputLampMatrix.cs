using System.Linq;
using System.Diagnostics;

namespace WPCEmu.Boards.Elements
{
    public class OutputLampMatrix
    {
        const byte MATRIX_COLUMN_SIZE = 64;
        const byte ALL_LAMPS_OFF = 0x00;

        int updateAfterTicks;
        public byte[] lampState;
        public byte activeRow;
        public byte activeColumn;
        int ticks;

        public static OutputLampMatrix getInstance(int updateLampTicks)
        {
            return new OutputLampMatrix(updateLampTicks);
        }

        public OutputLampMatrix(int updateAfterTicks)
        {
            this.updateAfterTicks = updateAfterTicks;
            lampState = Enumerable.Repeat(ALL_LAMPS_OFF, MATRIX_COLUMN_SIZE).ToArray();
            activeRow = 0;
            activeColumn = 0;
            ticks = 0;
        }

        void _updateLampState()
        {
            if (activeColumn == 0)
            {
                return;
            }

            for (byte i = 0; i < 8; i++)
            {
                if ((activeRow & (1 << i)) != 0)
                {
                    for (byte j = 0; j < 8; j++)
                    {
                        if ((activeColumn & (1 << j)) != 0)
                        {
                            lampState[(j << 3) | i] |= 0xFF;
                        }
                    }
                }
            }
        }

        public void executeCycle(int ticks)
        {
            this.ticks += ticks;
            if (this.ticks >= updateAfterTicks)
            {
                Debug.Print("update lamp state");
                this.ticks -= updateAfterTicks;
                lampState = lampState.Select((state) =>
                {
                    if (state > 7)
                    {
                        return (byte) (state - 8);
                    }
                    return (byte) 0;
                }).ToArray();
            }
        }

        public void setActiveRow(byte activeRow)
        {
            this.activeRow = activeRow;
            Debug.Print("SET ACTIVE_ROW {0}", this.activeRow);
            _updateLampState();
        }

        public void setActiveColumn(byte activeColumn)
        {
            this.activeColumn = activeColumn;
            Debug.Print("SET ACTIVE_COLUMN {0}", this.activeColumn);
            _updateLampState();
        }
    }
}
