using PureHabits.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Utils
{
    public class ProfileNameEditor : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private DataStorage dataStorage;
        [SerializeField] private Button saveButton;

        private Profile _profile;

        private void Awake()
        {
            saveButton.onClick.AddListener(Save);
            dataStorage.ProfileLoaded += DataStorageOnProfileLoaded;
        }

        private void DataStorageOnProfileLoaded(bool success, Profile profile)
        {
            if (!success)
                return;
            
            nameInput.text = dataStorage.Profile.Name;
            
            _profile = profile;
        }

        private void OnDestroy()
        {
            saveButton.onClick.RemoveListener(Save);
        }

        private void OnEnable()
        {
            if (_profile != null)
                nameInput.text = dataStorage.Profile.Name;
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(nameInput.text))
                return;
            
            if (dataStorage.Profile.Name == nameInput.text)
                return;
            
            dataStorage.ChangeProfileName(nameInput.text);
        }
    }
}