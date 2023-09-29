using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace MSuhininTestovoe.B2B
{
    public class DEBUG_DeathSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<IsPlayerDeathComponent> _isPlayerDeathPool;
        private EcsPool<IsRestartComponent> _isRestartPool;
        private EcsFilter _filter;
        private PlayerSharedData _sharedData;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;
            _filter = world.Filter<IsPlayerDeathComponent>().End();
            _isPlayerDeathPool = world.GetPool<IsPlayerDeathComponent>();
            _isRestartPool = world.GetPool<IsRestartComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    _isPlayerDeathPool.Del(entity);
                    _isRestartPool.Add(entity);
                }
            }
        }
    }
}