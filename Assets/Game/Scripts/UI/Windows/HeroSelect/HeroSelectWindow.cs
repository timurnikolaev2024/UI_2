using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class HeroSelectWindow : WindowBase, IHeroSelectWindow
    {
        [SerializeField] private MenuHeroSelectAnimator _animator;
        [SerializeField] private HeroProgressWidgetView _progressWidget;
        [SerializeField] private HeroImageWidgetView _imageWidget;
        [SerializeField] private Button _backButton;
        [SerializeField] private Transform _itemsRoot;

        public HeroProgressWidgetView ProgressWidget => _progressWidget;
        public HeroImageWidgetView ImageWidget => _imageWidget;
        public event Action BackClicked;
        public Transform ItemsRoot => _itemsRoot;

        public void Awake()
        {
            _backButton.onClick.AddListener(OnBackButtonClicked);
        }
        
        private void OnBackButtonClicked()
        {
            BackClicked?.Invoke();
        }

        public override async UniTask Show()
        {
            await _animator.ShowAsync();
        }

        public override async UniTask Hide()
        {
            await _animator.HideAsync();
        }
        
        public void OnDestroy()
        {
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
        }
    }
}