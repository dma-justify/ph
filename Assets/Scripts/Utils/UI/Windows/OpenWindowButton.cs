using UI.AbstractViews;
using UnityEngine;

namespace UI.Windows
{
    public class OpenWindowButton : AbstractButtonView
    {
        [SerializeField] private Window window;
        

        protected override void Button_OnClick()
        {
            window.Open();
        }
    }
}