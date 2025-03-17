using System;
using PureHabits.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Utils
{
    public class ProfilePreview : MonoBehaviour
    {
        [SerializeField] private DataStorage dataStorage;

        [SerializeField] private TMP_Text nameLabel;
        [SerializeField] private Image avatarView;
        [SerializeField] private SpriteStorage avatars;

        private void Awake()
        {
            dataStorage.ProfileLoaded += DataStorage_OnProfileLoaded;
            dataStorage.ProfileUpdated += DataStorage_OnProfileUpdated;
        }

        private void OnDestroy()
        {
            dataStorage.ProfileLoaded -= DataStorage_OnProfileLoaded;
            dataStorage.ProfileUpdated -= DataStorage_OnProfileUpdated;
        }

        private void DataStorage_OnProfileUpdated(Profile profile)
        {
            if (nameLabel != null)
                nameLabel.text = profile.Name;
            
            if (avatars != null)
                avatarView.sprite = avatars.GetSprite(profile.AvatarId);
        }

        private void DataStorage_OnProfileLoaded(bool success, Profile profile)
        {
            if (!success)
                return;

            if (nameLabel != null)
                nameLabel.text = profile.Name;
            
            if (avatars != null)
                avatarView.sprite = avatars.GetSprite(profile.AvatarId);
        }
    }
}