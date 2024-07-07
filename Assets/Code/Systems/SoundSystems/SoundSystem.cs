using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhininTestovoe.B2B
{
    public class SoundSystem: IEcsInitSystem
    {


        public void Init(IEcsSystems systems)
        {
            AddSoundSourceEntity(systems, "Music", AssetsNamesConstants.MUSIC_LOAD_DATA);
            AddSoundSourceEntity(systems, "Effects", AssetsNamesConstants.SOUNDEFFECTS_LOAD_DATA);
        }

        public void AddSoundSourceEntity(IEcsSystems systems, string sourceType, string assetNameConstatn)
        {
            var world = systems.GetWorld();
            var entity = world.NewEntity();

            ref var isSoundComponent = ref world.GetPool<IsSoundComponent>().Add(entity);
            isSoundComponent.Type = sourceType;

            ref var loadFactoryDataComponent = ref world.GetPool<LoadDataByNameComponent>().Add(entity);
            loadFactoryDataComponent.AddressableName = assetNameConstatn;

        }
    }
}