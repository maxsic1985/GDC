using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using LeoEcsPhysics;
using UnityEngine;


namespace MSuhininTestovoe.B2B
{
    public sealed class EcsStartup : MonoBehaviour
    {
        private EcsSystems _systems;
        private bool _hasInitCompleted;
        [SerializeField] EcsUguiEmitter uguiEmitter;
        [SerializeField] JoystickInputView _joystick; 
        [SerializeField] AttackInputView _attackView;

        private async void Start()
        {
            //
            SharedData shared = new();
            await shared.Init();
            
            shared.GetPlayerSharedData.GetPlayerCharacteristic.LoadInitValue();
            
            IPoolService poolService = new PoolService();
            await poolService.Initialize();

            var world = new EcsWorld();
            _systems = new EcsSystems(world, shared);
            EcsPhysicsEvents.ecsWorld = world;

            new InitializeAllSystem(_systems, poolService);

            _systems
                .AddWorld(new EcsWorld(), WorldsNamesConstants.EVENTS)
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(WorldsNamesConstants.EVENTS))
#endif
               .Inject(_joystick, WorldsNamesConstants.EVENTS)
               .Inject(_attackView, WorldsNamesConstants.EVENTS)
                .InjectUgui(uguiEmitter, WorldsNamesConstants.EVENTS)
                .Init();

            _hasInitCompleted = true;
        }

        private void Update()
        {
            if (_hasInitCompleted)
                _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                foreach (var worlds in _systems.GetAllNamedWorlds())
                {
                    worlds.Value.Destroy();
                }
                EcsPhysicsEvents.ecsWorld = null;
                _systems.GetWorld().Destroy();
                _systems = null;
            }
        }
    }
}