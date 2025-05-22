using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.UI.TypeResolver;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public sealed class WindowService
    {
        private readonly DiContainer _container;
        private readonly WindowCatalogue _catalogue;
        private readonly Transform _spawnRoot;
        private readonly ITypeResolver _typeResolver;

        private readonly Dictionary<WindowId, IWindowPresenter> _cache = new();
        private readonly Stack<IWindowPresenter> _stack = new();

        public WindowService(
            DiContainer container,
            WindowCatalogue catalogue,
            Transform spawnRoot,
            ITypeResolver typeResolver)
        {
            _container = container;
            _catalogue = catalogue;
            _spawnRoot = spawnRoot;
            _typeResolver = typeResolver;
        }
        
        public async UniTask OpenAsync(WindowId id)
        {
            var presenter = await GetOrCreatePresenterAsync(id);

            if (_stack.Count > 0 && ReferenceEquals(_stack.Peek(), presenter))
                return;

            if (_stack.Count > 0)
                await _stack.Peek().OnHideAsync();

            if (_stack.Contains(presenter))
            {
                while (!ReferenceEquals(_stack.Peek(), presenter))
                    _stack.Pop();
            }
            else
            {
                _stack.Push(presenter);
            }

            await presenter.OnShowAsync();
        }

        public async UniTask CloseCurrentAsync()
        {
            if (_stack.Count == 0)
                return;

            var top = _stack.Pop();
            await top.OnHideAsync();

            if (_stack.Count > 0)
                await _stack.Peek().OnShowAsync();
        }
        
        public async UniTask ReplaceAsync(WindowId id)
        {
            if (_stack.Count > 0)
            {
                var old = _stack.Pop();
                await old.OnHideAsync();
            }

            var presenter = await GetOrCreatePresenterAsync(id);
            _stack.Push(presenter);
            await presenter.OnShowAsync();
        }
        
        public async UniTask CloseAllAsync()
        {
            while (_stack.Count > 0)
            {
                var wnd = _stack.Pop();
                await wnd.OnHideAsync();
            }
        }
        
        private async UniTask<IWindowPresenter> GetOrCreatePresenterAsync(WindowId id)
        {
            if (_cache.TryGetValue(id, out var ready))
                return ready;

            var cfg = _catalogue.Windows.Find(w => w.Id == id)
                      ?? throw new KeyNotFoundException($"В каталоге нет окна {id}");

            var view = _container.InstantiatePrefabForComponent<WindowBase>(cfg.Prefab, _spawnRoot);
            
            var presenterType = _typeResolver.Resolve(cfg.PresenterName);
            var presenter = (IWindowPresenter)_container.Instantiate(presenterType, new object[] { view });

            await presenter.InitializeAsync();

            _cache[id] = presenter;
            return presenter;
        }
    }
}