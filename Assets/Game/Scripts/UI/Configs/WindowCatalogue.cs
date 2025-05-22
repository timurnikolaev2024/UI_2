using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    //легко тестировать наш SO каталог с помощью юнит тестов, не заходя в плеймод
    [CreateAssetMenu(menuName = "UI/Window Catalogue")]
    public class WindowCatalogue : ScriptableObject
    {
        public List<WindowEntry> Windows;
    }
}