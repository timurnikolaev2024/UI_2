using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.UI
{
    public interface IWindow
    {
        UniTask Show();
        UniTask Hide();
    }
    
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class WindowBase : MonoBehaviour, IWindow
    {
        public abstract UniTask Show();
        public abstract UniTask Hide();
    }
}