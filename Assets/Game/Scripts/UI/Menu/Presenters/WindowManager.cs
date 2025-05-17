using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.UI.Menu.Presenters
{
    public class WindowManager : MonoBehaviour
    {
        private MainMenuPresenter  _mainMenu;
        private HeroSelectPresenter _heroSelect;

        [Inject]
        private void Construct(MainMenuPresenter mainMenu,
            HeroSelectPresenter heroSelect)
        {
            _mainMenu  = mainMenu;
            _heroSelect = heroSelect;
        }

        private async void Start()
        {
            await UniTask.NextFrame();
            await UniTask.NextFrame();
            await _mainMenu.ShowAsync();
        }

        public async UniTask OpenHeroSelectAsync()
        {
            await _mainMenu.HideAsync();
            await _heroSelect.ShowAsync();
        }

        public async UniTask ReturnToMainMenuAsync()
        {
            await _heroSelect.HideAsync();
            await _mainMenu.ShowAsync();
        }
    }

}