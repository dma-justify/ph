using UnityEngine;

namespace Utils.Extended
{
    public static class Extensions
    {
        private const string DefaultTime = "00:00";
        private const string TimeFormat = "{0}:{1}";
        private const string PartTimeFormat = "0{0}";
        private const int TimeCycle = 60;
        private const int BoundTime = 10;
        
        public static string ToTimeFormat(this int seconds)
        {
            if (seconds == 0)
                return DefaultTime;
            
            return string.Format(TimeFormat, ToPartTimeFormat(seconds / TimeCycle),
                ToPartTimeFormat(seconds % TimeCycle));
        }
        
        private static string ToPartTimeFormat(this int time)
        {
            return BoundTime > time ? string.Format(PartTimeFormat, time) : time.ToString();
        }
        
        /*public static UniWebView ConfigureWebView(this UniWebView view, string url)
        {
            view.Load(url);
            view.Frame = new Rect(0, 0, Screen.width, Screen.height); 
            view.SetShowToolbar(true); 
            view.Show();

            return view;
        }*/

        public static void SetActive(this MonoBehaviour behaviour, bool active) => 
            behaviour.gameObject.SetActive(active);
    }
}