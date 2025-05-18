using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    [CreateAssetMenu(menuName = "Game/Hero Catalogue")]
    public class HeroCatalogue : ScriptableObject
    {
        public List<HeroConfigSO> Heroes;
    }
}