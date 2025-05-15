using Game.Core;
using Game.Events;

namespace DefaultNamespace
{
    public class MenuHomePresenter
    {
        private MenuHomeView _view;
        public MenuHomePresenter(MenuHomeView view, MenuPresenter menu)
        {
            _view = view;
            _view.ChooseHeroButton.onClick.AddListener(OnChooseHeroButtonClick);
        }

        void OnChooseHeroButtonClick()
        {
            EventBus.Publish(new ShowSelectHeroStartedEvent());
        }

        public void Dispose()
        {
            _view.ChooseHeroButton.onClick.RemoveListener(OnChooseHeroButtonClick);
        }
    }
}