using UI.AbstractViews;
using UnityEngine;

namespace PureHabits.Utils
{
    public class OpenUrlButton : AbstractButtonView
    {
        [SerializeField] private string url;
        protected override void Button_OnClick() => Application.OpenURL(url);
    }
}