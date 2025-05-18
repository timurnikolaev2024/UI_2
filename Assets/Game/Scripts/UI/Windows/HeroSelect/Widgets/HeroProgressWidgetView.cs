using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class HeroProgressWidgetView : MonoBehaviour
    {
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Image _levelBar;

        [Header("Health")]
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private Image _healthBar;
        
        [Header("Attack")]
        [SerializeField] private TextMeshProUGUI _attackText;
        [SerializeField] private Image _attackBar;

        [Header("HeroName")]
        [SerializeField] private TextMeshProUGUI _heroName;

        private Tween _levelTween, _healthTween, _attackTween;
        private HeroConfigSO _currentConfig;

        private const float AnimationDuration = 0.4f;

        public void SetProgress(HeroConfigSO config)
        {
            _currentConfig = config;
        }

        public async UniTask PlayAnimationAsync()
        {
            if (_currentConfig == null)
                return;

            _heroName.text = _currentConfig.HeroName;

            AnimateStat(ref _levelTween, _currentConfig.CurrentLevel, _currentConfig.MaxLevel, _levelText, _levelBar);
            AnimateStat(ref _healthTween, _currentConfig.CurrentHealth, _currentConfig.MaxHealth, _healthText, _healthBar);
            AnimateStat(ref _attackTween, _currentConfig.CurrentAttack, _currentConfig.MaxAttack, _attackText, _attackBar);

            await UniTask.Delay((int)(AnimationDuration * 1000));
        }

        private void AnimateStat(ref Tween tween, int value, int max, TextMeshProUGUI label, Image bar)
        {
            tween?.Kill();

            float fromValue = 0f;
            if (int.TryParse(label.text, out int parsed))
                fromValue = parsed;

            float toValue = value;
            float fillTarget = Mathf.Clamp01((float)value / max);

            tween = DOTween.To(() => fromValue, x =>
            {
                label.text = Mathf.RoundToInt(x).ToString();
            }, toValue, AnimationDuration).SetEase(Ease.OutCubic);

            bar.DOFillAmount(fillTarget, AnimationDuration).SetEase(Ease.OutCubic);
        }

        public void ResetImmediate()
        {
            _currentConfig = null;

            _heroName.text = "";
            _levelText.text = "0";
            _healthText.text = "0";
            _attackText.text = "0";

            _levelBar.fillAmount = 0f;
            _healthBar.fillAmount = 0f;
            _attackBar.fillAmount = 0f;
        }
    }
}