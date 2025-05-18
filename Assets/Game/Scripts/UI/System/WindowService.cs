using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public sealed class WindowService
    {
        private readonly DiContainer _container;
        private readonly WindowCatalogue _catalogue;
        private readonly Transform _spawnRoot;

        private readonly Dictionary<WindowId, IWindowPresenter> _cache = new();
        private readonly Stack<IWindowPresenter> _stack = new();

        public WindowService(
            DiContainer container,
            WindowCatalogue catalogue,
            Transform spawnRoot)
        {
            _container = container;
            _catalogue = catalogue;
            _spawnRoot = spawnRoot;
        }

        public async UniTask OpenAsync(WindowId id)
        {
            if (_stack.Count > 0)
                await _stack.Peek().OnHideAsync();

            var presenter = await GetOrCreatePresenterAsync(id);
            _stack.Push(presenter);

            await presenter.OnShowAsync();
        }

        private async UniTask<IWindowPresenter> GetOrCreatePresenterAsync(WindowId id)
        {
            if (_cache.TryGetValue(id, out var ready))
                return ready;

            var cfg = _catalogue.Windows.Find(w => w.Id == id)
                      ?? throw new KeyNotFoundException($"В каталоге нет окна {id}");

            var view = _container.InstantiatePrefabForComponent<WindowBase>(cfg.Prefab, _spawnRoot);
            var presenter = (IWindowPresenter)_container.Instantiate(cfg.PresenterType, new object[] { view });

            await presenter.InitializeAsync();

            if (cfg.IsSingleton)
                _cache[id] = presenter;

            return presenter;
        }
    }
}