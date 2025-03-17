using PureHabits.Data;
using UnityEngine;

namespace PureHabits.Utils
{
    public class PremiumHandler : MonoBehaviour
    {
        [SerializeField] private GameObject premiumWindow;
        [SerializeField] private GameObject premiumButton;
        [SerializeField] private DataStorage dataStorage;
        
        private void Awake()
        {
            dataStorage.PremiumActivated += ConfigurePremium;
            
            ConfigurePremium();
        }

        private void OnDestroy()
        {
            dataStorage.PremiumActivated -= ConfigurePremium;
        }

        private void ConfigurePremium()
        {
            if (premiumWindow.activeSelf)
                premiumWindow.SetActive(!dataStorage.Premium);
            
            premiumButton.SetActive(!dataStorage.Premium);
        }
    }
}