using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class MenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private MenuView _menuView;

        private MenuPresenter _presenter;

        private void Start()
        {
            _presenter = new MenuPresenter(_menuView);
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}