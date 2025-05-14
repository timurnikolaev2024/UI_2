using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MenuHeroSelectItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameLabel;
        [SerializeField] private Image _progressBar;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _selectedFrame;

        public void SetName(string name) => _nameLabel.text = name;
        public void SetIcon(Sprite sprite) => _icon.sprite = sprite;
        public void SetSelected(bool selected) => _selectedFrame.SetActive(selected);

        public void AnimateProgress(float target) => StartCoroutine(AnimateProgressRoutine(target));

        private IEnumerator AnimateProgressRoutine(float target)
        {
            float current = _progressBar.fillAmount;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 4;
                _progressBar.fillAmount = Mathf.Lerp(current, target, t);
                yield return null;
            }
        }

        public void SetClickListener(UnityAction onClick)
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() =>
            {
                AnimateClick();
                onClick?.Invoke();
            });
        }

        private void AnimateClick()
        {
            transform.DOKill();
            transform.DOScale(0.9f, 0.05f).OnComplete(() => transform.DOScale(1f, 0.1f));
        }

        public Sprite GetIcon() => _icon.sprite;
    }
}