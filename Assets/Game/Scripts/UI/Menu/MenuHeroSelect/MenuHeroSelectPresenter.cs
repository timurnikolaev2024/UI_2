using System.Collections.Generic;
using DefaultNamespace.Info;
using Game.Core;
using Game.Events;
using UnityEngine;

namespace DefaultNamespace
{
    public class MenuHeroSelectPresenter
    {
        private readonly MenuHeroSelectView _view;
        private readonly List<MenuHeroSelectItemPresenter> _itemPresenters = new();
        private MenuHeroSelectItemPresenter _currentSelected;
        private Dictionary<byte, MenuHeroSelectItemPresenter> _items = new();

        public MenuHeroSelectPresenter(MenuHeroSelectView view)
        {
            _view = view;
            _view.BackButton.onClick.AddListener(OnBackButtonClicked);
            CreateHeroItems();
            SelectHeroItem(_items[PlayerData.Instance.SelectedHero.Config.id]);
            EventBus.Subscribe<HeroSelectedEvent>(OnHeroSelected);
        }

        private void CreateHeroItems()
        {
            foreach (var heroModel in PlayerData.Instance.Heroes)
            {
                var itemView = GameObject.Instantiate(_view.ItemPrefab, _view.ItemsContainer);
                var presenter = new MenuHeroSelectItemPresenter(itemView, heroModel);
                _items.Add(heroModel.Config.id, presenter);
                _itemPresenters.Add(presenter);
            }
        }

        private void OnHeroSelected(HeroSelectedEvent e)
        {
            SelectHeroItem(_items[PlayerData.Instance.SelectedHero.Config.id]);
        }

        private void OnBackButtonClicked()
        {
            EventBus.Publish(new ShowHomeStartedEvent());
        }

        private void SelectHeroItem(MenuHeroSelectItemPresenter selected)
        {
            if (_currentSelected != null && _currentSelected != selected)
                _currentSelected.SetSelected(false);

            _currentSelected = selected;
            _currentSelected.SetSelected(true);
        }

        public void Dispose()
        {
            EventBus.Unsubscribe<HeroSelectedEvent>(OnHeroSelected);
            _view.BackButton.onClick.RemoveListener(OnBackButtonClicked);

            foreach (var item in _itemPresenters)
            {
                item.Dispose();   
            }
        }
    }
}