using Leopotam.EcsLite;

namespace MSuhininTestovoe.B2B
{
    public sealed class CameraSystems
    {
        public CameraSystems(EcsSystems systems)
        {
            systems
                .Add(new CameraSystem())
                .Add(new CameraInitSystem())
                .Add(new CameraBuildSystem())
                .Add(new CameraFollowSystem());
        }
    }
}