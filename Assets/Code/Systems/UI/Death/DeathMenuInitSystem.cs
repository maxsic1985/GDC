using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public class DeathMenuInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<IsDeathMenu> _isDeathMenuPool;
        
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsDeathMenu>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
        }

        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is UIDeathMenuData dataInit)
                {
                    ref var loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.Menu;
                    
                }
                _scriptableObjectPool.Del(entity);
            }
        }
    }
}