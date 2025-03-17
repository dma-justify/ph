using System;
using Newtonsoft.Json;
using Utils;

namespace PureHabits.Motivation
{
    public class LastMotivation
    {
        private const string LastMotivationKey = "ph_last_motivation_id";
        public DateTime DateTime;
        public int MotivationId;


        public static LastMotivation Load()
        {
            var json = PlayerPrefsHelper.GetString(LastMotivationKey);
            return string.IsNullOrEmpty(json)
                ? new LastMotivation()
                {
                    DateTime = DateTime.Today,
                    MotivationId = 0,
                }
                : JsonConvert.DeserializeObject<LastMotivation>(json);
        }

        public void Save(int id)
        {
            DateTime = DateTime.Today;
            MotivationId = id;


            var json = JsonConvert.SerializeObject(this);
            PlayerPrefsHelper.SetString(LastMotivationKey, json);
        }
    }
}