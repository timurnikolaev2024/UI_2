using System;
using Cysharp.Threading.Tasks;

namespace Game.UI.Windows.MainMenuSettings
{
    public class MainMenuSettingsPresenter : WindowPresenterBase<MainMenuSettingsWindow>, IDisposable
    {
        private readonly WindowService _windows;

        public MainMenuSettingsPresenter(MainMenuSettingsWindow view, WindowService windows)
            : base(view)
        {
            _windows = windows;
        }

        public override UniTask InitializeAsync()
        {
            Window.OnBackClicked += OnPlay;
            return UniTask.CompletedTask;
        }

        private async void OnPlay() 
        {
            await _windows.CloseCurrentAsync();
        }

        public void Dispose()
        {
            Window.OnBackClicked -= OnPlay;
        }
    }
}