using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Game.Extensions
{
    public static class DOTweenUniTaskExtensions
    {
        public static UniTask ToUniTask(this Tween tween)
        {
            var tcs = new UniTaskCompletionSource();

            if (tween == null || !tween.active || tween.IsComplete())
            {
                tcs.TrySetResult();
            }
            else
            {
                tween.OnComplete(() => tcs.TrySetResult());
                tween.OnKill(() => tcs.TrySetResult());
            }

            return tcs.Task;
        }
    }
}