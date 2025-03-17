using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using PureHabits.Data;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Onboarding.Profile
{
    public class SpriteSelector : MonoBehaviour
    {
        public event Action<SpriteSelectorView> Updated; 
        
        [SerializeField] private SpriteSelectorView prefab;
        [SerializeField] private Transform root;
        [SerializeField] private SpriteStorage avatars;
        [SerializeField] private ToggleGroup toggleGroup;
        
        private List<SpriteSelectorView> _views;

        public SpriteSelectorView Selected => _views?.FirstOrDefault(v => v.Toggle.isOn);
        
        private void Awake()
        {
            _views = new List<SpriteSelectorView>();

            for (int id = 0; id < avatars.Icons.Length; id++)
            {
                CreateAvatar(id);
            }
        }
        
        private void CreateAvatar(int id)
        {
            var view = Instantiate(prefab, root);
            view.Construct(id, toggleGroup, RefreshState, avatars);
            _views.Add(view);
        }

        public void Select(int id)
        {
            if (id >= 0)
                _views.First(v => v.Id == id).Toggle.isOn = true;
            else
            {
                if (Selected != null)
                    Selected.Toggle.isOn = false;
            }
                
        }

        private void RefreshState() => Updated?.Invoke(Selected);
    }
}