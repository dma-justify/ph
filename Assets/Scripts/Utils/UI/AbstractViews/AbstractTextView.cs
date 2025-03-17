using UnityEngine;
using UnityEngine.UI;

namespace UI.AbstractViews
{
    [RequireComponent(typeof(Text))]
    public abstract class AbstractTextView : MonoBehaviour
    {
        private Text _view;

        private void Awake()
        {
            _view = GetComponent<Text>();
            _view.text = GetText();
        }

        protected abstract string GetText();
    }
}