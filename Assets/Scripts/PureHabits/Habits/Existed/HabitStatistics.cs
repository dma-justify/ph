using TMPro;
using UnityEngine;

namespace PureHabits.Habits.Existed
{
    public class HabitStatistics : MonoBehaviour
    {
        [SerializeField] private TMP_Text completedLabel;
        [SerializeField] private TMP_Text uncompletedLabel;
        [SerializeField] private TMP_Text skippedLabel;

        public void SetStatistics(int completed, int uncompleted, int skipped = 0)
        {
            if (completedLabel != null)
                completedLabel.text = completed.ToString();
            
            if (uncompletedLabel != null)
                uncompletedLabel.text = uncompleted.ToString();
            
            if (skippedLabel != null)
                skippedLabel.text = skipped.ToString();
        }
    }
}