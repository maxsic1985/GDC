using Leopotam.EcsLite;



namespace MSuhininTestovoe.B2B
{
    public class DropSystems
    {
        public DropSystems(EcsSystems systems)
        {
            systems
                .Add(new DropCreateSystem());
            
        }
    }
}