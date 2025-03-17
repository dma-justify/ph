using System;
using System.Collections.Generic;
using System.Linq;
using Bitsplash.DatePicker;
using PureHabits.Data;
using PureHabits.Habits.Existed;
using UnityEngine;
using Utils.Extended;

namespace PureHabits.Calendar
{
    public class CalendarController : MonoBehaviour
    {
        [SerializeField] private DatePickerSettings datePickerSettings;
        [SerializeField] private HabitCalendarView prefab;
        [SerializeField] private Transform root;
        [SerializeField] private DataStorage dataStorage;

        [SerializeField] private HabitStatistics statistics;

        private List<HabitCalendarView> _views;
        
        private void Awake()
        {
            dataStorage.HabitAdded += DataStorage_OnHabitAdded;
            dataStorage.HabitsLoaded += DataStorage_OnHabitsLoaded;
            dataStorage.HabitUpdated += DataStorage_OnHabitUpdated;
            dataStorage.HabitDeleted += DataStorage_OnHabitDeleted;
            
            datePickerSettings.Content.OnSelectionChanged.AddListener(DatePickerSettings_OnSelectionChanged);
            datePickerSettings.Content.OnDisplayChanged.AddListener(DatePickerSettings_OnDisplayChanged);
            
            datePickerSettings.Content.Selection.SelectOne(DateTime.Today);
            
        }

        private void OnEnable()
        {
            root.parent.RebuildGraphic();
        }

        private void OnDestroy()
        {
            datePickerSettings.Content.OnSelectionChanged.RemoveListener(DatePickerSettings_OnSelectionChanged);
            datePickerSettings.Content.OnDisplayChanged.RemoveListener(DatePickerSettings_OnDisplayChanged);
            
            dataStorage.HabitAdded -= DataStorage_OnHabitAdded;
            dataStorage.HabitsLoaded -= DataStorage_OnHabitsLoaded;
            dataStorage.HabitUpdated -= DataStorage_OnHabitUpdated;
            dataStorage.HabitDeleted -= DataStorage_OnHabitDeleted;
        }

        private void DataStorage_OnHabitUpdated(Habit habit)
        {
            _views.First(hv => hv.Habit == habit).Configure(habit);
            
            var date = datePickerSettings.Content.Selection.GetItem(0);
            UpdateViews(date);
        }

        private void DataStorage_OnHabitDeleted(Habit habit)
        {
           var view = _views.First(hv => hv.Habit == habit);

           _views.Remove(view);
           Destroy(view.gameObject);
           
           var date = datePickerSettings.Content.Selection.GetItem(0);
           UpdateViews(date);
        }
        
        private void DataStorage_OnHabitAdded(Habit habit)
        {
            _views.Add(CreateView(habit));
            
            var date = datePickerSettings.Content.Selection.GetItem(0);
            UpdateViews(date);
        }

        private void DataStorage_OnHabitsLoaded(bool success, Habit[] habits)
        {
            _views = new List<HabitCalendarView>();
            
            var date = DateTime.Today;
            datePickerSettings.Content.Selection.SelectOne(date);

            if (!success)
                statistics.SetStatistics(0, 0);
            
            if (!success)
                return;
            
            foreach (Habit habit in habits)
            {
                _views.Add(CreateView(habit));
            }
            
            UpdateViews(date);
        }

        private void DatePickerSettings_OnDisplayChanged()
        {
            var date = datePickerSettings.Content.Selection.GetItem(0);
            
            UpdateViews(date);
        }

        private void DatePickerSettings_OnSelectionChanged()
        {
            var date = datePickerSettings.Content.Selection.GetItem(0);

            UpdateViews(date);
        }

        private void UpdateViews(DateTime date)
        {
            int completed = 0;
            int uncompleted = 0;
            
            if (_views == null)
                return;
            
            foreach (HabitCalendarView hv in _views)
            {
                if (hv.Habit.CreateDate.Equals(date))
                {
                    hv.SetActive(true);

                    var any = hv.Habit.MarkDates?.FirstOrDefault(m =>
                        m.DateTime.DayOfYear == date.DayOfYear && m.DateTime.Year == date.Year);

                    if (any == null) 
                        continue;
                    
                    if (any.Completed)
                        completed++;
                    else
                        uncompleted++;
                }
                else
                {
                    TimeSpan dif = hv.Habit.CreateDate - date;
                    hv.SetActive(date >= hv.Habit.CreateDate && dif.Days % (hv.Habit.Interval + 1) == 0);
                    
                    var any = hv.Habit.MarkDates?.FirstOrDefault(m =>
                        m.DateTime.DayOfYear == date.DayOfYear && m.DateTime.Year == date.Year);

                    if (any == null)
                        continue;
                    
                    if (any.Completed)
                        completed++;
                    else
                        uncompleted++;
                }
            }
            
            statistics.SetStatistics(completed, uncompleted);
            
            root.parent.RebuildGraphic();
        }

        private HabitCalendarView CreateView(Habit habit)
        {
            return Instantiate(prefab, root).Configure(habit);
        }
    }
}