using System.Collections.Generic;
using Game.Events;
using UnityEngine;

namespace Game.Core
{
    public class PlayerData : MonoBehaviour
    {
        public static PlayerData Instance { get; private set; }

        [Header("Available Hero Configs")]
        [SerializeField] private List<HeroConfigSO> heroConfigs;

        private List<HeroModel> _heroes = new();
        private HeroModel _selectedHero;

        public IReadOnlyList<HeroModel> Heroes => _heroes;
        public HeroModel SelectedHero => _selectedHero;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadHeroes();
        }

        private void LoadHeroes()
        {
            _heroes.Clear();

            foreach (var config in heroConfigs)
            {
                var model = new HeroModel(config);
                _heroes.Add(model);
            }

            SelectHero(_heroes.Count > 0 ? _heroes[0] : null);
        }

        public void SelectHero(HeroModel hero)
        {
            if (_heroes.Contains(hero))
            {
                _selectedHero = hero;
                EventBus.Publish(new HeroSelectedEvent(hero));
            }
        }
    }
}