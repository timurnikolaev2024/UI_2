using System;
using System.Collections;
using Game.UI.Common;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Scaler))]
    public class MenuHomeView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _chooseHeroButton;
        public Button ChooseHeroButton => _chooseHeroButton;
        private Scaler _scaler;

        private void Awake()
        {
            _scaler = GetComponent<Scaler>();
        }

        public void Show()
        {
            _scaler.Show();
        }

        public void Hide()
        {
            _scaler.Hide();
        }
    }
}