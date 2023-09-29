using Leopotam.EcsLite;


namespace MSuhininTestovoe.B2B
{
    public class DeathSystems
    {
        public DeathSystems(EcsSystems systems)
        {
            systems
                .Add(new DeathSystem());
        }
    }
}