using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.UI.Menu.Views;
using Zenject;

namespace Game.UI.Menu.Presenters
{
   public class HeroSelectPresenter : IDisposable
    {
        private readonly HeroSelectView _view;
        private readonly WindowManager _manager;
        private readonly MenuHeroSelectItemPresenter.Factory _factory;
        private readonly List<MenuHeroSelectItemPresenter> _itemPresenters = new();
        private readonly Dictionary<byte, MenuHeroSelectItemPresenter> _items = new();
        private MenuHeroSelectItemPresenter _currentSelected;
        private readonly PlayerData _playerData;

        [Inject]
        public HeroSelectPresenter(HeroSelectView view,
            WindowManager manager,
            MenuHeroSelectItemPresenter.Factory factory,
            PlayerData playerData)
        {
            _view = view;
            _manager = manager;
            _factory = factory;
            _playerData = playerData;
            _view.OnBackClicked += OnBackClicked;
        }

        public async UniTask ShowAsync()
        {
            CreateHeroItems();

            var selectedHero = _playerData.SelectedHero;
            SelectHero(selectedHero);

            await _view.ShowAsync();
            await AnimateHeroWidgetsAsync(selectedHero);
        }

        public async UniTask HideAsync()
        {
            _view.ProgressWidget.ResetImmediate();
            _view.ImageWidget.HideImmediate();
            await _view.HideAsync();
        }

        public void Dispose()
        {
            _view.OnBackClicked -= OnBackClicked;
        }

        private void CreateHeroItems()
        {
            if (_items.Count > 0) return;

            var selectedHero = _playerData.SelectedHero;

            foreach (var hero in _playerData.Heroes)
            {
                var presenter = _factory.Create(hero, OnHeroSelected);
                bool isSelected = hero == selectedHero;

                presenter.SetSelected(isSelected);
                if (isSelected)
                    _currentSelected = presenter;

                _itemPresenters.Add(presenter);
                _items[hero.Config.Id] = presenter;
            }
        }

        private void SelectHero(HeroModel hero)
        {
            if (!_items.TryGetValue(hero.Config.Id, out var presenter))
                return;

            if (_currentSelected != null && _currentSelected != presenter)
                _currentSelected.SetSelected(false);

            _currentSelected = presenter;
            _currentSelected.SetSelected(true);

            _view.ProgressWidget.SetProgress(hero.Config);
        }

        private async void OnHeroSelected(HeroModel hero)
        {
            _playerData.SelectHero(hero);
            SelectHero(hero);
            await AnimateHeroWidgetsAsync(hero);
        }

        private async UniTask AnimateHeroWidgetsAsync(HeroModel hero)
        {
            await UniTask.WhenAll(
                _view.ProgressWidget.PlayAnimationAsync(),
                _view.ImageWidget.PlayAnimationAsync(hero.Config.BigIcon)
            );
        }

        private async void OnBackClicked()
        {
            await _manager.ReturnToMainMenuAsync();
        }
    }
}