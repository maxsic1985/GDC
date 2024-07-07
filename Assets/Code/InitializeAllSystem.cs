using LeoEcsPhysics;
using Leopotam.EcsLite;

namespace MSuhininTestovoe.B2B
{
    internal class InitializeAllSystem
    {
        public InitializeAllSystem(EcsSystems systems, IPoolService poolService)
        {
            systems
                .Add(new InitializeServiceSystem(poolService))
                .Add(new LoadPrefabSystem())
                .Add(new LoadDataByNameSystem());

            new CommonSystems(systems);
            new TriggerSystems(systems);
            new PlayerSystems(systems);
            new EnemySystems(systems);
            new DropSystems(systems);
            new MenuSystems(systems);
            new DeathSystems(systems);
          
            
            new CameraSystems(systems);
            new SoundSystems(systems);
        }
    }
}