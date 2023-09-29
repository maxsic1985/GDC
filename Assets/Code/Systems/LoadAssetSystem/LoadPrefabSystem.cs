using Leopotam.EcsLite;
using LeopotamGroup.Globals;


namespace MSuhininTestovoe.B2B
{

    public sealed class LoadPrefabSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<LoadPrefabComponent> _loadFactoryPrefabComponentPool;
        private GameObjectAssetLoader _gameObjectAssetLoader;
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<LoadPrefabComponent>().End();
            _loadFactoryPrefabComponentPool = world.GetPool<LoadPrefabComponent>();
            _gameObjectAssetLoader = Service<GameObjectAssetLoader>.Get();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref LoadPrefabComponent loadFactoryPrefabComponent = ref _loadFactoryPrefabComponentPool.Get(entity);
                _gameObjectAssetLoader.LoadAsset(loadFactoryPrefabComponent.Value.RuntimeKey, systems.GetWorld(),entity);
                _loadFactoryPrefabComponentPool.Del(entity);
            }
        }
    }
}