using Leopotam.EcsLite;

namespace MSuhininTestovoe.B2B
{
    public class CameraSystem: IEcsInitSystem
    {
        private EcsPool<IsCameraComponent> _isCameraPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();
            
            _isCameraPool = world.GetPool<IsCameraComponent>();
            _isCameraPool.Add(entity);

            var loadDataByNameComponent = world.GetPool<LoadDataByNameComponent>();
            ref var loadFactoryDataComponent = ref loadDataByNameComponent.Add(entity);
            loadFactoryDataComponent.AddressableName = AssetsNamesConstants.CAMERA_PREFAB_NAME;
        }
    }
}