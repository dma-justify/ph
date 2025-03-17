using System;
using UI.AbstractViews;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Extensions
{
    public class BindableButtonView : AbstractButtonView
    {
        [SerializeField] private Text label;

        private Action _action;

        public void Bind(Action action, string labelText = "")
        {
            if (label != null)
                label.text = labelText;

            _action = action;
        }
        
        protected override void Button_OnClick()
        {
            _action?.Invoke();
        }
    }
}