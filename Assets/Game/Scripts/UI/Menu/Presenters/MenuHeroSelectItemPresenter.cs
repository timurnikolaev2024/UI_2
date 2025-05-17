using System;
using Game.Core;
using Game.UI.Menu.Views;
using Zenject;

namespace Game.UI.Menu.Presenters
{
    public class MenuHeroSelectItemPresenter : IDisposable
    {
        private readonly MenuHeroSelectItemView _view;
        private readonly HeroModel _model;
        private readonly Action<HeroModel> _onSelected;

        public MenuHeroSelectItemPresenter(MenuHeroSelectItemView view, HeroModel model, Action<HeroModel> onSelected)
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
        
        public class Factory
            : PlaceholderFactory<HeroModel, Action<HeroModel>, MenuHeroSelectItemPresenter> {}
    }
}