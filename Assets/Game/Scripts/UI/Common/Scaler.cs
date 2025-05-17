using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Extensions;
using UnityEngine;

namespace Game.UI.Common
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

        private Tween _currentTween;

        public async UniTask ShowAsync()
        {
            _currentTween?.Kill();

            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;

            gameObject.transform.localScale = Vector3.one * 0.01f;

            await gameObject.transform
                .DOScale(_overshootScale, _durationIn)
                .SetEase(_easeIn)
                .ToUniTask();

            await gameObject.transform
                .DOScale(1f, _durationOut)
                .SetEase(Ease.OutQuad)
                .ToUniTask();

            _canvasGroup.blocksRaycasts = true;
        }

        public async UniTask HideAsync()
        {
            _currentTween?.Kill();

            _canvasGroup.blocksRaycasts = false;

            await gameObject.transform
                .DOScale(_overshootScale, _durationOut)
                .SetEase(Ease.InQuad)
                .ToUniTask();

            await gameObject.transform
                .DOScale(0.01f, _durationIn)
                .SetEase(_easeOut)
                .ToUniTask();

            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0f;
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