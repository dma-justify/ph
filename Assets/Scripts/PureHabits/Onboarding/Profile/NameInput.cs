using System;
using TMPro;
using UnityEngine;

namespace PureHabits.Onboarding.Profile
{
    public class NameInput : MonoBehaviour
    {
        public event Action<string> NameUpdated;
        
        public string Name => inputField.text;
        [SerializeField] private TMP_InputField inputField;


        private void Awake() => inputField.onValueChanged.AddListener(InputField_OnValueChanged);

        private void OnDestroy() => inputField.onValueChanged.RemoveListener(InputField_OnValueChanged);

        public void SetValue(string value) => inputField.text = value;

        private void InputField_OnValueChanged(string value) => NameUpdated?.Invoke(value);
    }
}