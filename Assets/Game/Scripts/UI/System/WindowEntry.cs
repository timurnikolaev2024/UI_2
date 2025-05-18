using System;
using System.Linq;

namespace Game.UI
{
    public enum WindowId { MainMenu, HeroSelect, Settings }

    [Serializable]
    public class WindowEntry
    {
        public WindowId Id;
        
        public WindowBase Prefab;

        public string PresenterName;

        public bool IsSingleton;

        public Type PresenterType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PresenterName))
                    throw new Exception($"PresenterName пуст у окна {Id}");

                var type = Type.GetType(PresenterName, throwOnError: false);
                if (type != null)
                    return type;

                var allTypes = System.Reflection.Assembly
                    .GetExecutingAssembly()
                    .GetTypes();

                type = allTypes.FirstOrDefault(t => t.FullName == PresenterName);

                if (type == null)
                    type = allTypes.FirstOrDefault(t => t.Name == PresenterName);

                if (type == null)
                    throw new Exception(
                        $"Не найден класс {PresenterName} для окна {Id}. " +
                        $"Убедись, что указано полное имя типа или AssemblyQualifiedName");

                return type;
            }
        }
    }
}