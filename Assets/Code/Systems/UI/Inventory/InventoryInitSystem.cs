using Leopotam.EcsLite;


namespace MSuhininTestovoe.B2B
{
    public class InventoryInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filterInventory;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<IsInventory> _isInventoryPool;
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filterInventory = _world
                .Filter<IsInventory>()
                .Inc<ScriptableObjectComponent>()
                .End();

            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
        }

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filterInventory)
            {
                if (_scriptableObjectPool.Get(entity).Value is InventoryData dataInit)
                {
                    ref var loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.Inventory;
                    
                }
                _scriptableObjectPool.Del(entity);
            }
        }
    }
}