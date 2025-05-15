using System;
using DG.Tweening;
using Game.Core;
using Game.Events;
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

        private Tween _levelTween, _healthTween, _attackTween;

        private void Awake()
        {
            EventBus.Subscribe<ShowSelectHeroEndedEvent>(OnPanelShown);
            EventBus.Subscribe<HeroSelectedEvent>(OnHeroSelected);
            EventBus.Subscribe<ShowHomeStartedEvent>(OnShowHome);
        }

        private void OnShowHome(ShowHomeStartedEvent e)
        {
            levelText.text = String.Empty;
            healthText.text = String.Empty;
            attackText.text = String.Empty;
            levelBar.fillAmount = 0f;
            healthBar.fillAmount = 0f;
            attackBar.fillAmount = 0f;
        }

        private void OnPanelShown(ShowSelectHeroEndedEvent e)
        {
            SetStats(PlayerData.Instance.SelectedHero.Config);
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe<HeroSelectedEvent>(OnHeroSelected);
            EventBus.Unsubscribe<ShowSelectHeroEndedEvent>(OnPanelShown);
            EventBus.Unsubscribe<ShowHomeStartedEvent>(OnShowHome);
        }
        
        private void OnHeroSelected(HeroSelectedEvent e)
        {
            SetStats(e.Model.Config);
        }

        private void SetStats(HeroConfigSO model)
        {
            heroName.text = model.heroName;
            AnimateStat(ref _levelTween, model.currentLevel, model.maxLevel, levelText, levelBar);
            AnimateStat(ref _healthTween, model.currentHealth, model.maxHealth, healthText, healthBar);
            AnimateStat(ref _attackTween, model.currentAttack, model.maxAttack, attackText, attackBar);
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