namespace Game.Core
{
    public class HeroModel
    {
        public HeroConfigSO Config { get; }

        public HeroModel(HeroConfigSO config)
        {
            Config = config;
        }
    }
}