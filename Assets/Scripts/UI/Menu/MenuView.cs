using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private MenuHomeView _homeView;
        [SerializeField] private MenuHeroSelectView _heroSelectView;

        public MenuHomeView HomeView => _homeView;
        public MenuHeroSelectView HeroSelectView => _heroSelectView;
    }
}