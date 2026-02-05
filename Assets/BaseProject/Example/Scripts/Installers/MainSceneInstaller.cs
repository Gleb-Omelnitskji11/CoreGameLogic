using System.Collections.Generic;
using BaseProject.Example.Scripts.Gameplay;
using BaseProject.Example.Scripts.Monetization;
using BaseProject.Scripts.Core.GameState;
using Game.Gameplay;
using UnityEngine;
using Zenject;

namespace BaseProject.Example.Scripts.Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameplayType _gameplayType = GameplayType.Clicker;
        [SerializeField] private ClickOptionsConfig _clickOptionsConfig;

        public override void InstallBindings()
        {
            ResolverDependency();
            Container.Bind<GameStateManager>().AsCached().NonLazy();
            

            switch (_gameplayType)
            {
                case GameplayType.Clicker:
                    Container.Bind<BaseGameplayController>().To<ClickerGameplayController>()
                        .AsCached()
                        .WithArguments(_clickOptionsConfig)
                        .NonLazy();
                    break;
                case GameplayType.Timer:
                case GameplayType.Swipe:
                    throw new KeyNotFoundException($"Gameplay type {_gameplayType} is not supported");
                    break;
            }
        }

        private void ResolverDependency()
        {
            var adapter = StaticContext.Container.Resolve<RewardAdapter>();
        }
        
        enum GameplayType
        {
            Clicker,
            Timer,
            Swipe
        }

    }
}