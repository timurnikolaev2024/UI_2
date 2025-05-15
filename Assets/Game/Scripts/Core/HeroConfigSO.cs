using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Game/HeroConfig")]
    public class HeroConfigSO : ScriptableObject
    {
        public byte id;
        public string heroName;
        public int maxLevel;
        public int currentLevel;
        public int maxHealth;
        public int currentHealth;
        public int maxAttack;
        public int currentAttack;
        public Sprite icon;
        public Sprite bigIcon;
        public Color color;
    }
}