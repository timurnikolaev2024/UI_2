using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Menu.Views
{
    public class MenuHeroSelectItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _selectedFrame;
        [SerializeField] private Button _button;
        [SerializeField] private Image _colorBg;

        public event Action OnClicked;
        
        public void Awake()
        {
            _button.onClick.AddListener(OnButtonClick);
        }
        
        private void OnButtonClick()
        {
            OnClicked?.Invoke();
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
        
        public void SetColor(Color color)
        {
            _colorBg.color = color;
        }

        public void SetSelected(bool selected)
        {
            _selectedFrame.SetActive(selected);
        }

        public void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}