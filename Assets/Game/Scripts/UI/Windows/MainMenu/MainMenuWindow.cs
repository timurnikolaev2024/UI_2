using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainMenuWindow : WindowBase, IMainMenuWindow
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Scaler _scaler;
        
        public event Action PlayClicked;

        public void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _scaler.ResetState();
        }

        private void OnPlayButtonClicked()
        {
            PlayClicked?.Invoke();
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