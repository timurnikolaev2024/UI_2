using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public class SlideInCanvasAnimator : MonoBehaviour
    {
        [Header("Slide Settings")]
        [SerializeField] private float _slideDistance = 1800f;
        [SerializeField] private float _slideDuration = 0.5f;
        [SerializeField] private Ease _easeIn = Ease.OutBounce;
        [SerializeField] private Ease _easeOut = Ease.OutExpo;
        [SerializeField] private float _startDelay = 0.5f;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Vector2 _originalPos;
        private Tween _currentTween;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Show();
            
            if(Input.GetKeyDown(KeyCode.LeftShift))
                Hide();
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _originalPos = _rectTransform.anchoredPosition;
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void Show()
        {
            _currentTween?.Kill();

            DOVirtual.DelayedCall(_startDelay, () =>
            {
                _rectTransform.anchoredPosition = _originalPos + Vector2.up * _slideDistance;
                _canvasGroup.alpha = 1f;
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;

                _currentTween = _rectTransform.DOAnchorPos(_originalPos, _slideDuration)
                    .SetEase(_easeIn)
                    .OnComplete(() =>
                    {
                        _canvasGroup.interactable = true;
                        _canvasGroup.blocksRaycasts = true;
                    });
            });
        }

        public void Hide()
        {
            _currentTween?.Kill();

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            _currentTween = _rectTransform.DOAnchorPos(_originalPos + Vector2.up * _slideDistance, _slideDuration)
                .SetEase(_easeOut)
                .OnComplete(() =>
                {
                    _canvasGroup.alpha = 0f;
                    _rectTransform.anchoredPosition = _originalPos;
                });
        }

        public void ResetState()
        {
            _currentTween?.Kill();
            _rectTransform.anchoredPosition = _originalPos;
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}