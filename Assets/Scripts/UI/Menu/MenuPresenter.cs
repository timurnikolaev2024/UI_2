using UnityEngine;

namespace DefaultNamespace
{
    public class MenuPresenter
    {
        private readonly MenuView _view;
        private readonly MenuHomePresenter _homePresenter;
        private readonly MenuHeroSelectPresenter _heroSelectPresenter;

        public MenuPresenter(MenuView view)
        {
            _view = view;
            _homePresenter = new MenuHomePresenter(view.HomeView, this);
            _heroSelectPresenter = new MenuHeroSelectPresenter(view.HeroSelectView, this);

            ShowHome();
        }

        public void ShowHome()
        {
            _view.HomeView.Show();
            _view.HeroSelectView.Hide();
        }

        public void ShowHeroSelect()
        {
            _view.HomeView.Hide();
            _view.HeroSelectView.Show();
        }

    }
}