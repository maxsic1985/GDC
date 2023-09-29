using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;


namespace  MSuhininTestovoe.B2B
{
    public sealed class InitializeServiceSystem : IEcsInitSystem
    {
        private readonly IPoolService _poolService;
    

        public InitializeServiceSystem(IPoolService poolService)
        {
            _poolService = poolService;
        }

        public void Init(IEcsSystems systems)
        {
            Service<ITimeService>.Set(new UnityTimeService());
            Service<GameObjectAssetLoader>.Set(new GameObjectAssetLoader());
            Service<ScriptableObjectAssetLoader>.Set(new ScriptableObjectAssetLoader());
            Service<IPoolService>.Set(_poolService);
          
        }
    }
}
