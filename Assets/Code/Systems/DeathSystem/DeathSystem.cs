using Leopotam.EcsLite;
using LeopotamGroup.Globals;


namespace MSuhininTestovoe.B2B
{
    public class DeathSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<IsPlayerDeathComponent> _isPlayerDeathPool;
        private EcsPool<IsDeathMenu> _isDeathMenuActive;
        private PlayerSharedData _sharedData;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _sharedData = systems.GetShared<SharedData>().GetPlayerSharedData;

            _filter = world
                .Filter<IsPlayerComponent>()
                .End();
            _isPlayerDeathPool = world.GetPool<IsPlayerDeathComponent>();
            _isDeathMenuActive = world.GetPool<IsDeathMenu>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_sharedData.GetPlayerCharacteristic.GetLives.GetCurrrentLives <= 0)
                {
                    if (!_isPlayerDeathPool.Has(entity))
                    {
                        ref var deathComponent = ref _isPlayerDeathPool.Add(entity);
                    }

                    if (!_isDeathMenuActive.Has(entity)) return;
                    ref var showMenu = ref _isDeathMenuActive.Get(entity);
                    var timeServise = Service<ITimeService>.Get();
                    timeServise.Pause();
                }
            }
        }
    }
}