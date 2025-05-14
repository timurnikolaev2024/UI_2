using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class AnimatedButton : Button
    {
        [SerializeField] private float _pressedScale = 0.9f;
        [SerializeField] private float _pressDuration = 0.08f;
        [SerializeField] private float _releaseDuration = 0.15f;
        
        private Transform _cachedTransform;
        private Shadow _shadow;

        protected override void Awake()
        {
            base.Awake();
            _cachedTransform = transform;
            TryGetComponent(out _shadow);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            AnimatePress();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            AnimateRelease();
        }

        private void AnimatePress()
        {
            if (_shadow != null)
                _shadow.enabled = false;
            
            _cachedTransform.DOKill();
            _cachedTransform.DOScale(_pressedScale, _pressDuration).SetEase(Ease.OutQuad)
                .SetUpdate(true);
        }

        private void AnimateRelease()
        {
            if (_shadow != null)
                _shadow.enabled = true;
            
            _cachedTransform.DOKill();
            _cachedTransform.DOScale(1f, _releaseDuration).SetEase(Ease.OutBack);
        }
    }
}