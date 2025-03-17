using System;
using System.Collections.Generic;
using System.Linq;
using PureHabits.Data;
using PureHabits.Habits.New;
using PureHabits.Utils;
using TMPro;
using UnityEngine;
using Utils.Extended;

namespace PureHabits.Habits.Existed
{
    public class HabitsController : MonoBehaviour
    {
        [SerializeField] private HabitView prefab;
        [SerializeField] private HabitView prefabNegative;
        [SerializeField] private Transform root;
        [SerializeField] private DateTimePreview datePreview;
        [SerializeField] private HabitStatistics statistics;
        [SerializeField] private TMP_Text habitsLabel;
        [SerializeField] private DataStorage dataStorage;
        [SerializeField] private HabitsCreator habitsCreator;

        private List<HabitView> _views;

        private void Awake()
        {
            dataStorage.HabitsLoaded += DataStorage_OnHabitsLoaded;
            dataStorage.HabitAdded += DataStorage_OnHabitAdded;
            dataStorage.HabitUpdated += DataStorage_OnHabitUpdated;
            datePreview.DateChanged += DatePreview_OnChanged;
            dataStorage.HabitDeleted += DataStorage_OnHabitDeleted;
        }

        private void DataStorage_OnHabitUpdated(Habit habit)
        {
            var view = _views.First(v => v.Habit == habit);
            
            if (view.isPositive != habit.Positive)
            {                 
                _views.Remove(view);
                Destroy(view.gameObject);
                _views.Add(CreateView(habit));
            }

            UpdateHabitsByInterval();
        }

        private void OnDestroy()
        {
            dataStorage.HabitsLoaded -= DataStorage_OnHabitsLoaded;
            dataStorage.HabitDeleted -= DataStorage_OnHabitDeleted;
            dataStorage.HabitUpdated -= DataStorage_OnHabitUpdated;
            dataStorage.HabitAdded -= DataStorage_OnHabitAdded;
            datePreview.DateChanged -= DatePreview_OnChanged;
        }

        private void DataStorage_OnHabitDeleted(Habit habit)
        {
            var view = _views.First(v => v.Habit == habit);

            _views.Remove(view);
            
            Destroy(view.gameObject);
        }

        private void DatePreview_OnChanged(DateTime date)
        {
            foreach (HabitView view in _views) 
                view.SetDate(date);

            UpdateHabitsByInterval();
        }

        private void DataStorage_OnHabitAdded(Habit habit)
        {
            _views.Add(CreateView(habit));
            UpdateHabitsByInterval();
        }
        
        private void DataStorage_OnHabitsLoaded(bool success, Habit[] habits)
        {
            _views = new List<HabitView>();
            
            if (!success)
                statistics.SetStatistics(0, 0);
            
            if (!success)
                return;
            
            foreach (Habit habit in habits) 
                _views.Add(CreateView(habit));
            
            UpdateHabitsByInterval();
        }

        private HabitView CreateView(Habit habit)
        {
            return Instantiate(habit.Positive ? prefab : prefabNegative, root).Construct(habit, dataStorage, datePreview.DateTime, Edit);
        }

        private void Edit(Habit habit) => habitsCreator.Edit(habit);

        private void UpdateHabitsByInterval()
        {
            var date = datePreview.DateTime;
            int completed = 0;
            int skipped = 0;
            int uncompleted = 0;

            habitsLabel.text = datePreview.DateTime.Equals(DateTime.Today)
                ? "Your Habits for today"
                : "Your Habits for this day";
             
            foreach (HabitView hv in _views)
            {
                int comp = 0;
                int uncomp = 0;
                int days = (DateTime.Today - hv.Habit.CreateDate).Days;
                if (hv.Habit.MarkDates != null)
                {
                    comp = hv.Habit.MarkDates.Count(m => m.Completed);
                    uncomp = hv.Habit.MarkDates.Count(m => !m.Completed && m.Marked);
                }
                if (hv.Habit.CreateDate.Equals(date))
                {
                    hv.SetActive(true);
                }
                else
                {
                    TimeSpan dif = hv.Habit.CreateDate - date;
                    hv.SetActive(date >= hv.Habit.CreateDate && dif.Days % (hv.Habit.Interval + 1) == 0);
                }
                
                var total = (days / (hv.Habit.Interval + 1)) + 1;
                int skp = total - (comp + uncomp);
                
                completed += comp;
                uncompleted += uncomp;
                skipped += skp;
            }
            
            statistics.SetStatistics(completed, uncompleted, skipped);
        }
        
    }
}