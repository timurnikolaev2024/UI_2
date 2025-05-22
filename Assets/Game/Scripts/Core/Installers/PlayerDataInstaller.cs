using Game.Core;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class PlayerDataInstaller : MonoInstaller
    {
        [SerializeField] private HeroCatalogue _heroCatalogue;

        public override void InstallBindings()
        {
            Container
                .Bind<PlayerData>()
                .FromMethod(_ => new PlayerData(_heroCatalogue.Heroes))
                .AsSingle()
                .NonLazy();
        }
    }
}