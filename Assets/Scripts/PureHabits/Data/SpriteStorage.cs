using UnityEngine;

namespace PureHabits.Data
{
    [CreateAssetMenu(fileName = "SpriteStorage", menuName = "PureHabits/SpriteStorage", order = 0)]
    public class SpriteStorage : ScriptableObject
    {
        public Sprite[] Icons => icons;
        
        [SerializeField] private Sprite[] icons;

        public int GetId(Sprite sprite)
        {
            for (var i = 0; i < icons.Length; i++)
            {
                if (sprite == icons[i])
                    return i;
            }

            return -1;
        }

        public Sprite GetSprite(int id)
        {
            return icons[id];
        }
    }
}