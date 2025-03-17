using System;
using PureHabits.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Calendar
{
    public class HabitCalendarView : MonoBehaviour
    {
        [SerializeField] private Image iconView;
        [SerializeField] private TMP_Text nameLabel;
        [SerializeField] private TMP_Text descLabel;
        [SerializeField] private SpriteStorage icons;

        public Habit Habit { get; private set; }
        
        public HabitCalendarView Configure(Habit habit)
        {
            Habit = habit;
            
            iconView.sprite = icons.GetSprite(habit.IconId);
            nameLabel.text = habit.Name;
            descLabel.text = habit.Desc;

            return this;
        }
    }
}