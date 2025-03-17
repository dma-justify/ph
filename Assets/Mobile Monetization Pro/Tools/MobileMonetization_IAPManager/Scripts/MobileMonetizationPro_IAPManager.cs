using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System.Collections.Generic;
using System.Reflection;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

namespace MobileMonetizationPro
{
    [Serializable]
    public class ConsumableItem
    {
        public Button UIButton;
        public bool CanBuyInQuantity = true;
        public string ProduceId;
        public MonoBehaviour AddScript;
        public string FunctionToInvoke;
    }
    [Serializable]
    public class NonConsumableItem
    {
        public Button UIButton;
        public string ProduceId;
        public bool RemoveAdsFromGame = false;
        public MonoBehaviour AddScript;
        public string FunctionToInvoke;
    }
    [Serializable]
    public class SubscriptionItem
    {
        public Button UIButton;
        public string ProduceId;
        public MonoBehaviour AddScript;
        public string FunctionToInvokeForActivatingSubscription;
        public string FunctionToInvokeForDeactivatingSubscription;
    }

    public class MobileMonetizationPro_IAPManager : MonoBehaviour, IStoreListener
    {
        IStoreController m_StoreContoller;
        IExtensionProvider m_StoreExtensionProvider;

        [Header("Restore Purchases For iOS")]
        public Button RestorePurchaseButton;

        [HideInInspector]
        public List<ConsumableItem> CreateConsumableItems = new List<ConsumableItem>();
        [HideInInspector]
        public List<NonConsumableItem> CreateNonConsumableItems = new List<NonConsumableItem>();
        [HideInInspector]
        public List<SubscriptionItem> CreateSubscriptions = new List<SubscriptionItem>();

        public Data data;
        public Payload payload;
        public PayloadData payloadData;

        int ItemIndex;
        int ItemIndexForNonConsumable;
        int ItemIndexForSubscription;
        const string k_Environment = "production";

        void Awake()
        {
            // Uncomment this line to initialize Unity Gaming Services.
            Initialize(OnSuccess, OnError);
        }
        void Initialize(Action onSuccess, Action<string> onError)
        {
            try
            {
                var options = new InitializationOptions().SetEnvironmentName(k_Environment);

                UnityServices.InitializeAsync(options).ContinueWith(task => onSuccess());
            }
            catch (Exception exception)
            {
                onError(exception.Message);
            }
        }
        void OnSuccess()
        {
            var text = "Congratulations!\nUnity Gaming Services has been successfully initialized.";
            // informationText.text = text;
            Debug.Log(text);
        }

        void OnError(string message)
        {
            var text = $"Unity Gaming Services failed to initialize with error: {message}.";
            //  informationText.text = text;
            Debug.LogError(text);
        }
        private void Start()
        {
            for (int i = 0; i < CreateConsumableItems.Count; i++)
            {
                if(CreateConsumableItems[i].UIButton != null)
                {
                    int itemIndex = i; // Create a local variable to capture the current index
                    CreateConsumableItems[i].UIButton.onClick.AddListener(() => Consumable_Btn_Pressed(CreateConsumableItems[itemIndex].ProduceId));
                }
               

            }
            for (int i = 0; i < CreateNonConsumableItems.Count; i++)
            {
                if (CreateNonConsumableItems[i].UIButton != null)
                {
                    int itemIndex = i; // Create a local variable to capture the current index
                    CreateNonConsumableItems[i].UIButton.onClick.AddListener(() => NonConsumable_Btn_Pressed(CreateNonConsumableItems[itemIndex].ProduceId));
                }
             

            }
            for (int i = 0; i < CreateSubscriptions.Count; i++)
            {
                if(CreateSubscriptions[i].UIButton != null)
                {
                    int itemIndex = i; // Create a local variable to capture the current index
                    CreateSubscriptions[i].UIButton.onClick.AddListener(() => Subscription_Btn_Pressed(CreateSubscriptions[itemIndex].ProduceId));
                }
                
            }

            if (RestorePurchaseButton != null)
            {
                if(RestorePurchaseButton != null)
                {
                    RestorePurchaseButton.onClick.AddListener(() => RestorePurchases());
                }
            }

            SetupBuilder();
        }
        public void RemoveAdsFromGame()
        {
            PlayerPrefs.SetInt("AdsRemoved", 1);
        }
        #region setup and initialize
        void SetupBuilder()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (var item in CreateConsumableItems)
            {
                builder.AddProduct(item.ProduceId, ProductType.Consumable);
            }

            foreach (var item in CreateNonConsumableItems)
            {
                builder.AddProduct(item.ProduceId, ProductType.NonConsumable);
            }

            foreach (var item in CreateSubscriptions)
            {
                builder.AddProduct(item.ProduceId, ProductType.Subscription);
            }

            UnityPurchasing.Initialize(this, builder);
        }
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            print("Success");
            m_StoreContoller = controller;
            m_StoreExtensionProvider = extensions;

            for (int i = 0; i < CreateNonConsumableItems.Count; i++)
            {
                ItemIndexForNonConsumable = i;
                CheckNonConsumable(CreateNonConsumableItems[i].ProduceId);
            }
            for (int i = 0; i < CreateSubscriptions.Count; i++)
            {
                ItemIndexForSubscription = i;
                CheckSubscription(CreateSubscriptions[i].ProduceId);
            }
        }
        #endregion


        #region button clicks 
        public void Consumable_Btn_Pressed(string ID)
        {
            ReturnLocalizedPrice(ID);
            m_StoreContoller.InitiatePurchase(ID);
        }

        public void NonConsumable_Btn_Pressed(string ID)
        {

            ReturnLocalizedPrice(ID);
            m_StoreContoller.InitiatePurchase(ID);

        }

        public void Subscription_Btn_Pressed(string ID)
        {


            ReturnLocalizedPrice(ID);
            m_StoreContoller.InitiatePurchase(ID);

        }
        #endregion
        private void SetupButtonCallbackForConsumableItem(ConsumableItem item)
        {
            if (item.UIButton != null && item.AddScript != null && !string.IsNullOrEmpty(item.FunctionToInvoke))
            {
                // Use reflection to invoke the function by name
                MethodInfo method = item.AddScript.GetType().GetMethod(item.FunctionToInvoke);
                if (method != null)
                {
                    // Check if the method is public and has no parameters
                    if (method.IsPublic && method.GetParameters().Length == 0)
                    {
                        method.Invoke(item.AddScript, null);
                    }
                    else
                    {
                        Debug.LogError("Function '" + item.FunctionToInvoke + "' is not public or has parameters.");
                    }
                }
                else
                {
                    Debug.LogError("Function '" + item.FunctionToInvoke + "' not found in script.");
                }
            }
        }
        private void SetupButtonCallbackForNonConsumableItem(NonConsumableItem item)
        {
            if (item.UIButton != null && item.AddScript != null && !string.IsNullOrEmpty(item.FunctionToInvoke))
            {

                // Use reflection to invoke the function by name
                MethodInfo method = item.AddScript.GetType().GetMethod(item.FunctionToInvoke);
                if (method != null)
                {
                    // Check if the method is public and has no parameters
                    if (method.IsPublic && method.GetParameters().Length == 0)
                    {
                        method.Invoke(item.AddScript, null);
                    }
                    else
                    {
                        Debug.LogError("Function '" + item.FunctionToInvoke + "' is not public or has parameters.");
                    }
                }
                else
                {
                    Debug.LogError("Function '" + item.FunctionToInvoke + "' not found in script.");
                }

            }
        }
        private void SetupButtonCallbackForSubscriptionItemActivation(SubscriptionItem item)
        {
            if (item.UIButton != null && item.AddScript != null && !string.IsNullOrEmpty(item.FunctionToInvokeForActivatingSubscription))
            {

                // Use reflection to invoke the function by name
                MethodInfo method = item.AddScript.GetType().GetMethod(item.FunctionToInvokeForActivatingSubscription);
                if (method != null)
                {
                    // Check if the method is public and has no parameters
                    if (method.IsPublic && method.GetParameters().Length == 0)
                    {
                        method.Invoke(item.AddScript, null);
                    }
                    else
                    {
                        Debug.LogError("Function '" + item.FunctionToInvokeForActivatingSubscription + "' is not public or has parameters.");
                    }
                }
                else
                {
                    Debug.LogError("Function '" + item.FunctionToInvokeForActivatingSubscription + "' not found in script.");
                }
            }
        }
        private void SetupButtonCallbackForSubscriptionItemDeactivation(SubscriptionItem item)
        {
            if (item.UIButton != null && item.AddScript != null && !string.IsNullOrEmpty(item.FunctionToInvokeForDeactivatingSubscription))
            {

                // Use reflection to invoke the function by name
                MethodInfo method = item.AddScript.GetType().GetMethod(item.FunctionToInvokeForDeactivatingSubscription);
                if (method != null)
                {
                    // Check if the method is public and has no parameters
                    if (method.IsPublic && method.GetParameters().Length == 0)
                    {
                        method.Invoke(item.AddScript, null);
                    }
                    else
                    {
                        Debug.LogError("Function '" + item.FunctionToInvokeForDeactivatingSubscription + "' is not public or has parameters.");
                    }
                }
                else
                {
                    Debug.LogError("Function '" + item.FunctionToInvokeForDeactivatingSubscription + "' not found in script.");
                }
            }
        }

        #region main
        //processing purchase
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            //Retrive the purchased product
            var product = purchaseEvent.purchasedProduct;

            print("Purchase Complete" + product.definition.id);

            foreach (var item in CreateConsumableItems)
            {
                if (product.definition.id == item.ProduceId)
                {
                    for (int i = 0; i < CreateConsumableItems.Count; i++)
                    {
                        if (item.ProduceId == CreateConsumableItems[i].ProduceId)
                        {
                            ItemIndex = i;
                        }
                    }

                    if (Application.platform == RuntimePlatform.Android)
                    {
                        if (CreateConsumableItems[ItemIndex].CanBuyInQuantity == true)
                        {
                            string receipt = product.receipt;
                            data = JsonUtility.FromJson<Data>(receipt);
                            payload = JsonUtility.FromJson<Payload>(data.Payload);
                            payloadData = JsonUtility.FromJson<PayloadData>(payload.json);

                            int quantity = payloadData.quantity;

                            for (int i = 0; i < quantity; i++)
                            {
                                SetupButtonCallbackForConsumableItem(CreateConsumableItems[ItemIndex]);
                            }
                        }
                        else
                        {
                            SetupButtonCallbackForConsumableItem(CreateConsumableItems[ItemIndex]);
                        }

                    }
                    else if (Application.platform == RuntimePlatform.IPhonePlayer)
                    {
                        SetupButtonCallbackForConsumableItem(CreateConsumableItems[ItemIndex]);
                    }
                }
            }
            foreach (var item in CreateNonConsumableItems)
            {

                if (product.definition.id == item.ProduceId)
                {
                    for (int i = 0; i < CreateNonConsumableItems.Count; i++)
                    {
                        if (item.ProduceId == CreateNonConsumableItems[i].ProduceId)
                        {
                            ItemIndex = i;
                        }
                    }

                    if (CreateNonConsumableItems[ItemIndex].RemoveAdsFromGame == true)
                    {
                        RemoveAdsFromGame();
                    }

                    SetupButtonCallbackForNonConsumableItem(CreateNonConsumableItems[ItemIndex]);
                }
            }
            foreach (var item in CreateSubscriptions)
            {
                if (product.definition.id == item.ProduceId)
                {
                    for (int i = 0; i < CreateSubscriptions.Count; i++)
                    {
                        if (item.ProduceId == CreateSubscriptions[i].ProduceId)
                        {
                            ItemIndex = i;
                        }
                    }
                    SetupButtonCallbackForSubscriptionItemActivation(CreateSubscriptions[ItemIndex]);
                }

            }

            return PurchaseProcessingResult.Complete;
        }
        #endregion




        void CheckNonConsumable(string id)
        {
            if (m_StoreContoller != null)
            {
                var product = m_StoreContoller.products.WithID(id);
                if (product != null)
                {
                    if (product.hasReceipt)//purchased
                    {
                        SetupButtonCallbackForNonConsumableItem(CreateNonConsumableItems[ItemIndexForNonConsumable]);
                    }
                    else
                    {
                        print("Ads Not Removed from the game");
                    }
                }
            }
        }


        void CheckSubscription(string id)
        {

            var subProduct = m_StoreContoller.products.WithID(id);
            if (subProduct != null)
            {
                try
                {
                    if (subProduct.hasReceipt)
                    {
                        var subManager = new SubscriptionManager(subProduct, null);
                        var info = subManager.getSubscriptionInfo();

                        if (info.isSubscribed() == Result.True)
                        {
                            SetupButtonCallbackForSubscriptionItemActivation(CreateSubscriptions[ItemIndexForSubscription]);
                        }
                        else
                        {
                            SetupButtonCallbackForSubscriptionItemDeactivation(CreateSubscriptions[ItemIndexForSubscription]);

                        }

                    }
                    else
                    {
                        print("receipt not found !!");
                    }
                }
                catch (Exception)
                {

                    print("It only work for Google store, app store, amazon store, you are using fake store!!");
                }
            }
            else
            {
                print("product not found !!");
            }
        }


        #region error handeling
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            print("failed" + error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            print("initialize failed" + error + message);
        }



        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            print("purchase failed" + failureReason);
        }

        #endregion
        public string ReturnLocalizedPrice(string id)// Product id
        {
            Product p = m_StoreContoller.products.WithID(id);
            decimal price = p.metadata.localizedPrice;
            string code = p.metadata.isoCurrencyCode;
            return price + " " + code;
        }
        public void RestorePurchases()
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                // Restore purchases for iOS
                Debug.Log("RestorePurchases for iOS started...");
                var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
                apple.RestoreTransactions((result, message) =>
                {
                    if (result)
                    {
                        Debug.Log("RestorePurchases for iOS successful.");
                    }
                    else
                    {
                        Debug.Log("RestorePurchases for iOS failed. Message: " + message);
                    }
                });
            }
        }
    }
    public class SkuDetails
    {
        public string productId;
        public string type;
        public string title;
        public string name;
        public string iconUrl;
        public string description;
        public string price;
        public long price_amount_micros;
        public string price_currency_code;
        public string skuDetailsToken;
    }
    public class PayloadData
    {
        public string orderId;
        public string packageName;
        public string productId;
        public long purchaseTime;
        public int purchaseState;
        public string purchaseToken;
        public int quantity;
        public bool acknowledged;
    }
    public class Payload
    {
        public string json;
        public string signature;
        public List<SkuDetails> skuDetails;
        public PayloadData payloadData;
    }
    public class Data
    {
        public string Payload;
        public string Store;
        public string TransactionID;
    }
}