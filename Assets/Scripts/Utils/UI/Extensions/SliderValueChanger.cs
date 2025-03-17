using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Extensions
{
    public class SliderValueChanger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float step = 0.1f;
        [SerializeField] private Slider slider;
        
        private bool _down;

        private void Update()
        {
            if (!_down)
                return;
            
            ChangeValue();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _down = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _down = false;
        }

        private void ChangeValue()
        {
            var value = slider.value;
            
            value += step * Time.deltaTime;

            if (value >= slider.maxValue)
                value = slider.maxValue;

            if (value < slider.minValue)
                value = slider.minValue;
            
            

            slider.value = value;
        }
    }
}