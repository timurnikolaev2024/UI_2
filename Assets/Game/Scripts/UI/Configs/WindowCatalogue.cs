using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "UI/Window Catalogue")]
    public class WindowCatalogue : ScriptableObject
    {
        public List<WindowEntry> Windows;
    }
}