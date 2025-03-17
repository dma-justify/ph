using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Utils;

namespace PureHabits.Data
{
    public class DataStorage : MonoBehaviour
    {
        private const int HabitsLimitedCount = 3;
        
        private const string ExperienceKey = "tph_user_level_data";
        private const string PremiumKey = "tph_user_premium_active";
        private const string ProfileKey = "tph_user_profile_data";
        private const string HabitsKey = "tph_user_habits_data";
        
        public event Action PremiumActivated;

        public event Action ExperienceChanged;
        
        public event Action<bool, Habit[]> HabitsLoaded; 
        public event Action<bool, Profile> ProfileLoaded;
        public event Action<Profile> ProfileUpdated; 
        public event Action<Habit> HabitAdded;
        public event Action<Habit> HabitUpdated;
        public event Action<Habit> HabitDeleted;
 
        public bool Premium
        {
            get => PlayerPrefsHelper.GetBool(PremiumKey);
            private set => PlayerPrefsHelper.SetBool(PremiumKey, value);
        }

        public int Experience
        {
            get => PlayerPrefsHelper.GetInt(ExperienceKey);

            private set
            {
                PlayerPrefsHelper.SetInt(ExperienceKey, value >= 0 ? value : 0);
                ExperienceChanged?.Invoke();
            }
        }

        public bool CanCreateHabit => Premium || HabitsLimitedCount >= _habits.Count;

        public Profile Profile => _profile;
        public IReadOnlyList<Habit> Habits => _habits;
        
        private Profile _profile;
        private List<Habit> _habits;
        
        private void Start()
        {
            string habitsJson = PlayerPrefsHelper.GetString(HabitsKey, string.Empty);
            string profileJson = PlayerPrefsHelper.GetString(ProfileKey, string.Empty);
            
            bool habitsExists = !string.IsNullOrEmpty(habitsJson);
            bool profileExists = !string.IsNullOrEmpty(profileJson);
            
            _profile = profileExists ? JsonConvert.DeserializeObject<Profile>(profileJson) : null;
            Habit[] habits = habitsExists ? JsonConvert.DeserializeObject<HabitsCollection>(habitsJson).Habits : null;

            
            _habits = habitsExists ? habits.ToList() : new List<Habit>();
            
            Debug.Log($"Profile loaded \n{profileJson}");
            Debug.Log($"Habits loaded \n{habitsJson}");
            Debug.Log($"Premium active: {Premium}");
            
            ProfileLoaded?.Invoke(profileExists, _profile);
            HabitsLoaded?.Invoke(habitsExists, habits);
        }

        public void CreateProfile(string profileName, int avatar)
        {
            var profile = new Profile()
            {
                Name = profileName,
                AvatarId = avatar
            };

            string json = JsonConvert.SerializeObject(profile);
            PlayerPrefsHelper.SetString(ProfileKey, json);

            _profile = profile;
            
            ProfileLoaded?.Invoke(true, profile);
        }

        public void ChangeProfileName(string newName)
        {
            _profile.Name = newName;
            string json = JsonConvert.SerializeObject(_profile);
            PlayerPrefsHelper.SetString(ProfileKey, json);
            ProfileUpdated?.Invoke(_profile);
        }

        public void ApplyHabitChange(Habit habit = null)
        {
            if (habit != null && !_habits.Contains(habit)) 
                Debug.Log("Matching Habit Not Found");
            
            var collection = new HabitsCollection
            {
                Habits = _habits.ToArray()
            };
            
            string json = JsonConvert.SerializeObject(collection);
            PlayerPrefsHelper.SetString(HabitsKey, json);
            
            HabitUpdated?.Invoke(habit);
        }

        public void AddHabit(Habit habit)
        {
            _habits.Add(habit);

            var collection = new HabitsCollection
            {
                Habits = _habits.ToArray()
            };
            
            string json = JsonConvert.SerializeObject(collection);
            PlayerPrefsHelper.SetString(HabitsKey, json);
            
            HabitAdded?.Invoke(habit);
        }

        public void DeleteHabit(Habit habit)
        {
            _habits.Remove(habit);
            
            var collection = new HabitsCollection
            {
                Habits = _habits.ToArray()
            };
            
            string json = JsonConvert.SerializeObject(collection);
            PlayerPrefsHelper.SetString(HabitsKey, json);
            
            HabitDeleted?.Invoke(habit);
        }

        public void SetHabitCompletion(Habit habit, bool completed)
        {
            MarkDate todayState = null;

            if (habit.MarkDates == null)
                habit.MarkDates = new List<MarkDate>();
            else
                todayState = habit.MarkDates.FirstOrDefault(m =>
                    m.DateTime.DayOfYear == DateTime.Today.DayOfYear && m.DateTime.Year == DateTime.Today.Year);

            if (todayState == null)
            {
                todayState = new MarkDate
                {
                    DateTime = DateTime.Today,
                };

                habit.MarkDates.Add(todayState);
            }

            todayState.Marked = true;
            todayState.Completed = completed;

            ApplyHabitChange(habit);

            if (_habits != null)
            {
                var count = _habits.SelectMany(h => h.MarkDates).Count(m => m.Completed);

                Experience = count * 25;
            }
        }


        public MarkDate[] GetStatisticsForDay(DateTime date)
        {
            var stat = GetStatistics();

            return stat.Where(s => s.DateTime.DayOfYear == date.DayOfYear && s.DateTime.Year == date.Year).ToArray();
        }

        public void ActivatePremium()
        {
            Premium = true;
            PremiumActivated?.Invoke();
        }

        public MarkDate[] GetStatistics()
        {
           return _habits.SelectMany(h => h.MarkDates).ToArray();
        }
    }
}