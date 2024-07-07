using Leopotam.EcsLite;
using UnityEngine;


namespace MSuhininTestovoe.B2B
{
    sealed class SoundCatchSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<SoundEffectsSourceComponent> _soundEffectsSourceComponentPool;
        private EcsPool<IsPlaySoundComponent> _isPlaySoundComponentPool;
        public static int sounEffectsSourceEntity;
        


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<IsPlaySoundComponent>().End();
            _soundEffectsSourceComponentPool = world.GetPool<SoundEffectsSourceComponent>();
            _isPlaySoundComponentPool = world.GetPool<IsPlaySoundComponent>();
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var soundEffectsSourceComponent = ref _soundEffectsSourceComponentPool.Get(sounEffectsSourceEntity);
                var audioSource = soundEffectsSourceComponent.Source;

                var isSoundFromTriggerComponent = _isPlaySoundComponentPool.Get(entity);
                audioSource.Play();
                audioSource.PlayOneShot(soundEffectsSourceComponent.Tracks[(int)isSoundFromTriggerComponent.SoundType]);

                _isPlaySoundComponentPool.Del(entity);
            }
        }


    }
}