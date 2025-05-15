using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [RequireComponent(typeof(RawImage))]
    public class HeroWipeTransition : MonoBehaviour
    {
        [SerializeField] private Material wipeMaterial;
        [SerializeField] private float transitionDuration = 0.5f;

        private RawImage rawImage;
        private Tween activeTween;

        private void Awake()
        {
            rawImage = GetComponent<RawImage>();
            rawImage.material = new Material(wipeMaterial);
            HideCurrentHero();
        }

        public void ShowCurrentHero()
        {
            KillActiveTween();
            SetCutoff(1f);
            activeTween = DOTween.To(GetCutoff, SetCutoff, 0f, transitionDuration);
        }

        public void HideCurrentHero()
        {
            KillActiveTween();
            activeTween = DOTween.To(GetCutoff, SetCutoff, 1f, transitionDuration);
        }

        public void ShowNewHero(Texture newTexture)
        {
            KillActiveTween();

            float half = transitionDuration / 2f;

            activeTween = DOTween.To(GetCutoff, SetCutoff, 1f, transitionDuration)
                .OnComplete(() =>
                {
                    rawImage.texture = newTexture;

                    activeTween = DOTween.To(GetCutoff, SetCutoff, 0f, transitionDuration);
                });
        }

        public void ShowNewHero(Sprite sprite)
        {
            ShowNewHero(sprite.texture);
        }

        private float GetCutoff() => rawImage.material.GetFloat("_Cutoff");
        private void SetCutoff(float value) => rawImage.material.SetFloat("_Cutoff", value);

        private void KillActiveTween()
        {
            if (activeTween != null && activeTween.IsActive())
                activeTween.Kill();
        }
    }
}