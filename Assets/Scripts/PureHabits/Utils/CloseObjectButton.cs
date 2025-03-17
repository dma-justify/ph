using System;
using UI.AbstractViews;
using UnityEngine;

namespace PureHabits.Utils
{
    public class CloseObjectButton : AbstractButtonView
    {
        [SerializeField] private GameObject go;
        
        protected override void Button_OnClick() => go.SetActive(false);
    }
}