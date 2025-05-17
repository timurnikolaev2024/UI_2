using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(fileName = "HeroConfig", menuName = "Game/HeroConfig")]
    public class HeroConfigSO : ScriptableObject
    {
        public byte Id; 
        public string HeroName;
        public int MaxLevel;
        public int CurrentLevel;
        public int MaxHealth;
        public int CurrentHealth;
        public int MaxAttack;
        public int CurrentAttack;
        public Sprite Icon;
        public Sprite BigIcon;
        public Color Color;
    }
}