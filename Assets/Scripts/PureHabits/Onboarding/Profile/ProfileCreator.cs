using System;
using PureHabits.Data;
using PureHabits.Onboarding.Sequence;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Onboarding.Profile
{
    public class ProfileCreator : AbstractSequenceElement
    {
        [SerializeField] private Button saveButton;
        [SerializeField] private NameInput nameInput;
        [SerializeField] private SpriteSelector avatarSelector;
        [SerializeField] private DataStorage dataStorage;
        [SerializeField] private GameObject window;

        protected override void Awake()
        {
            base.Awake();
            nameInput.NameUpdated += NameInput_OnNameUpdated;
            avatarSelector.Updated += SpriteSelectorOnSpriteUpdated;
            saveButton.onClick.AddListener(SaveButton_OnClick);
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            nameInput.NameUpdated -= NameInput_OnNameUpdated;
            avatarSelector.Updated -= SpriteSelectorOnSpriteUpdated;
            saveButton.onClick.RemoveListener(SaveButton_OnClick);
        }

        public override void ExecuteSequence() => window.SetActive(true);

        protected override void AbandonSequence() => window.SetActive(false);

        private void SaveButton_OnClick()
        {
            dataStorage.CreateProfile(nameInput.Name, avatarSelector.Selected.Id);
            OnExecute();
        }

        private void SpriteSelectorOnSpriteUpdated(SpriteSelectorView sprite)
        {
            saveButton.interactable = sprite != null && !string.IsNullOrEmpty(nameInput.Name);
        }

        private void NameInput_OnNameUpdated(string value)
        {
            saveButton.interactable = avatarSelector.Selected != null && !string.IsNullOrEmpty(value);
        }
    }
}