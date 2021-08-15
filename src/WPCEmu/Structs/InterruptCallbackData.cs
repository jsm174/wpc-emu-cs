using System;

namespace WPCEmu
{
    public struct InterruptCallbackData
    {
        public Action irq;
        public Action firqFromDmd;
        public Action reset;
    }
}