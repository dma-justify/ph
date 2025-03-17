using System.Linq;
using PureHabits.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Developed
{
    public class DevelopedHabitView : MonoBehaviour
    {
        [SerializeField] private Image bgSprite;
        [SerializeField] private Image iconLabel;
        [SerializeField] private TMP_Text nameLabel;
        [SerializeField] private TMP_Text descLabel;
        [SerializeField] private TMP_Text completeLabel;
        [SerializeField] private TMP_Text uncompleteLabel;

        [SerializeField] private Sprite completeBgSprite;
        [SerializeField] private Sprite uncompleteBgSprite;
        
        [SerializeField] private SpriteStorage iconsStorage;
        public Habit Habit { get; private set; }

        public DevelopedHabitView Configure(Habit habit)
        {
            Habit = habit;
            
            bgSprite.sprite = habit.Positive ? completeBgSprite : uncompleteBgSprite;
            iconLabel.sprite = iconsStorage.GetSprite(habit.IconId);

            nameLabel.text = habit.Name;
            descLabel.text = habit.Desc;

            completeLabel.text = (habit.MarkDates?.Count(m => m.Completed) ?? 0).ToString();
            uncompleteLabel.text = (habit.MarkDates?.Count(m => !m.Completed && m.Marked) ?? 0).ToString();

            return this;
        }
    }
}