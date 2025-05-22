using System;

namespace Game.UI.Windows.MainMenuSettings
{
    public interface IMainMenuSettingsWindow : IWindow
    {
        event Action OnBackClicked;
    }
}