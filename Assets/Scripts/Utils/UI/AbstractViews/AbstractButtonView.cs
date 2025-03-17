using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AbstractViews
{
    [RequireComponent(typeof(Button))]
    public abstract class AbstractButtonView : MonoBehaviour
    {
        protected Button Button => _button == null ? _button = GetComponent<Button>() : _button;

        private Button _button;
        
        protected virtual void Awake() => Button.onClick.AddListener(Button_OnClick);

        protected virtual void OnDestroy() => Button.onClick.RemoveListener(Button_OnClick);

        protected abstract void Button_OnClick();
    }
}