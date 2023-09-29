using Leopotam.EcsLite;
using LeopotamGroup.Globals;


namespace MSuhininTestovoe.B2B
{
    public sealed class LoadDataByNameSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadDataByNameComponent> _loadDataByNameComponentPool;
        private ScriptableObjectAssetLoader _scriptableObjectAssetLoader;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<LoadDataByNameComponent>().End();
            _loadDataByNameComponentPool = world.GetPool<LoadDataByNameComponent>();
            _scriptableObjectAssetLoader = Service<ScriptableObjectAssetLoader>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref LoadDataByNameComponent loadDataByNameComponent = ref _loadDataByNameComponentPool.Get(entity);
                _scriptableObjectAssetLoader.LoadAsset(loadDataByNameComponent.AddressableName, systems.GetWorld(),
                    entity);
                _loadDataByNameComponentPool.Del(entity);
            }
        }
    }
}