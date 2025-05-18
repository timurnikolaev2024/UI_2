using System;
using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public class MainMenuPresenter : PresenterBase<MainMenuWindow>, IDisposable
    {
        private readonly WindowService _windows;

        public MainMenuPresenter(MainMenuWindow view, WindowService windows)
            : base(view)
        {
            _windows = windows;
        }

        public override UniTask InitializeAsync()
        {
            Window.PlayClicked += OnPlay;
            return UniTask.CompletedTask;
        }

        private async void OnPlay() 
        {
            await _windows.OpenAsync(WindowId.HeroSelect);
        }

        public void Dispose()
        {
            Window.PlayClicked -= OnPlay;
        }
    }

}