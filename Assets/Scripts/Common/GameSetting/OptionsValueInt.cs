using System;

namespace Common.GameSetting
{
    [Serializable]
    public struct OptionsValueInt
    {
        public int Value;
        public int MinValue;
        public int MaxValue;
    }
}