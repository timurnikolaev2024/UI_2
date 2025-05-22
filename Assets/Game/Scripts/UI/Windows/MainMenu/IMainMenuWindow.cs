using System;

namespace Game.UI
{
    public interface IMainMenuWindow : IWindow
    {
        event Action PlayClicked;
        event Action OnSettingsClicked;
    }
}