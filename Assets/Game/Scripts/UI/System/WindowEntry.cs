using System;
using System.Linq;

namespace Game.UI
{
    public enum WindowId { MainMenu, HeroSelect, Settings }

    [Serializable]
    public class WindowEntry
    {
        public WindowId Id;
        
        public WindowBase Prefab;

        public string PresenterName;
    }
}