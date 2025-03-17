using UnityEngine;
using UnityEngine.UI;
using Utils.Extended;

namespace Utils.Timer
{
    [RequireComponent(typeof(Text))]
    public class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField] private bool format = true;
        [SerializeField] private bool rebuild;
        
        private Text _view;
        
        private void Awake()
        {
            _view = GetComponent<Text>();
        }

        public void SetTime(int seconds)
        {
            _view.text = format ? seconds.ToTimeFormat() : seconds.ToString();
            
            if (rebuild)
                gameObject.RebuildGraphic();
        }
    }
}