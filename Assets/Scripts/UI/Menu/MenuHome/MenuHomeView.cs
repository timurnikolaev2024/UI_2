using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MenuHomeView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button _chooseHeroButton;
        public Button ChooseHeroButton => _chooseHeroButton;

        public void Show() => GetComponent<Scaler>().Show();
        public void Hide() => GetComponent<Scaler>().Hide();
    }
}