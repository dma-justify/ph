using UI.Windows;
using UnityEngine;

namespace UI.AbstractViews
{
    public abstract class AbstractWindowButton : AbstractButtonView
    {
        [SerializeField] private Window window;
        
        protected override void Button_OnClick() => DoAction(window);

        protected abstract void DoAction(Window window);
    }
}