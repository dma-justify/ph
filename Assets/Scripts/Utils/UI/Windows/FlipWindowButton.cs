using UI.AbstractViews;
using UnityEngine;

namespace UI.Windows
{
    public class FlipWindowButton : AbstractButtonView
    {
        [SerializeField] private Window window;
        
        protected override void Button_OnClick()
        {
            window.Flip();
        }
    }
}