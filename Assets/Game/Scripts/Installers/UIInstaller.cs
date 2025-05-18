using System;
using Game.Core;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private MenuHeroSelectItemView _itemPrefab;
        [SerializeField] private WindowCatalogue _catalogue;
        [SerializeField] private Transform _spawnRoot;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UIEntryPoint>().FromComponentInHierarchy().AsSingle();

            Container.BindInstance(_catalogue);

            Container.Bind<WindowService>().AsSingle().WithArguments(_spawnRoot);
            
            Container.BindInstance(_itemPrefab)
                .WithId("HeroItemPrefab");
            
            Container.Bind<MenuHeroSelectItemPresenter>().AsTransient();
            
            Container.BindFactory<
                    HeroModel,
                    Action<HeroModel>,
                    Transform,
                    MenuHeroSelectItemPresenter,
                    MenuHeroSelectItemPresenter.Factory>()
                .FromFactory<MenuHeroSelectItemPresenterFactory>();
        }
    }
}