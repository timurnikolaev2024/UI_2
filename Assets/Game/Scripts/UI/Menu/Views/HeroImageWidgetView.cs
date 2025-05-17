using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Menu.Views
{
    [RequireComponent(typeof(RawImage))]
    public class HeroImageWidgetView : MonoBehaviour
    {
        [SerializeField] private Material _wipeMaterial;
        [SerializeField] private float _transitionDuration = 0.5f;

        private RawImage _rawImage;
        private Tween _activeTween;

        public void Awake()
        {
            _rawImage = GetComponent<RawImage>();
            _rawImage.material = new Material(_wipeMaterial);
            HideImmediate();
        }

        public async UniTask PlayAnimationAsync(Sprite sprite)
        {
            await ShowAsync(sprite.texture);
        }

        public async UniTask HideAsync()
        {
            KillActiveTween();
        
            var tcs = new UniTaskCompletionSource();
        
            _activeTween = DOTween.To(GetCutoff, SetCutoff, 1f, _transitionDuration)
                .OnComplete(() => tcs.TrySetResult());
        
            await tcs.Task;
        }

        private async UniTask ShowAsync(Texture texture)
        {
            KillActiveTween();

            float half = _transitionDuration / 2f;
            
            await DOTween.To(GetCutoff, SetCutoff, 1f, half).ToUniTask();

            _rawImage.texture = texture;

            await DOTween.To(GetCutoff, SetCutoff, 0f, half).ToUniTask();
        }

        public void HideImmediate()
        {
            SetCutoff(1f);
        }

        private float GetCutoff() => _rawImage.material.GetFloat("_Cutoff");
        private void SetCutoff(float value) => _rawImage.material.SetFloat("_Cutoff", value);

        private void KillActiveTween()
        {
            if (_activeTween?.IsActive() == true)
                _activeTween.Kill();
        }
    }
}