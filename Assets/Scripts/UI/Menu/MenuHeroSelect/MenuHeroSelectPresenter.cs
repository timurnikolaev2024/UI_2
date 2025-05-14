using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class MenuHeroSelectPresenter
    {
        private readonly MenuHeroSelectView _view;
        private readonly MenuPresenter _menu;
        private readonly List<MenuHeroSelectItemPresenter> _itemPresenters = new();
        private MenuHeroSelectItemPresenter _currentSelected;

        public MenuHeroSelectPresenter(MenuHeroSelectView view, MenuPresenter menu)
        {
            _view = view;
            _menu = menu;

            view.BackButton.onClick.AddListener(menu.ShowHome);

            //create items from data
        }

        private void OnHeroSelected(MenuHeroSelectItemPresenter selected)
        {
            //change selected hero
        }
    }
}