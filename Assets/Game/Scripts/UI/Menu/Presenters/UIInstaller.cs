using Game.Core;
using Game.UI.Menu.Presenters.Factory;
using Game.UI.Menu.Views;
using UnityEngine;
using Zenject;

namespace Game.UI.Menu.Presenters
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private MenuHeroSelectItemView _itemPrefab;
        [SerializeField] private Transform _itemsRoot;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WindowManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<HeroSelectView> ().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuView>   ().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<HeroSelectPresenter>().AsSingle();

            Container.BindInstance(_itemPrefab).WithId("ItemPrefab");
            Container.BindInstance(_itemsRoot).WithId("ItemsRoot");
            Container
                .BindFactory<HeroModel, System.Action<HeroModel>,
                    MenuHeroSelectItemPresenter, MenuHeroSelectItemPresenter.Factory>()
                .FromFactory<MenuHeroSelectItemPresenterFactory>();
        }
    }
}