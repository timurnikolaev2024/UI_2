using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public abstract class WindowPresenterBase<TView> : IWindowPresenter
        where TView : IWindow
    {
        protected TView Window { get; }
        IWindow IWindowPresenter.Window => Window;

        protected WindowPresenterBase(TView view) => Window = view;

        public virtual UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask OnShowAsync()
        {
            return Window.Show();
        }

        public virtual UniTask OnHideAsync()
        {
            return Window.Hide();
        }
    }
}