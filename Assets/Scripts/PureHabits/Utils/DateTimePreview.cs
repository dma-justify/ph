using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Utils
{
    public class DateTimePreview : MonoBehaviour
    {
        public event Action<DateTime> DateChanged; 
        public DateTime DateTime { get; private set; }
        
        [SerializeField] private Button prev;
        [SerializeField] private Button next;
        [SerializeField] private TMP_Text label;
        [SerializeField] private string format = "MMMM d";
        private void Awake()
        {
            DateTime = DateTime.Today;
            
            prev.onClick.AddListener(PreviousDate_OnClick);
            next.onClick.AddListener(NextDate_OnClick);
            
            UpdateLabel();
        }

        private void NextDate_OnClick()
        {
            DateTime = DateTime.AddDays(1);
            DateChanged?.Invoke(DateTime);
            UpdateLabel();
        }

        private void PreviousDate_OnClick()
        {
            DateTime = DateTime.AddDays(-1);
            DateChanged?.Invoke(DateTime);
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            label.text = DateTime.ToString(format);
        }
    }
}
