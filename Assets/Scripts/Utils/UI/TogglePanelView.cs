using System;
using UI.AbstractViews;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TogglePanelView : AbstractToggleView
    {
        public bool IsOn 
        { 
            get => Toggle.isOn;
            set => Toggle.isOn = value;
        }
        
        [SerializeField] private GameObject view;

        public void Refresh()
        {
            Toggle_OnValueChanged(Toggle.isOn);
        }

        public TogglePanelView BindView(GameObject view, ToggleGroup toggleGroup)
        {
            this.view = view;
            
            if (toggleGroup != null) 
                Toggle.group = toggleGroup;
            
            Toggle_OnValueChanged(Toggle.isOn);
            return this;
        }
        
        protected override void Toggle_OnValueChanged(bool isOn)
        {
            view.SetActive(isOn);
        }
    }
}