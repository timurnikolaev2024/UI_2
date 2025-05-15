using DefaultNamespace.Info;
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

            UIEventBus.Subscribe<ShowHomeStartedEvent>(ShowHome);
            UIEventBus.Subscribe<ShowSelectHeroStartedEvent>(ShowHeroSelect);
            UIEventBus.Publish(new ShowHomeStartedEvent());
        }

        public void ShowHome(ShowHomeStartedEvent e)
        {
            _view.HomeView.Show();
            _view.HeroSelectView.Hide();
        }

        public void ShowHeroSelect(ShowSelectHeroStartedEvent e)
        {
            _view.HomeView.Hide();
            _view.HeroSelectView.Show();
        }

    }
}