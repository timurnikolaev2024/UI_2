using System;
using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class MenuHeroSelectItemPresenter : IDisposable
    {
        private readonly MenuHeroSelectItemView _view;
        private readonly HeroModel _model;
        private readonly Action<HeroModel> _onSelected;

        public MenuHeroSelectItemPresenter(MenuHeroSelectItemView view, HeroModel model, System.Action<HeroModel> onSelected)
        {
            _view = view;
            _model = model;
            _onSelected = onSelected;

            _view.SetIcon(_model.Config.Icon);
            _view.SetColor(_model.Config.Color);
            _view.OnClicked += HandleClick;
        }

        public void SetSelected(bool isSelected)
        {
            _view.SetSelected(isSelected);
        }

        public void Dispose()
        {
            _view.OnClicked -= HandleClick;
        }

        private void HandleClick()
        {
            _onSelected?.Invoke(_model);
        }
        
        public class Factory :
            PlaceholderFactory<
                HeroModel,
                Action<HeroModel>,
                Transform,
                MenuHeroSelectItemPresenter>
        { }
    }
}