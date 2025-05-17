using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Extensions;
using UnityEngine; 

namespace Game.UI.Common
{
    public class MenuHeroSelectAnimator : MonoBehaviour
    {
        [SerializeField] private float _slideDistance = 1800f;
        [SerializeField] private float _slideDuration = 0.5f;
        [SerializeField] private Ease _easeIn = Ease.OutBounce;
        [SerializeField] private Ease _easeOut = Ease.OutExpo;
        [SerializeField] private float _startDelay = 0.5f;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Vector2 _originalPos;
        private Tween _currentTween;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _originalPos = _rectTransform.anchoredPosition;
            ResetState();
        }

        public async UniTask ShowAsync()
        {
            _currentTween?.Kill();

            await UniTask.Delay(System.TimeSpan.FromSeconds(_startDelay));

            _rectTransform.anchoredPosition = _originalPos + Vector2.up * _slideDistance;
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            await _rectTransform
                .DOAnchorPos(_originalPos, _slideDuration)
                .SetEase(_easeIn)
                .ToUniTask();

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public async UniTask HideAsync()
        {
            _currentTween?.Kill();

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            await _rectTransform
                .DOAnchorPos(_originalPos + Vector2.up * _slideDistance, _slideDuration)
                .SetEase(_easeOut)
                .ToUniTask();

            _canvasGroup.alpha = 0f;
            _rectTransform.anchoredPosition = _originalPos;
        }

        private void ResetState()
        {
            _currentTween?.Kill();
            _rectTransform.anchoredPosition = _originalPos;
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}