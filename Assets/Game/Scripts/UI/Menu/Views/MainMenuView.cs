using System;
using Cysharp.Threading.Tasks;
using Game.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Menu.Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Scaler _scaler;

        public event Action OnPlayClicked;

        public void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClicked);
            _scaler.ResetState();
        }

        public async UniTask ShowAsync()
        {
            await _scaler.ShowAsync();
        }

        public async UniTask HideAsync()
        {
            await _scaler.HideAsync();
        }

        private void OnPlayButtonClicked()
        {
            OnPlayClicked?.Invoke();
        }

        public void OnDestroy()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }
    }
}