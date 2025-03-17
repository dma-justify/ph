using UnityEngine;

namespace UI.Windows
{
    public class Window : MonoBehaviour
    {
        private const int StopScale = 0;
        private const int RunScale = 1;
        
        [SerializeField] protected bool freezeTimeScale;
        [SerializeField] private bool disableOnDisable;
        private void OnEnable()
        {
            if (freezeTimeScale) 
                Time.timeScale = StopScale;
        }

        private void OnDisable()
        {
            if (freezeTimeScale) 
                Time.timeScale = RunScale;

            if (disableOnDisable) 
                gameObject.SetActive(false);
        }

        public void Open() => gameObject.SetActive(true);

        public void Close() => gameObject.SetActive(false);

        public void Flip()
        {
            if (freezeTimeScale) 
                Time.timeScale = !gameObject.activeSelf ? RunScale : StopScale;
            
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}