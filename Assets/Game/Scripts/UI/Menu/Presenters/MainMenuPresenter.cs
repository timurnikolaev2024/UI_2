using System;
using Cysharp.Threading.Tasks;
using Game.UI.Menu.Views;

namespace Game.UI.Menu.Presenters
{
    public class MainMenuPresenter : IDisposable
    {
        private readonly MainMenuView _view;
        private readonly WindowManager _manager;

        public MainMenuPresenter(MainMenuView view, WindowManager manager)
        {
            _view = view;
            _manager = manager;
            _view.OnPlayClicked += OnPlayClicked;
        }

        public async UniTask ShowAsync()
        {
            await _view.ShowAsync();
        }

        public async UniTask HideAsync()
        {
            await _view.HideAsync();
        }

        private async void OnPlayClicked()
        {
            await _manager.OpenHeroSelectAsync();
        }

        public void Dispose()
        {
            _view.OnPlayClicked -= OnPlayClicked;
        }
    }

}