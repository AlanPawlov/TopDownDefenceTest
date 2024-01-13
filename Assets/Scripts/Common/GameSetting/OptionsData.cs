using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.GameSetting
{
    [Serializable]
    public sealed class OptionsData : IDisposable
    {
        [NonSerialized] private List<OptionsValue> _allValues = new List<OptionsValue>(64);
        [SerializeField] private List<OptionsValue> _values;
        private bool _isInitialized;

        public List<OptionsValue> AllValues => _allValues;
        public List<OptionsValue> Values => _values;

        public OptionsData(List<OptionsValue> valueValues)
        {
            _values = valueValues;
        }

        public void Initialize()
        {
            if (_isInitialized)
                return;

            _allValues.AddRange(_values);
            _isInitialized = true;
        }

        public void AddRuntimeValue(OptionsValue value)
        {
            Assert.IsTrue(_isInitialized);

            if (Contains(value.Key) == true)
                return;

            _allValues.Add(value);
        }

        public bool Contains(string key)
        {
            Assert.IsTrue(_isInitialized);

            return TryGet(key, out OptionsValue value);
        }

        public bool TryGet(string key, out OptionsValue value)
        {
            Assert.IsTrue(_isInitialized);

            value = default;

            for (int i = 0; i < _allValues.Count; i++)
            {
                if (_allValues[i].Key == key)
                {
                    value = _allValues[i];
                    return true;
                }
            }

            return false;
        }

        public void Dispose()
        {
            _isInitialized = default;
            _values = default;
            _allValues = new List<OptionsValue>(64);
        }
    }
}