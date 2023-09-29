using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public sealed class SynchronizeTransformAndPositionSystem: IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<PositionComponent> _positionComponentPool;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<TransformComponent>().Inc<PositionComponent>().End();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _positionComponentPool = world.GetPool<PositionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var transformComponent = ref _transformComponentPool.Get(entity);
                ref var positionComponent = ref _positionComponentPool.Get(entity);
                transformComponent.Value.localPosition = positionComponent.Value;
            }
        }
    }
}