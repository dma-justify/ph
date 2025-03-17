using System;
using System.Collections;
using PureHabits.Data;
using UI;
using UnityEngine;

namespace PureHabits.Onboarding.Sequence
{
    public class SequenceStarter : MonoBehaviour
    {
        [SerializeField] private GameObject onbWindow;
        [SerializeField] private AbstractSequenceElement sequenceElement;
        [SerializeField] private DataStorage dataStorage;
        [SerializeField] private TogglePanelView[] togglePanelView;
        [SerializeField] private GameObject loading;
        
        private void Awake()
        {
            dataStorage.ProfileLoaded += DataStorage_OnProfileLoaded;
            loading.SetActive(true);
        }

        private void OnDestroy()
        {
            dataStorage.ProfileLoaded -= DataStorage_OnProfileLoaded;
        }

        private void DataStorage_OnProfileLoaded(bool success, Data.Profile _) => StartCoroutine(DelayedInit(success));

        private IEnumerator DelayedInit(bool success)
        {
            yield return new WaitForSeconds(2f);
            
            onbWindow.SetActive(!success);
            
            if (!success)
                sequenceElement.ExecuteSequence();
            
            foreach (var view in togglePanelView)
            {
                view.Refresh();
            }
            
            loading.SetActive(false);
        }
    }
}