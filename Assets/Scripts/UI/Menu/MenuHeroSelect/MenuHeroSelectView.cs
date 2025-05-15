using System.Collections;
using DefaultNamespace.Info;
using DefaultNamespace.Portrait;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [RequireComponent(typeof(MenuHeroSelectAnimator))]
    public class MenuHeroSelectView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _backButton;
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private MenuHeroSelectInfoView _menuHeroSelectInfoView;
        [SerializeField] private MenuHeroSelectPortraitView _menuHeroSelectPortraitView;
        [SerializeField] private MenuHeroSelectItemView _itemPrefab;

        public Button BackButton => _backButton;
        public Transform ItemsContainer => _itemsContainer;
        public MenuHeroSelectItemView ItemPrefab => _itemPrefab;

        public void Show()
        {
            GetComponent<MenuHeroSelectAnimator>().Show();
        }

        public void Hide()
        {
            GetComponent<MenuHeroSelectAnimator>().Hide();
        }
    }
}