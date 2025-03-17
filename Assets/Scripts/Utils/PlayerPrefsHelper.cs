using UnityEngine;

namespace Utils
{
    public class PlayerPrefsHelper
    {
        private const int DefaultValue = 0;
        private const int TrueKey = 1;


        public static bool GetBool(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? TrueKey : DefaultValue) == TrueKey;
        }

        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? TrueKey : DefaultValue);
            PlayerPrefs.Save();
        }

        public static int GetInt(string key, int defaultValue = DefaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public static string GetString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
    }
}