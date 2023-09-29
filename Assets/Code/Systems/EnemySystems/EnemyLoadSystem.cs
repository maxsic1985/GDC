using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public class EnemyLoadSystem: IEcsInitSystem
    {
        private EcsPool<IsEnemyComponent> _isEnemyPool;

        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isEnemyPool = world.GetPool<IsEnemyComponent>();
            _isEnemyPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.ENEMY_DATA;
        }
    }
}