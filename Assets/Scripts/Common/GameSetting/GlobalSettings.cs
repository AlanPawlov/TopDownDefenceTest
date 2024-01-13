using System;
using UnityEngine;

namespace Common.GameSetting
{
    [Serializable]
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "GameSetting/Global Settings")]
    public class GlobalSettings : ScriptableObject
    {
        public OptionsData DefaultOptions;
    }
}