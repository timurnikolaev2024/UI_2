using System;
using Game.Core;
using Game.UI.Menu.Views;
using UnityEngine;
using Zenject;

namespace Game.UI.Menu.Presenters.Factory
{
    public class MenuHeroSelectItemPresenterFactory
        : IFactory<HeroModel, Action<HeroModel>, MenuHeroSelectItemPresenter>
    {
        readonly DiContainer _container;
        readonly MenuHeroSelectItemView _prefab;
        readonly Transform _root;

        public MenuHeroSelectItemPresenterFactory(
            DiContainer container,
            [Inject(Id = "ItemPrefab")] MenuHeroSelectItemView prefab,
            [Inject(Id = "ItemsRoot")]  Transform root)
        {
            _container = container;
            _prefab = prefab;
            _root = root;
        }

        public MenuHeroSelectItemPresenter Create(HeroModel hero, Action<HeroModel> cb)
        {
            var view = _container.InstantiatePrefabForComponent<MenuHeroSelectItemView>(_prefab, _root);
            return _container.Instantiate<MenuHeroSelectItemPresenter>(
                new object[] { view, hero, cb });
        }
    }
}