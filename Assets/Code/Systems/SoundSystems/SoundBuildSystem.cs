using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhininTestovoe.B2B
{
    public class SoundBuildSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PrefabComponent> _prefabPool;
        private EcsPool<TransformComponent> _transformComponentPool;
        private EcsPool<SoundStartPositionComponent> _soundStartPositionComponentPool;
        private EcsPool<SoundMusicSourceComponent> _soundMusicSourceComponentPool;
        private EcsPool<SoundEffectsSourceComponent> _soundEffectsSourceComponentPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<IsSoundComponent>().Inc<PrefabComponent>().End();
            _prefabPool = world.GetPool<PrefabComponent>();
            _transformComponentPool = world.GetPool<TransformComponent>();
            _soundStartPositionComponentPool = world.GetPool<SoundStartPositionComponent>();
            _soundMusicSourceComponentPool = world.GetPool<SoundMusicSourceComponent>();
            _soundEffectsSourceComponentPool = world.GetPool<SoundEffectsSourceComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var prefabComponent = ref _prefabPool.Get(entity);
                ref var transformComponent = ref _transformComponentPool.Add(entity);
                ref var soundPositionComponent = ref _soundStartPositionComponentPool.Get(entity);

                var gameObject = Object.Instantiate(prefabComponent.Value);
                var audioSource = gameObject.GetComponent<AudioSource>();
                transformComponent.Value = gameObject.GetComponent<TransformView>().Transform;
                gameObject.transform.position = soundPositionComponent.Value;

                if (_soundMusicSourceComponentPool.Has(entity))
                {
                    ref var soundMusicSourceComponent = ref _soundMusicSourceComponentPool.Get(entity);
                    soundMusicSourceComponent.Source = audioSource;
                    audioSource.clip = soundMusicSourceComponent.Tracks[soundMusicSourceComponent.PlayedTrack];
                    audioSource.Play();
                }
                else if (_soundEffectsSourceComponentPool.Has(entity))
                {
                    ref var soundEffectsSourceComponent = ref _soundEffectsSourceComponentPool.Get(entity);
                    soundEffectsSourceComponent.Source = audioSource;
                }

                _prefabPool.Del(entity);
            }
        }
    }
}