using System;
using System.Collections.Generic;
using System.Linq;
using PureHabits.Data;
using UnityEngine;

namespace PureHabits.Developed
{
    public class DevelopedHabitsController : MonoBehaviour
    {
        [SerializeField] private DataStorage dataStorage;
        [SerializeField] private DevelopedHabitView prefab;
        [SerializeField] private Transform root;
        
        private List<DevelopedHabitView> _views;
        

        private void Awake()
        {
            dataStorage.HabitsLoaded += DataStorage_OnHabitsLoaded;
            dataStorage.HabitDeleted += DataStorage_OnHabitDeleted;
            
            dataStorage.HabitUpdated += DataStorage_OnHabitUpdated;
            dataStorage.HabitAdded += DataStorage_OnHabitAdded;
        }

        private void DataStorage_OnHabitAdded(Habit habit)
        {
            _views.Add(CreateView(habit));
        }

        private void DataStorage_OnHabitUpdated(Habit habit)
        {
            _views.First(v => v.Habit == habit).Configure(habit);
        }

        private void DataStorage_OnHabitDeleted(Habit habit)
        {
            var view = _views.First(v => v.Habit == habit);

            _views.Remove(view);
            
            Destroy(view.gameObject);
        }

        private void DataStorage_OnHabitsLoaded(bool success, Habit[] habits)
        {
            _views = new List<DevelopedHabitView>();
            
            if (!success)
                return;
            
            foreach (Habit habit in habits) 
                _views.Add(CreateView(habit));
        }

        private DevelopedHabitView CreateView(Habit habit)
        {
            return Instantiate(prefab, root).Configure(habit);
        }
    }
}