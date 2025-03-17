using UnityEngine;
using UnityEngine.UI;
using Utils.Extended;

namespace Utils.Timer
{
    [RequireComponent(typeof(Text))]
    public class ColoredTimerView : MonoBehaviour, ITimerView
    {
        private Text _view;

        [SerializeField] private Color boundColor = Color.red;
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private int boundTime = 10;

        [SerializeField] private bool format;
        
        private void Awake()
        {
            _view = GetComponent<Text>();
        }
        
        public void SetTime(int seconds)
        {
            _view.color = seconds >= boundTime ? defaultColor : boundColor;
            _view.text = format ? seconds.ToTimeFormat() : seconds.ToString();
        }
    }
}