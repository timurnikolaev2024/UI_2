using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Game.UI.TypeResolver
{
    public sealed class BasicTypeResolver : ITypeResolver
    {
        private readonly Dictionary<string, Type> _cache = new();
        private readonly Assembly _assemblyToSearch = typeof(IWindowPresenter).Assembly;

        public Type Resolve(string presenterName)
        {
            if (string.IsNullOrWhiteSpace(presenterName))
                throw new TypeLoadException("Имя презентера пустое!");

            if (_cache.TryGetValue(presenterName, out var ready))
                return ready;

            var type = Type.GetType(presenterName, throwOnError: false);

            if (type == null)
            {
                type = _assemblyToSearch.GetTypes().FirstOrDefault(t =>
                    t.FullName == presenterName || t.Name == presenterName);
            }

            if (type == null || !typeof(IWindowPresenter).IsAssignableFrom(type))
                throw new TypeLoadException($"Презентер '{presenterName}' не найден");

            _cache[presenterName] = type;
            return type;
        }
    }
}