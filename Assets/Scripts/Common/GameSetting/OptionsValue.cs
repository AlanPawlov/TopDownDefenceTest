using System;

namespace Common.GameSetting
{
    [Serializable]
    public unsafe struct OptionsValue
    {
        public string Key;
        public EOptionsValueType Type;
        public bool BoolValue;
        public OptionsValueFloat FloatValue;
        public OptionsValueInt IntValue;
        public string StringValue;

        public OptionsValue(string key, EOptionsValueType type) : this()
        {
            Key = key;
            Type = type;
        }

        public OptionsValue(string key, bool value) : this()
        {
            Key = key;
            Type = EOptionsValueType.Bool;
            BoolValue = value;
        }

        public OptionsValue(string key, int value) : this()
        {
            Key = key;
            Type = EOptionsValueType.Int;
            IntValue.Value = value;
        }

        public OptionsValue(string key, float value) : this()
        {
            Key = key;
            Type = EOptionsValueType.Float;
            FloatValue.Value = value;
        }

        public OptionsValue(string key, string value) : this()
        {
            Key = key;
            Type = EOptionsValueType.String;
            StringValue = value;
        }

        public bool Equals(OptionsValue other)
        {
            if (Key != other.Key)
                return false;

            if (Type != other.Type)
                return false;

            switch (Type)
            {
                case EOptionsValueType.Bool:
                    return BoolValue == other.BoolValue;
                case EOptionsValueType.Float:
                    return FloatValue.Value == other.FloatValue.Value;
                case EOptionsValueType.Int:
                    return IntValue.Value == other.IntValue.Value;
                case EOptionsValueType.String:
                    return StringValue == other.StringValue;
            }

            return true;
        }
    }
}