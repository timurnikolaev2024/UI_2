using DefaultNamespace.Info;
using Game.Core;
using Game.Events;
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
            _heroSelectPresenter = new MenuHeroSelectPresenter(view.HeroSelectView);

            EventBus.Subscribe<ShowHomeStartedEvent>(ShowHome);
            EventBus.Subscribe<ShowSelectHeroStartedEvent>(ShowHeroSelect);
            EventBus.Publish(new ShowHomeStartedEvent());
        }

        private void ShowHome(ShowHomeStartedEvent e)
        {
            _view.HomeView.Show();
            _view.HeroSelectView.Hide();
        }

        private void ShowHeroSelect(ShowSelectHeroStartedEvent e)
        {
            _view.HomeView.Hide();
            _view.HeroSelectView.Show();
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<ShowHomeStartedEvent>(ShowHome);
            EventBus.Unsubscribe<ShowSelectHeroStartedEvent>(ShowHeroSelect);
            _homePresenter.Dispose();
            _heroSelectPresenter.Dispose();
        }
    }
}