using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Info
{
    public class MenuHeroSelectInfoView : MonoBehaviour
    {
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image levelBar;

        [Header("Health")]
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Image healthBar;

        [Header("Attack")]
        [SerializeField] private TextMeshProUGUI attackText;
        [SerializeField] private Image attackBar;
        
        [Header("HeroName")]
        [SerializeField] private TextMeshProUGUI heroName;

        private Tween levelTween, healthTween, attackTween;

        private void Awake()
        {
            UIEventBus.Subscribe<ShowSelectHeroEndedEvent>(OnPanelShown);
            UIEventBus.Subscribe<HeroSelectedEvent>(OnHeroSelected);
        }

        private void OnPanelShown(ShowSelectHeroEndedEvent obj)
        {
            SetStats(PlayerData.Instance.SelectedHero.Config);
        }

        private void OnDestroy()
        {
            UIEventBus.Unsubscribe<HeroSelectedEvent>(OnHeroSelected);
            UIEventBus.Unsubscribe<ShowSelectHeroEndedEvent>(OnPanelShown);
        }
        
        private void OnHeroSelected(HeroSelectedEvent e)
        {
            SetStats(e.Model.Config);
        }

        private void SetStats(HeroConfigSO model)
        {
            heroName.text = model.heroName;
            AnimateStat(ref levelTween, model.currentLevel, model.maxLevel, levelText, levelBar);
            AnimateStat(ref healthTween, model.currentHealth, model.maxHealth, healthText, healthBar);
            AnimateStat(ref attackTween, model.currentAttack, model.maxAttack, attackText, attackBar);
        }

        private void AnimateStat(ref Tween tween, int value, int max, TextMeshProUGUI label, Image bar)
        {
            tween?.Kill();

            float duration = 0.4f;
            float fromValue = 0;
            if (int.TryParse(label.text, out int current))
                fromValue = current;

            float toValue = value;
            float targetFill = Mathf.Clamp01((float)value / max);

            tween = DOTween.To(() => fromValue, x =>
            {
                label.text = Mathf.RoundToInt(x).ToString();
            }, toValue, duration).SetEase(Ease.OutCubic);

            bar.DOFillAmount(targetFill, duration).SetEase(Ease.OutCubic);
        }
    }
}