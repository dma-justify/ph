using System;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Habits.New
{
    public class HabitTypeSwitcher : MonoBehaviour
    {
        public event Action HabitTypeChanged;
        public bool Selected { get; private set; }
        public bool Positive { get; private set; }
        
        [SerializeField] private Toggle positive;
        [SerializeField] private Toggle negative;

        private void Awake()
        {
            positive.onValueChanged.AddListener(PositiveChanged);
            negative.onValueChanged.AddListener(NegativeChanged);
        }

        private void OnDestroy()
        {
            positive.onValueChanged.RemoveListener(PositiveChanged);
            negative.onValueChanged.RemoveListener(NegativeChanged);
        }

        public void SetPositive()
        {
            positive.isOn = true;
        }

        public void SetNegative()
        {
            negative.isOn = true;
        }

        public void ClearSelection()
        {
            positive.isOn = false;
            negative.isOn = false;
        }

        private void NegativeChanged(bool isOn)
        {
            Selected = positive.isOn || negative.isOn;
            Positive = positive.isOn;
            
            HabitTypeChanged?.Invoke();
        }

        private void PositiveChanged(bool isOn)
        {
            Selected = positive.isOn || negative.isOn;
            Positive = positive.isOn;
            
            HabitTypeChanged?.Invoke();
        }
    }
}