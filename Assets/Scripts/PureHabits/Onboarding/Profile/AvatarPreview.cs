using PureHabits.Data;
using UnityEngine;
using UnityEngine.UI;

namespace PureHabits.Onboarding.Profile
{
    public class AvatarPreview : MonoBehaviour
    {
        [SerializeField] private SpriteSelector selector;
        [SerializeField] private SpriteStorage avatars;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Image view;

        private void Awake()
        {
            selector.Updated += Selector_OnAvatarUpdated;
        }

        private void OnDestroy()
        {
            selector.Updated -= Selector_OnAvatarUpdated;
        }

        private void Selector_OnAvatarUpdated(SpriteSelectorView v)
        {
            view.sprite = v == null ? defaultSprite : avatars.GetSprite(v.Id);
        }
    }
}