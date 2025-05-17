using System;
using Cysharp.Threading.Tasks;
using Game.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Menu.Views
{
    public class HeroSelectView : MonoBehaviour
    {
        [SerializeField] private MenuHeroSelectAnimator _animator;
        [SerializeField] private HeroProgressWidgetView _progressWidget;
        [SerializeField] private HeroImageWidgetView _imageWidget;
        [SerializeField] private Button _backButton;

        public HeroProgressWidgetView ProgressWidget => _progressWidget;
        public HeroImageWidgetView ImageWidget => _imageWidget;

        public event Action OnBackClicked;

        public void Awake()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }

        public async UniTask ShowAsync()
        {
            await _animator.ShowAsync();
        }

        public async UniTask HideAsync()
        {
            await _animator.HideAsync();
        }
        
        private void OnBackButtonClicked()
        {
            OnBackClicked?.Invoke();
        }

        public void OnDestroy()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }
    }
}