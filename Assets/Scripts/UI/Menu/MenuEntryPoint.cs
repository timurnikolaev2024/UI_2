using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class MenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private MenuView _menuView;

        private MenuPresenter presenter;

        private void Start()
        {
            presenter = new MenuPresenter(_menuView);
        }
    }
}