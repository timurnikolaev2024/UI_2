using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MenuHeroSelectView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _backButton;
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private Image _selectedHeroIcon;

        public Button BackButton => _backButton;
        public Transform ItemsContainer => _itemsContainer;

        public void Show()
        {
            GetComponent<SlideInCanvasAnimator>().Show();
        }

        public void Hide()
        {
            GetComponent<SlideInCanvasAnimator>().Hide();
        }
    }
}