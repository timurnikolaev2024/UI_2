using System;
using System.Collections.Generic;
using System.Linq;
using Game.UI;
using Game.UI.TypeResolver;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class WindowPresenterResolutionTests
    {
        private WindowCatalogue _catalogue;
        private BasicTypeResolver _resolver;

        [SetUp]
        public void Setup()
        {
            _catalogue = Resources.Load<WindowCatalogue>("WindowCatalogue");
            _resolver = new BasicTypeResolver();
        }
        
        [Test]
        public void AllPresentersCanBeResolved()
        {
            Assert.IsNotNull(_catalogue, "Каталог окон не найден!");

            var failed = new List<string>();
        
            foreach (var window in _catalogue.Windows)
            {
                try
                {
                    var type = _resolver.Resolve(window.PresenterName);
                    Assert.IsNotNull(type, $"Презентер не найден: {window.PresenterName}");
                }
                catch (Exception e)
                {
                    Debug.LogError($"Ошибка при разрешении {window.PresenterName}: {e.Message}");
                    failed.Add(window.PresenterName);
                }
            }
        
            if (failed.Count > 0)
            {
                Assert.Fail($"Не удалось разрешить следующие презентеры: {string.Join(", ", failed)}");
            }
        }
        
        [Test]
        public void AllWindowIdsAreUnique()
        {
            Assert.IsNotNull(_catalogue, "Каталог окон не найден!");

            var ids = _catalogue.Windows.Select(w => w.Id);
            var duplicates = ids.GroupBy(id => id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicates.Count > 0)
            {
                Assert.Fail($"Обнаружены дубликаты WindowId: {string.Join(", ", duplicates)}");
            }
        }
    }
}