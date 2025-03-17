using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Onboarding.Sequence
{
    public abstract class AbstractSequenceElement : MonoBehaviour
    {
        [SerializeField] private AbstractSequenceElement nextElement;
        [SerializeField] private Button executeButton;

        protected virtual void Awake()
        {
            if (executeButton != null)
                executeButton.onClick.AddListener(OnExecute);
        }

        protected virtual void OnDestroy()
        {
            if (executeButton != null)
                executeButton.onClick.RemoveListener(OnExecute);
        }


        protected void OnExecute()
        {
            AbandonSequence();
            
            if (nextElement)
                nextElement.ExecuteSequence();
        }

        public abstract void ExecuteSequence();

        protected abstract void AbandonSequence();
    }
}