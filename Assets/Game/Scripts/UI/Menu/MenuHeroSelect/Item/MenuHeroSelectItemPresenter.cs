using System;
using Game.Core;
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
            _view.SetIcon(hero.Config.icon);
            _view.SetColor(hero.Config.color);
            Hero = hero;

            SetSelected(false);

            _view.OnClicked += OnHeroClicked;
        }

        private void OnHeroClicked()
        {
            PlayerData.Instance.SelectHero(Hero);
        }

        public void SetSelected(bool selected)
        {
            _view.SetSelected(selected);
        }

        public void Dispose()
        {
            _view.OnClicked -= OnHeroClicked;
        }
    }
}