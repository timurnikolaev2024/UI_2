using System;
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
        [SerializeField] private GameObject _selectedFrame;
        [SerializeField] private Button _button;
        
        public event Action OnClicked;

        private void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
        
        private void OnButtonClick()
        {
            OnClicked?.Invoke();
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        public void SetSelected(bool selected)
        {
            _selectedFrame.SetActive(selected);
        }
    }
}