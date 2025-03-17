using System;
using System.Collections;
using System.Collections.Generic;
using PureHabits.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Utils
{
    public class LevelProgressView : MonoBehaviour
    {
        [SerializeField] private Image progressLabel;
        [SerializeField] private TMP_Text levelLabel;
        [SerializeField] private DataStorage dataStorage;

        private Coroutine _routine;

        private void Awake() => dataStorage.ExperienceChanged += DataStorage_OnExperienceChanged;

        private void Start() => DataStorage_OnExperienceChanged();

        private void OnDestroy() => dataStorage.ExperienceChanged -= DataStorage_OnExperienceChanged;


        private void DataStorage_OnExperienceChanged()
        {
            if (progressLabel != null)
            {
                if (isActiveAndEnabled)
                {
                    if (_routine != null)
                        StopCoroutine(_routine);
                    
                    _routine = StartCoroutine(FillProgress(dataStorage.Experience > 0
                        ? (dataStorage.Experience % 100f) / 100
                        : 0));
                }
                else
                {
                    progressLabel.fillAmount = dataStorage.Experience > 0
                        ? (dataStorage.Experience % 100f) / 100
                        : 0;
                }
            }
            
            if (levelLabel != null) 
                levelLabel.text = (dataStorage.Experience > 0 ? dataStorage.Experience / 100 : 0).ToString();
        }

        private IEnumerator FillProgress(float progress)
        {
            if (Mathf.Approximately(progressLabel.fillAmount, progress))
            {
                progressLabel.fillAmount = progress;
                _routine = null;
                yield break;
            }
            
            var initialValue = progressLabel.fillAmount;
            for (float t = 0; t <= 1; t+= Time.deltaTime)
            {
                progressLabel.fillAmount = Mathf.Lerp(initialValue, progress, t);
                yield return null;
            }

            progressLabel.fillAmount = progress;
            _routine = null;
        }
    }
}