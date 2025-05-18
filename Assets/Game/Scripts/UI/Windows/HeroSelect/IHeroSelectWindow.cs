using System;
using UnityEngine;

namespace Game.UI
{
    public interface IHeroSelectWindow : IWindow
    {
        HeroProgressWidgetView ProgressWidget { get; }
        HeroImageWidgetView ImageWidget { get; }
        event Action BackClicked;
        Transform ItemsRoot { get; }
    }
}