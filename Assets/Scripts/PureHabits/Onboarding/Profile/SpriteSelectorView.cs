using System;
using PureHabits.Data;
using UI.AbstractViews;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Onboarding.Profile
{
    public class SpriteSelectorView : AbstractToggleView
    {
        [SerializeField] private Image label;
        private SpriteStorage _storage;
        private Action _valueChanged;
        
        public int Id { get; private set; }

        private bool _prev;
        
        public void Construct(int id, ToggleGroup toggleGroup, Action valueChanged, SpriteStorage storage)
        {
            Id = id;
            label.sprite = storage.GetSprite(id);
            Toggle.group = toggleGroup;
            _storage = storage;
            _valueChanged = valueChanged;
            Toggle.isOn = false;
        }

        protected override void Toggle_OnValueChanged(bool isOn) => _valueChanged?.Invoke();
    }
}