using System;
using System.Linq;
using PureHabits.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Habits.Existed
{
    public class HabitView : MonoBehaviour
    {
        public Habit Habit => _habit;
        
        [SerializeField] private SpriteStorage icons;
        [SerializeField] private Image iconView;
        [SerializeField] private TMP_Text nameLabel;
        [SerializeField] private TMP_Text descLabel;
        
        [SerializeField] private Button completedButton;
        [SerializeField] private Button uncompletedButton;

        [SerializeField] private Button editButton;
        [SerializeField] private Button deleteButton;

        public bool isPositive;
        
        private readonly Color _fadedColor = new Color(1, 1, 1, .5f);
        
        private Habit _habit;
        private DateTime _date;
        private MarkDate _currentMark;
        private DataStorage _dataStorage;

        private Action<Habit> _editAction;
        
        private void Awake()
        {
            completedButton.onClick.AddListener(SetCompleted);
            uncompletedButton.onClick.AddListener(SetUncompleted);
            
            editButton.onClick.AddListener(Edit);
            deleteButton.onClick.AddListener(Delete);
        }

        private void OnDestroy()
        {
            completedButton.onClick.RemoveListener(SetCompleted);
            uncompletedButton.onClick.RemoveListener(SetUncompleted);
            
            editButton.onClick.RemoveListener(Edit);
            deleteButton.onClick.RemoveListener(Delete);
            
            _dataStorage.HabitUpdated -= DataStorage_OnHabitUpdated;
        }

        public HabitView Construct(Habit habit, DataStorage dataStorage, DateTime dateTime, Action<Habit> editAction)
        {
            _habit = habit;
            _dataStorage = dataStorage;

            _editAction = editAction;
            
            _dataStorage.HabitUpdated += DataStorage_OnHabitUpdated;
            
            SetDate(dateTime);
            
            return this;
        }

        private void DataStorage_OnHabitUpdated(Habit habit)
        {
            if (_habit == habit) 
                SetDate(_date);
        }

        public void SetDate(DateTime dateTime)
        {
            _date = dateTime;
            
            _currentMark = _habit.MarkDates?.FirstOrDefault(m =>
                m.DateTime.DayOfYear == dateTime.DayOfYear && m.DateTime.Year == dateTime.Year);
            
            UpdateView();
        }

        private void Edit()
        {
            _editAction?.Invoke(_habit);
        }

        private void Delete()
        {
            _dataStorage.DeleteHabit(_habit);
        }

        private void UpdateView()
        {
            nameLabel.text = _habit.Name;
            descLabel.text = _habit.Desc;
            iconView.sprite = icons.GetSprite(_habit.IconId);
            
            if (_currentMark == null || !_currentMark.Marked)
            {
                completedButton.targetGraphic.color = _fadedColor;
                uncompletedButton.targetGraphic.color = _fadedColor;
            }
            else
            {
                completedButton.targetGraphic.color = (_currentMark.Completed) ? Color.white : _fadedColor;
                uncompletedButton.targetGraphic.color = (!_currentMark.Completed) ? Color.white : _fadedColor;
            }
            
            bool isToday = DateTime.Today.DayOfYear == _date.DayOfYear && DateTime.Today.Year == _date.Year;
            
            completedButton.interactable = isToday;
            uncompletedButton.interactable = isToday;
        }

        private void SetCompleted()
        {
             _dataStorage.SetHabitCompletion(_habit, true);
        }

        private void SetUncompleted()
        {
            _dataStorage.SetHabitCompletion(_habit, false);
        }
    }
}