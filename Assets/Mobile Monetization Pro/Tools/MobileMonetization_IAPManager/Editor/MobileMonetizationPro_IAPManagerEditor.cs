using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection;
using System;
using System.Linq;

namespace MobileMonetizationPro
{
    [CustomEditor(typeof(MobileMonetizationPro_IAPManager))]
    public class MobileMonetizationPro_IAPManagerEditor : Editor
    {
        private MobileMonetizationPro_IAPManager iapHelper;

        private void OnEnable()
        {
            iapHelper = (MobileMonetizationPro_IAPManager)target;
        }
        private GUIStyle GetButtonStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.button);
            style.normal.textColor = Color.green;
            style.fontSize = 14; // Adjust the font size as needed
            style.fontStyle = FontStyle.Bold;

            return style;
        }

        public override void OnInspectorGUI()
        {
            MobileMonetizationPro_IAPManager script = (MobileMonetizationPro_IAPManager)target;
            DrawDefaultInspector();

            EditorGUILayout.Space();
            DisplayConsumableItemsList();
            DisplayNonConsumableItemsList();
            DisplaySubscriptionItemsList();

            DisplayItemList(iapHelper.CreateConsumableItems, "Consumable Item");
            DisplayItemList(iapHelper.CreateNonConsumableItems, "Non-Consumable Item");
            DisplayItemList(iapHelper.CreateSubscriptions, "Subscription Item");

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("SAVE THE CHANGES", GetButtonStyle(), GUILayout.Width(450), GUILayout.Height(40)))
            {
                // Check if the prefab exists
                string prefabPath = "Assets/Mobile Monetization Pro/Tools/MobileMonetization_IAPManager/Prefab/MobileMonetizationPro_IAPManager.prefab";
                GameObject existingPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

                // Get information for PlayerPrefs key
                string sceneName = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name;
                string gameObjectName = script.gameObject.name;
                string prefabKey = $"{sceneName}_{gameObjectName}";

                if (existingPrefab != null && PlayerPrefs.HasKey(prefabKey))
                {
                    // If the prefab exists and the key exists, apply changes directly to the existing prefab
                    MobileMonetizationPro_IAPManager existingPrefabScript = existingPrefab.GetComponent<MobileMonetizationPro_IAPManager>();
                    if (existingPrefabScript != null)
                    {
                        EditorUtility.CopySerialized(script, existingPrefabScript);

                        // Mark the scene as dirty to ensure changes are saved
                        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene());
                    }
                }
                else
                {
                    // If the prefab doesn't exist or the key doesn't exist, instantiate a new prefab from the original asset
                    GameObject prefabAsset = PrefabUtility.LoadPrefabContents(prefabPath);
                    if (prefabAsset != null)
                    {
                        // Apply changes to the instantiated prefab
                        MobileMonetizationPro_IAPManager newPrefabScript = prefabAsset.GetComponent<MobileMonetizationPro_IAPManager>();
                        if (newPrefabScript != null)
                        {
                            EditorUtility.CopySerialized(script, newPrefabScript);

                            // Save the instantiated prefab as a new prefab asset
                            PrefabUtility.SaveAsPrefabAsset(prefabAsset, prefabPath);

                            // Save information about the new prefab in PlayerPrefs
                            PlayerPrefs.SetString(prefabKey, prefabPath);
                            PlayerPrefs.Save();

                            // Unload the prefab contents
                            PrefabUtility.UnloadPrefabContents(prefabAsset);
                        }
                    }
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal(); 
        }

        private void DisplayItemList<T>(List<T> itemList, string itemType)
        {
            bool isListEmpty = itemList.Count == 0;

            EditorGUILayout.BeginHorizontal();

            if (!isListEmpty && GUILayout.Button("Remove", GUILayout.ExpandWidth(true)))
            {
                itemList.RemoveAt(itemList.Count - 1);
            }

            if (GUILayout.Button("Add " + itemType, GUILayout.ExpandWidth(true)))
            {
                AddItemToItemList(itemList);
            }

            EditorGUILayout.EndHorizontal();
        }

        private void AddItemToItemList<T>(List<T> itemList)
        {
            // You can customize this method to add items of the appropriate type to your list.
            // For now, it adds default values to the list.
            itemList.Add(default(T));
        }
        private void DisplayConsumableItemsList()
        {
            GUIStyle itemLabelStyle = new GUIStyle(EditorStyles.boldLabel);
            itemLabelStyle.normal.textColor = Color.white; // Set text color to dark black

            for (int i = 0; i < iapHelper.CreateConsumableItems.Count; i++)
            {
                GUILayout.BeginVertical("Box");

                EditorGUILayout.LabelField("Consumable Item " + (i + 1), itemLabelStyle); // Apply the style

                ConsumableItem item = iapHelper.CreateConsumableItems[i];

                if (item != null)
                {
                    item.UIButton = (Button)EditorGUILayout.ObjectField("UI Button", item.UIButton, typeof(Button), true);
                    item.CanBuyInQuantity = EditorGUILayout.Toggle("Can Buy In Quantity", item.CanBuyInQuantity);
                    item.ProduceId = EditorGUILayout.TextField("Product ID", item.ProduceId);

                    item.AddScript = (MonoBehaviour)EditorGUILayout.ObjectField("Add Script", item.AddScript, typeof(MonoBehaviour), true);

                    if (item.AddScript != null)
                    {
                        item.FunctionToInvoke = DisplayFunctionList(item.AddScript, item.FunctionToInvoke, "Select Function To Invoke");
                    }
                }

                GUILayout.EndVertical();
            }
        }
        private void DisplayNonConsumableItemsList()
        {
            GUIStyle itemLabelStyle = new GUIStyle(EditorStyles.boldLabel);
            itemLabelStyle.normal.textColor = Color.magenta; // Set text color to dark black

            for (int i = 0; i < iapHelper.CreateNonConsumableItems.Count; i++)
            {
                GUILayout.BeginVertical("Box");

                EditorGUILayout.LabelField("Non Consumable Item " + (i + 1), itemLabelStyle); // Apply the style

                NonConsumableItem item = iapHelper.CreateNonConsumableItems[i];

                if (item != null)
                {
                    item.UIButton = (Button)EditorGUILayout.ObjectField("UI Button", item.UIButton, typeof(Button), true);
                    item.ProduceId = EditorGUILayout.TextField("Product ID", item.ProduceId);
                    item.RemoveAdsFromGame = EditorGUILayout.Toggle("Remove Ads On Purchase", item.RemoveAdsFromGame);
                    item.AddScript = (MonoBehaviour)EditorGUILayout.ObjectField("Add Script", item.AddScript, typeof(MonoBehaviour), true);

                    if (item.AddScript != null)
                    {
                        item.FunctionToInvoke = DisplayFunctionList(item.AddScript, item.FunctionToInvoke, "Select Function To Invoke");
                    }
                }

                GUILayout.EndVertical();
            }
        }
        private void DisplaySubscriptionItemsList()
        {
            GUIStyle itemLabelStyle = new GUIStyle(EditorStyles.boldLabel);
            itemLabelStyle.normal.textColor = Color.yellow; // Set text color to dark black

            for (int i = 0; i < iapHelper.CreateSubscriptions.Count; i++)
            {
                GUILayout.BeginVertical("Box");

                EditorGUILayout.LabelField("Subscription Item" + (i + 1), itemLabelStyle); // Apply the style

                SubscriptionItem item = iapHelper.CreateSubscriptions[i];

                if (item != null)
                {
                    item.UIButton = (Button)EditorGUILayout.ObjectField("UI Button", item.UIButton, typeof(Button), true);
                    item.ProduceId = EditorGUILayout.TextField("Product ID", item.ProduceId);

                    item.AddScript = (MonoBehaviour)EditorGUILayout.ObjectField("Add Script", item.AddScript, typeof(MonoBehaviour), true);

                    if (item.AddScript != null)
                    {
                        item.FunctionToInvokeForActivatingSubscription = DisplayFunctionList(item.AddScript, item.FunctionToInvokeForActivatingSubscription, "Select Function To Invoke For Activation");
                        item.FunctionToInvokeForDeactivatingSubscription = DisplayFunctionList(item.AddScript, item.FunctionToInvokeForDeactivatingSubscription, "Select Function To Invoke For Deactivation");
                    }
                }
                GUILayout.EndVertical();
            }
        }


        private string DisplayFunctionList(MonoBehaviour script, string selectedFunctionName, string FunctionName)
        {
            List<string> functionNames = GetPublicVoidMethodNames(script);
            if (functionNames.Count == 0)
            {
                EditorGUILayout.HelpBox("No public void methods found in the selected script.", MessageType.Warning);
                return null;
            }

            int selectedIndex = functionNames.IndexOf(selectedFunctionName);
            if (selectedIndex == -1)
            {
                selectedIndex = 0; // Default to the first method if not found
            }

            selectedIndex = EditorGUILayout.Popup(FunctionName, selectedIndex, functionNames.ToArray());

            if (selectedIndex >= 0 && selectedIndex < functionNames.Count)
            {
                return functionNames[selectedIndex];
            }

            return null;
        }

        private List<string> GetPublicVoidMethodNames(MonoBehaviour script)
        {
            List<string> methodNames = new List<string>();

            Type type = script.GetType();
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);

            // List of Unity-specific method names to filter out
            string[] unityMethodNames = {
        "Awake", "Start", "Update", "FixedUpdate", "LateUpdate",
        "OnEnable", "OnDisable", "OnDestroy", "OnValidate", /* Add more as needed */
    };

            foreach (MethodInfo method in methods)
            {
                if (method.DeclaringType == type && method.ReturnType == typeof(void) &&
                    method.GetParameters().Length == 0 && !unityMethodNames.Contains(method.Name))
                {
                    methodNames.Add(method.Name);
                }
            }

            return methodNames;
        }
    }
}
