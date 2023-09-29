using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public class SlotsLoadSystem : IEcsInitSystem
    {
        private EcsPool<IsItemComponent> _itemPool;
        
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            LoadSlotData(world);
        }
        
        
        private void LoadSlotData(EcsWorld world)
        {
            var entity = world.NewEntity();

            _itemPool = world.GetPool<IsItemComponent>();
            _itemPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.SLOT;
        }
    }
}