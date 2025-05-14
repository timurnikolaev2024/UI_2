using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MenuHeroSelectItemPresenter
    {
        private readonly MenuHeroSelectItemView _view;
        private readonly string _name;
        private readonly Sprite _icon;

        public Sprite Icon => _icon;

        public MenuHeroSelectItemPresenter(
            MenuHeroSelectItemView view,
            string name,
            float progress,
            Action<MenuHeroSelectItemPresenter> onSelected)
        {
            _view = view;
            _name = name;
            _icon = view.GetIcon();

            view.SetName(name);
            view.SetIcon(_icon);
            view.AnimateProgress(progress);
            view.SetSelected(false);
            view.SetClickListener(() => onSelected?.Invoke(this));
        }

        public void SetSelected(bool selected)
        {
            _view.SetSelected(selected);
        }
    }
}