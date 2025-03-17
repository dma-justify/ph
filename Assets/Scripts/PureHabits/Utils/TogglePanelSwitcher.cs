using System;
using UI.AbstractViews;
using UnityEngine;

namespace PureHabits.Utils
{
    public class TogglePanelSwitcher : AbstractToggleView
    {
        [SerializeField] private GameObject panel;

        private void Start() => ForceUpdate();

        public void ForceUpdate() => Toggle_OnValueChanged(Toggle.isOn);
        protected override void Toggle_OnValueChanged(bool isOn) => panel.SetActive(isOn);
    }
}