using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MenuHeroSelectItemPresenter
    {
        private readonly MenuHeroSelectItemView _view;
        public HeroModel Hero { get; }

        public MenuHeroSelectItemPresenter(MenuHeroSelectItemView view, HeroModel hero)
        {
            _view = view;
            view.SetIcon(hero.Config.icon);
            Hero = hero;

            SetSelected(false);

            _view.OnClicked += () =>
            {
                PlayerData.Instance.SelectHero(Hero);
            };
        }

        public void SetSelected(bool selected)
        {
            _view.SetSelected(selected);
        }
    }
}