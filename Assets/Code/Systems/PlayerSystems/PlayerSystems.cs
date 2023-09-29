using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public class PlayerSystems
    {
        public PlayerSystems(EcsSystems systems)
        {
            systems
                .Add(new PlayerLoadSystem())
                .Add(new PlayerInitSystem())
                .Add(new PlayerBuildSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerAtackSystem())
                .Add(new PlayerMoveSystem());

        }
    }
}