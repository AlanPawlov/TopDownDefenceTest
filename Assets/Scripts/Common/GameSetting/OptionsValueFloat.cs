using System;

namespace Common.GameSetting
{
    [Serializable]
    public struct OptionsValueFloat
    {
        public float Value;
        public float MinValue;
        public float MaxValue;
    }
}