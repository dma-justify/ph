using UI.AbstractViews;
using UnityEngine;

namespace PureHabits.Habits.New
{
    public class NewHabitButton : AbstractButtonView
    {
        [SerializeField] private HabitsCreator habitsCreator;
        protected override void Button_OnClick() => habitsCreator.CreateNew();
    }
}