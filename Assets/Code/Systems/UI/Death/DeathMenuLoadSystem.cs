using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public class DeathMenuLoadSystem : IEcsInitSystem
    {
        private EcsPool<IsDeathMenu> _isDeathMenuPool;

        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isDeathMenuPool = world.GetPool<IsDeathMenu>();
            _isDeathMenuPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.MENU_DEATH;
        }
    }
}