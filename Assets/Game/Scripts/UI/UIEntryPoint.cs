using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class UIEntryPoint : MonoBehaviour
    {
        [Inject] private WindowService _windows;

        private async void Start()
        {
            await UniTask.DelayFrame(2);
            await _windows.OpenAsync(WindowId.MainMenu);
        }
    }
}