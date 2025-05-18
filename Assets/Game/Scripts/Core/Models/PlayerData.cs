using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Core
{
    public class PlayerData
    {
        public IReadOnlyList<HeroModel> Heroes  { get; }
        public HeroModel                SelectedHero { get; private set; }

        public event Action<HeroModel>  OnHeroSelected;

        public PlayerData(IEnumerable<HeroConfigSO> heroConfigs)
        {
            Heroes = heroConfigs.Select(cfg => new HeroModel(cfg)).ToList();

            if (Heroes.Count > 0)
                SelectHero(Heroes[0]);
        }

        public void SelectHero(HeroModel hero)
        {
            if (hero == null || !Heroes.Contains(hero)) return;

            SelectedHero = hero;
            OnHeroSelected?.Invoke(hero);
        }
    }

}