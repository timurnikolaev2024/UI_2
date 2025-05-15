namespace DefaultNamespace
{
    public class HeroModel
    {
        public HeroConfigSO Config { get; }
        public HeroRuntimeData Runtime { get; }

        public HeroModel(HeroConfigSO config)
        {
            Config = config;
            Runtime = new HeroRuntimeData
            {
                currentLevel = 1,
                currentHealth = config.maxHealth,
                currentAttack = config.maxAttack
            };
        }
    }
}