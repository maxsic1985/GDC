using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhininTestovoe.B2B
{
    public class CameraInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<CameraStartPositionComponent> _cameraStartPositionComponentPool;
        private EcsPool<CameraStartRotationComponent> _cameraStartRotationComponentPool;
        private EcsPool<IsCameraComponent> _isCameraComponentPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsCameraComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _cameraStartPositionComponentPool = _world.GetPool<CameraStartPositionComponent>();
            _cameraStartRotationComponentPool = _world.GetPool<CameraStartRotationComponent>();
            _isCameraComponentPool = _world.GetPool<IsCameraComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is CameraLoadData dataInit)
                {
                    ref LoadPrefabComponent loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    loadPrefabFromPool.Value = dataInit.Camera;

                    ref CameraStartPositionComponent cameraStartPositionComponent =
                        ref _cameraStartPositionComponentPool.Add(entity);
                    cameraStartPositionComponent.Value = dataInit.StartPosition;

                    ref CameraStartRotationComponent cameraStartRotationComponent =
                        ref _cameraStartRotationComponentPool.Add(entity);
                    cameraStartRotationComponent.Value = dataInit.StartRotation;

                    ref IsCameraComponent isCameraComponent = ref _isCameraComponentPool.Get(entity);
                    isCameraComponent.CameraSmoothness = dataInit.CameraSmoothness;
                    isCameraComponent.Offset = dataInit.StartPosition;
                    isCameraComponent.CurrentVelocity = Vector3.zero;
                }

                _scriptableObjectPool.Del(entity);
            }
        }
    }
}