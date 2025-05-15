namespace DefaultNamespace
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