using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Core;
using Zenject;

namespace Game.UI
{
    public sealed class HeroSelectPresenter
        : WindowPresenterBase<HeroSelectWindow>, IDisposable
    {
        private readonly WindowService _windows;
        private readonly MenuHeroSelectItemPresenter.Factory _itemFactory;
        private readonly PlayerData _player;
        private readonly List<MenuHeroSelectItemPresenter> _items   = new();
        private readonly Dictionary<byte, MenuHeroSelectItemPresenter> _byId = new();
        private MenuHeroSelectItemPresenter _current;
        
        [Inject]
        public HeroSelectPresenter(
            HeroSelectWindow window,
            WindowService windows,
            MenuHeroSelectItemPresenter.Factory itemFactory,

            PlayerData player)
            : base(window)
        {
            _windows = windows;
            _itemFactory = itemFactory;
            _player = player;
        }

        public override UniTask InitializeAsync()
        {
            Window.BackClicked += OnBack;
            SpawnItemsIfNeeded();
            return UniTask.CompletedTask;
        }
        
        public override async UniTask OnShowAsync()
        {
            await base.OnShowAsync();
            SelectHero(_player.SelectedHero);
            await AnimateWidgetsAsync(_player.SelectedHero);
        }

        public override async UniTask OnHideAsync()
        {
            await base.OnHideAsync();
            Window.ProgressWidget.ResetImmediate();
            Window.ImageWidget.HideImmediate();
        }
        
        private void SpawnItemsIfNeeded()
        {
            if (_items.Count > 0) return;

            foreach (var hero in _player.Heroes)
            {
                var presenter = _itemFactory.Create(
                                    hero,
                                    OnHeroChosen,
                                    Window.ItemsRoot);

                bool selected = hero == _player.SelectedHero;
                presenter.SetSelected(selected);
                if (selected) _current = presenter;

                _items.Add(presenter);
                _byId[hero.Config.Id] = presenter;
            }
        }

        private void SelectHero(HeroModel hero)
        {
            if (!_byId.TryGetValue(hero.Config.Id, out var presenter))
                return;

            if (_current != null && _current != presenter)
                _current.SetSelected(false);

            _current = presenter;
            _current.SetSelected(true);

            Window.ProgressWidget.SetProgress(hero.Config);
        }

        private async void OnHeroChosen(HeroModel hero)
        {
            _player.SelectHero(hero);
            SelectHero(hero);
            await AnimateWidgetsAsync(hero);
        }

        private async UniTask AnimateWidgetsAsync(HeroModel hero)
        {
            await UniTask.WhenAll(
                Window.ProgressWidget.PlayAnimationAsync(),
                Window.ImageWidget.PlayAnimationAsync(hero.Config.BigIcon));
        }

        private async void OnBack()
        {
            await _windows.OpenAsync(WindowId.MainMenu);
        }

        public void Dispose()
        {
            Window.BackClicked -= OnBack;
        }
    }
}
