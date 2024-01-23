using System;
using UnityEngine;

namespace CommonTemplate.GameSetting
{
    [Serializable]
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "GameSetting/Global Settings")]
    public class GlobalSettings : ScriptableObject
    {
        public OptionsData DefaultOptions;
    }
}