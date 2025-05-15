using Game.Core;

namespace Game.Events
{
    public class HeroSelectedEvent
    {
        public HeroModel Model;

        public HeroSelectedEvent(HeroModel model)
        {
            Model = model;
        }
    }
}