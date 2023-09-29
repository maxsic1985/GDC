using Leopotam.EcsLite;


namespace MSuhininTestovoe.B2B
{
    public class InventoryLoadSystem : IEcsInitSystem
    {
        private EcsPool<IsInventory> _inventoryPool;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            LoadInventoryData(world);
           
        }

        private void LoadInventoryData(EcsWorld world)
        {
            var entity = world.NewEntity();

            _inventoryPool = world.GetPool<IsInventory>();
            _inventoryPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.INVENTORY;
        } 
        
      
    }
}