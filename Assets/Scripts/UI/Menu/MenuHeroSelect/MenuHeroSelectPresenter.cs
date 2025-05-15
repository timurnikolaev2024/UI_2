using System.Collections.Generic;
using DefaultNamespace.Info;
using UnityEngine;

namespace DefaultNamespace
{
    public class MenuHeroSelectPresenter
    {
        private readonly MenuHeroSelectView _view;
        private readonly MenuPresenter _menu;
        private readonly List<MenuHeroSelectItemPresenter> _itemPresenters = new();
        private MenuHeroSelectItemPresenter _currentSelected;
        private Dictionary<byte, MenuHeroSelectItemPresenter> _items = new();

        public MenuHeroSelectPresenter(MenuHeroSelectView view, MenuPresenter menu)
        {
            _view = view;
            _view.BackButton.onClick.AddListener(() =>
            {
                UIEventBus.Publish(new ShowHomeStartedEvent());
            });
            _menu = menu;

            var q = PlayerData.Instance.Heroes;
            foreach (var heroModel in PlayerData.Instance.Heroes)
            {
                var itemView = GameObject.Instantiate(view.ItemPrefab, view.ItemsContainer);
                var presenter = new MenuHeroSelectItemPresenter(itemView, heroModel);
                _items.Add(heroModel.Config.id, presenter);
                _itemPresenters.Add(presenter);
            }
            
            OnHeroSelected(_items[PlayerData.Instance.SelectedHero.Config.id]);
            UIEventBus.Subscribe<HeroSelectedEvent>(OnHeroSelectedD);
        }

        private void OnHeroSelectedD(HeroSelectedEvent e)
        {
            OnHeroSelected(_items[PlayerData.Instance.SelectedHero.Config.id]);
        }

        private void OnHeroSelected(MenuHeroSelectItemPresenter selected)
        {
            if (_currentSelected != null && _currentSelected != selected)
                _currentSelected.SetSelected(false);

            _currentSelected = selected;
            _currentSelected.SetSelected(true);
        }
    }
}