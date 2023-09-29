using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public class EnemySystems
    {
        public EnemySystems(EcsSystems systems)
        {
            systems
                .Add(new EnemyLoadSystem())
                .Add(new EnemyInitSystem())
                .Add(new EnemyAtackSystem())
                .Add(new EnemyDeathSystem());

        }
    }
}