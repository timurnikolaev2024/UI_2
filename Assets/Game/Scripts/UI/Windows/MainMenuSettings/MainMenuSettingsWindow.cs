using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Windows.MainMenuSettings
{
    public class MainMenuSettingsWindow : WindowBase, IMainMenuSettingsWindow
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Scaler _scaler;
        
        public event Action OnBackClicked;

        public void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _scaler.ResetState();
        }

        private void OnPlayButtonClicked()
        {
            OnBackClicked?.Invoke();
        }

        public override async UniTask Show()
        {
            await _scaler.ShowAsync();
        }

        public override async UniTask Hide()
        {
            await _scaler.HideAsync();
        }
        
        public void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }
    }
}