using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public interface IWindowPresenter
    {
        IWindow Window { get; }
        UniTask InitializeAsync();
        UniTask OnShowAsync();
        UniTask OnHideAsync();
    }
}