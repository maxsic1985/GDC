using LeoEcsPhysics;
using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public class TriggerSystems
    {
        public TriggerSystems(EcsSystems systems)
        {
            systems
                .Add(new TriggerSystem())
                .DelHerePhysics();
        }
    }
}