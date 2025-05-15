using DG.Tweening;
using Game.Core;
using Game.Events;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Portrait
{
    [RequireComponent(typeof(RawImage))]
    public class MenuHeroSelectPortraitView : MonoBehaviour
    {
        [SerializeField] private Material _wipeMaterial;
        [SerializeField] private float _transitionDuration = 0.5f;

        private RawImage _rawImage;
        private Tween _activeTween;

        private void Awake()
        {
            _rawImage = GetComponent<RawImage>();
            _rawImage.material = new Material(_wipeMaterial);
            HideImmediate();

            EventBus.Subscribe<ShowSelectHeroEndedEvent>(OnPanelShown);
            EventBus.Subscribe<HeroSelectedEvent>(OnHeroSelected);
            EventBus.Subscribe<ShowHomeStartedEvent>(OnShowHomeStarted);
        }

        private void OnShowHomeStarted(ShowHomeStartedEvent obj)
        {
            HideImmediate();
        }

        private void OnPanelShown(ShowSelectHeroEndedEvent obj)
        {
            Show(PlayerData.Instance.SelectedHero.Config.bigIcon);
        }
        private void OnDestroy()
        {
            EventBus.Unsubscribe<ShowSelectHeroEndedEvent>(OnPanelShown);
            EventBus.Unsubscribe<HeroSelectedEvent>(OnHeroSelected);
            EventBus.Unsubscribe<ShowHomeStartedEvent>(OnShowHomeStarted);
        }
        
        private void OnHeroSelected(HeroSelectedEvent e)
        {
            Show(e.Model.Config.bigIcon.texture);
        }

        public void Show(Sprite sprite)
        {
            Show(sprite.texture);
        }

        public void Show(Texture texture)
        {
            KillActiveTween();
            
            float half = _transitionDuration / 2f;
            
            _activeTween = DOTween.To(GetCutoff, SetCutoff, 1f, half).OnComplete(() =>
            {
                _rawImage.texture = texture;
            
                _activeTween = DOTween.To(GetCutoff, SetCutoff, 0f, half);
            });
        }

        public void Hide()
        {
            KillActiveTween();
            _activeTween = DOTween.To(GetCutoff, SetCutoff, 1f, _transitionDuration);
        }

        private void HideImmediate()
        {
            SetCutoff(1f);
        }

        private float GetCutoff()
        {
            return _rawImage.material.GetFloat("_Cutoff");
        }

        private void SetCutoff(float value)
        {
            _rawImage.material.SetFloat("_Cutoff", value);
        }

        private void KillActiveTween()
        {
            if (_activeTween != null && _activeTween.IsActive())
                _activeTween.Kill();
        }
    }


}