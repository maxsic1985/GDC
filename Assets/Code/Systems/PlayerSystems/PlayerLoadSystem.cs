using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public class PlayerLoadSystem: IEcsInitSystem
    {
        private EcsPool<IsPlayerComponent> _isPlayerPool;

        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isPlayerPool = world.GetPool<IsPlayerComponent>();
            _isPlayerPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.STATIC_PLAYER_DATA;
        }
    }
}