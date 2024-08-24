using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ratferences {
    public class ValueReference : ScriptableObject {
        public delegate void OnValueChangedSignal();
        public OnValueChangedSignal ValueChangedSignal;
    }

    public class ValueReference<T> : ValueReference {

        public delegate void OnValueChanged(T newValue);

        public OnValueChanged ValueChanged;

        [SerializeField]
        private T _value;

        public T Value {
            get {
                return _value;
            }
            set {
                SetValue(value);
            }
        }

        /// <summary>
        /// Identical to using the property.
        /// </summary>
        /// <param name="newValue">The new value</param>
        public void SetValue(T newValue) {
#if UNITY_EDITOR
            _cachedValue = newValue;
#endif
            _value = newValue;
            ValueChanged?.Invoke(Value);
            ValueChangedSignal?.Invoke();
        }

#if UNITY_EDITOR
        T _cachedValue;
        private void Awake() {
            _cachedValue = _value;
        }

        private void OnValidate() {
            if (Application.isPlaying && !EqualityComparer<T>.Default.Equals(_cachedValue, _value)) {
                SetValue(_value);
            }
        }
#endif
    }
}