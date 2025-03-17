using UnityEngine;
using UnityEngine.UI;

namespace UI.AbstractViews
{
    [RequireComponent(typeof(Toggle))]
    public abstract class AbstractToggleView : MonoBehaviour
    {
        public Toggle Toggle => _toggle == null ? _toggle = GetComponent<Toggle>() : _toggle;
        
        private Toggle _toggle;

        protected virtual void Awake() => Toggle.onValueChanged.AddListener(Toggle_OnValueChanged);

        protected virtual void OnDestroy() => Toggle.onValueChanged.RemoveListener(Toggle_OnValueChanged);

        protected abstract void Toggle_OnValueChanged(bool isOn);
    }
}