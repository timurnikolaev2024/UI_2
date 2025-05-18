using System;
using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public sealed class MenuHeroSelectItemPresenterFactory :
         IFactory<HeroModel, Action<HeroModel>, Transform, MenuHeroSelectItemPresenter>
    {
        private readonly DiContainer            _container;
        private readonly MenuHeroSelectItemView _prefab;

        public MenuHeroSelectItemPresenterFactory(
            DiContainer container,
            [Inject(Id = "HeroItemPrefab")] MenuHeroSelectItemView prefab)
        {
            _container = container;
            _prefab    = prefab;
        }

        public MenuHeroSelectItemPresenter Create(
            HeroModel hero,
            Action<HeroModel> onSelected,
            Transform parent)
        {
            var view = _container.InstantiatePrefabForComponent<MenuHeroSelectItemView>(_prefab, parent);
            return _container.Instantiate<MenuHeroSelectItemPresenter>(
                new object[] { view, hero, onSelected });
        }
    }
}