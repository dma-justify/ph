using System;

using UnityEngine;

namespace PureHabits.Onboarding.Sequence
{
    public class PanelSequence : AbstractSequenceElement
    {
        [SerializeField] private GameObject panel;
        
        public override void ExecuteSequence() => panel.SetActive(true);
        protected override void AbandonSequence() => panel.SetActive(false);
    }
}