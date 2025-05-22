using System;
using System.Linq;
using Game.UI;
using Game.UI.TypeResolver;
using NUnit.Framework;
using UnityEngine;

namespace Tests.UI
{
    public class WindowCatalogue_ValidationTests
    {
        private WindowCatalogue _catalogue;
        private BasicTypeResolver _resolver;

        [SetUp]
        public void Setup()
        {
            _catalogue = Resources.Load<WindowCatalogue>("WindowCatalogue");
            Assert.IsNotNull(_catalogue, "Не найден WindowCatalogue в Resources");

            _resolver = new BasicTypeResolver();
        }

        [Test]
        public void AllPresenterNamesResolveToValidTypes()
        {
            foreach (var entry in _catalogue.Windows)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(entry.PresenterName),
                    $"У окна {entry.Id} пустое имя презентера");

                Type type = null;

                try
                {
                    type = _resolver.Resolve(entry.PresenterName);
                }
                catch (Exception ex)
                {
                    Assert.Fail($"Не удалось разрешить тип {entry.PresenterName}: {ex.Message}");
                }

                Assert.IsTrue(typeof(IWindowPresenter).IsAssignableFrom(type),
                    $"Тип {type} не реализует IWindowPresenter");
            }
        }

        [Test]
        public void AllWindowIdsAreUnique()
        {
            var duplicates = _catalogue.Windows
                .GroupBy(w => w.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            Assert.IsEmpty(duplicates,
                $"В каталоге повторяются WindowId: {string.Join(", ", duplicates)}");
        }
    }
}