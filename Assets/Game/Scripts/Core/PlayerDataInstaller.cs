using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Core
{
    public class PlayerDataInstaller : MonoInstaller
    {
        [Header("Hero Configs placed here")]
        [SerializeField] private List<HeroConfigSO> heroConfigs;

        public override void InstallBindings()
        {
            Container
                .Bind<PlayerData>()
                .FromMethod(_ => new PlayerData(heroConfigs))
                .AsSingle()
                .NonLazy();
        }
    }
}