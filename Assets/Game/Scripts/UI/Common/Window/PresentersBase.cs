using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public abstract class PresenterBase<TView> : IWindowPresenter
        where TView : IWindow
    {
        protected TView Window { get; }
        IWindow IWindowPresenter.Window => Window;

        protected PresenterBase(TView view) => Window = view;

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