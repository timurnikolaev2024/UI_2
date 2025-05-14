using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class Scaler : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        [Header("Scale Settings")]
        [SerializeField] private float _overshootScale = 1.1f;
        [SerializeField] private float _durationIn = 0.03f;
        [SerializeField] private float _durationOut = 0.1f;
        [SerializeField] private Ease _easeIn = Ease.InQuad;
        [SerializeField] private Ease _easeOut = Ease.InQuad;
        [SerializeField] private float _startDelay = 0.5f;

        private Tween _currentTween;

        private void Update()
        {
            // if(Input.GetKeyDown(KeyCode.Space))
            //     Show();
            //
            // if(Input.GetKeyDown(KeyCode.LeftShift))
            //     Hide();
        }

        public void Show()
        {
            _currentTween?.Kill();

            DOVirtual.DelayedCall(_startDelay, () =>
            {
                _canvasGroup.interactable = true;
                _canvasGroup.alpha = 1f;

                gameObject.transform.localScale = Vector3.one * 0.01f;

                _currentTween = gameObject.transform.DOScale(_overshootScale, _durationIn)
                    .SetEase(_easeIn)
                    .OnComplete(() =>
                    {
                        gameObject.transform.DOScale(1f, _durationOut)
                            .SetEase(Ease.OutQuad);
                        _canvasGroup.blocksRaycasts = true;
                    });
            });
        }

        public void Hide()
        {
            _currentTween?.Kill();
            
            _canvasGroup.blocksRaycasts = false;
            gameObject.transform.DOScale(_overshootScale, _durationOut)
                .SetEase(Ease.InQuad)
                .OnComplete(() =>
                {
                    gameObject.transform.DOScale(0.01f, _durationIn)
                        .SetEase(_easeOut)
                        .OnComplete(() =>
                        {
                            _canvasGroup.interactable = false;
                            _canvasGroup.alpha = 0f;
                        });
                });
        }

        public void ResetState()
        {
            _currentTween?.Kill();
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            gameObject.transform.localScale = Vector3.one * 0.01f;
        }
    }
}