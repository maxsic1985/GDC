using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public class WorldCreator
    {
        private readonly EcsSystems _systems;
        public WorldCreator(EcsSystems systems)
        {
            _systems = systems;
        }

        public WorldCreator AddNewWorld(string worldName)
        {
            var world = new EcsWorld();
            _systems.AddWorld(world, worldName);
            return this;
        }
    }
}