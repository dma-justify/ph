using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace PureHabits.Motivation
{
    public class MotivationView : MonoBehaviour
    {
        [SerializeField] private Text label;
        
        private void Awake()
        {
            label.text = GetMotivationText();
        }

        private string GetMotivationText()
        {
            var motivation = LastMotivation.Load();

            if (motivation.DateTime == DateTime.Today)
                return GetTextStringFromMotivation(MotivationInfo.Motivations[motivation.MotivationId]);

            var id = Random.Range(0, MotivationInfo.Motivations.Length);
            motivation.Save(id);

            return GetTextStringFromMotivation(MotivationInfo.Motivations[id]);
        }

        private string GetTextStringFromMotivation(MotivationInfo info)
        {
            return info.Content + "\n" + "\n" + info.Author;
        }
    }
}