using System;
using PureHabits.Data;
using PureHabits.Onboarding.Profile;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Habits.New
{
    public class HabitsCreator : MonoBehaviour
    {
        [SerializeField] private DataStorage dataStorage;
        [SerializeField] private Button saveButton;
        [SerializeField] private SpriteSelector iconSelector;
        [SerializeField] private TMP_InputField descInput;
        [SerializeField] private TMP_InputField intervalInput;
        [SerializeField] private HabitTypeSwitcher typeSwitcher;
        [SerializeField] private NameInput nameInput;
        [SerializeField] private GameObject window;

        [SerializeField] private TMP_Text titleLabel;
        [SerializeField] private GameObject premiumWindow;

        private Habit _habit;
        

        private void Awake()
        {
            saveButton.onClick.AddListener(SaveButton_OnClick);
            iconSelector.Updated += IconSelector_OnUpdated;
            nameInput.NameUpdated += NameInput_OnNameUpdated;
            typeSwitcher.HabitTypeChanged += TypeSwitcher_OnHabitTypeChanged;
            intervalInput.onValueChanged.AddListener(IntervalInput_OnValueChanged);
            
            intervalInput.keyboardType = TouchScreenKeyboardType.PhonePad;
            
        }


        private void OnDestroy()
        {
            saveButton.onClick.RemoveListener(SaveButton_OnClick);
            iconSelector.Updated -= IconSelector_OnUpdated;
            nameInput.NameUpdated -= NameInput_OnNameUpdated;
            typeSwitcher.HabitTypeChanged -= TypeSwitcher_OnHabitTypeChanged;
            intervalInput.onValueChanged.RemoveListener(IntervalInput_OnValueChanged);
        }

        public void CreateNew()
        {
            if (dataStorage.CanCreateHabit)
                ShowCreateWindow();
            else
                ShowPremiumWindow();
        }

        private void ShowPremiumWindow() => premiumWindow.SetActive(true);

        private void ShowCreateWindow()
        {
            window.SetActive(true);
            
            _habit = null;

            titleLabel.text = "Add new habit";
            
            typeSwitcher.ClearSelection();
            descInput.text = string.Empty;
            nameInput.SetValue(string.Empty);
            intervalInput.text = string.Empty;
            iconSelector.Select(-1);
        }

        public void Edit(Habit habit)
        {
            window.SetActive(true);
            
            _habit = habit;

            titleLabel.text = "Edit Habit";

            if (habit.Positive)
                typeSwitcher.SetPositive();
            else
                typeSwitcher.SetNegative();

            descInput.text = habit.Desc;
            nameInput.SetValue(habit.Name);
            iconSelector.Select(habit.IconId);
            intervalInput.text = habit.Interval.ToString();
        }

        private void TypeSwitcher_OnHabitTypeChanged()
        {
            saveButton.interactable = GetValid();
        }

        private void IntervalInput_OnValueChanged(string text)
        {
            saveButton.interactable = GetValid();
        }

        
        private void NameInput_OnNameUpdated(string title)
        {
            saveButton.interactable = GetValid();
        }

        private void IconSelector_OnUpdated(SpriteSelectorView selector)
        {
            saveButton.interactable = GetValid();
        }


        private bool GetValid()
        {
            return !string.IsNullOrEmpty(nameInput.Name) 
                   && !string.IsNullOrEmpty(intervalInput.text) 
                   && typeSwitcher.Selected 
                   && iconSelector.Selected != null;
        }

        private void SaveButton_OnClick()
        {
            var habitName = nameInput.Name;
            var habitDesc = descInput.text;
            var positive = typeSwitcher.Positive;
            var iconId = iconSelector.Selected.Id;
            var interval = int.Parse(intervalInput.text);

            if (_habit == null)
            {
                var habit = new Habit()
                {
                    Name = habitName,
                    Desc = habitDesc,
                    IconId = iconId,
                    Interval = interval,
                    Positive = positive,
                    CreateDate = DateTime.Today,
                };
                
                dataStorage.AddHabit(habit);
            }
            else
            {
                _habit.Name = habitName;
                _habit.Desc = habitDesc;
                _habit.Positive = positive;
                _habit.IconId = iconId;
                _habit.Interval = interval;
                
                Debug.Log("Habit changed!");
                dataStorage.ApplyHabitChange(_habit);
            }

            window.SetActive(false);
        }
    }
}