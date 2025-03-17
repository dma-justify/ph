using UnityEngine;

namespace Utils.Extended
{
    public class Dispatcher : MonoBehaviour
    {
        private static Dispatcher _instance;

        public static Dispatcher Instance()
        {
            if (_instance == null)
                _instance = CreateInstance();
            
            return _instance;
        }
        
        private static Dispatcher CreateInstance()
        {
            var go = new GameObject(nameof(Dispatcher));
            var dispatcher = go.AddComponent<Dispatcher>();
            return dispatcher;
        }
    }
}