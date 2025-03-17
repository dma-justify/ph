using PureHabits.Data;
using UI.AbstractViews;
using UnityEngine;

namespace PureHabits.Onboarding.Profile
{
    public class RandomProfile : AbstractButtonView
    {
        [SerializeField] private SpriteSelector avatarSelector;
        [SerializeField] private NameInput nameInput;
        [SerializeField] private SpriteStorage avatars;

        private string[] names = new[]
        {
            "Iron Fang",
            "Shadow Howl",
            "Crimson Blade",
            "Stormbreaker",
            "Nightshade",
            "Frostbite",
            "Emberclaw",
            "Rusty Hook",
            "Thunderhoof",
            "Venomstrike"
        };
        
        protected override void Button_OnClick()
        {
            avatarSelector.Select(Random.Range(0, avatars.Icons.Length));
            nameInput.SetValue(names[Random.Range(0, names.Length)]);
        }
    }
}