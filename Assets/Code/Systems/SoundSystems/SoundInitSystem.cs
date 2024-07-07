using Leopotam.EcsLite;
using UnityEngine;

namespace MSuhininTestovoe.B2B
{


    public class SoundInitSystem: IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsWorld _world;
        private EcsPool<ScriptableObjectComponent> _scriptableObjectPool;
        private EcsPool<LoadPrefabComponent> _loadPrefabPool;
        private EcsPool<SoundStartPositionComponent> _soundStartPositionComponentPool;
        private EcsPool<SoundMusicSourceComponent> _soundMusicSourceComponentPool;
        private EcsPool<SoundEffectsSourceComponent> _soundEffectsSourceComponentPool;
        private EcsPool<IsSoundComponent> _isSoundComponentPool;


        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<IsSoundComponent>().Inc<ScriptableObjectComponent>().End();
            _scriptableObjectPool = _world.GetPool<ScriptableObjectComponent>();
            _loadPrefabPool = _world.GetPool<LoadPrefabComponent>();
            _soundStartPositionComponentPool = _world.GetPool<SoundStartPositionComponent>();
            _soundMusicSourceComponentPool = _world.GetPool<SoundMusicSourceComponent>();
            _soundEffectsSourceComponentPool = _world.GetPool<SoundEffectsSourceComponent>();
            _isSoundComponentPool = _world.GetPool<IsSoundComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            
            foreach (var entity in _filter)
            {
                if (_scriptableObjectPool.Get(entity).Value is SoundLoadData dataInit)
                {
                    ref var isSoundComponent = ref _isSoundComponentPool.Get(entity);
                    ref var loadPrefabFromPool = ref _loadPrefabPool.Add(entity);
                    ref var soundStartPositionComponent = ref _soundStartPositionComponentPool.Add(entity);

                    soundStartPositionComponent.Value = dataInit.StartPosition;
                    loadPrefabFromPool.Value = dataInit.Source;


                    if (isSoundComponent.Type == "Music")
                    {
                        ref var soundMusicSourceComponent = ref _soundMusicSourceComponentPool.Add(entity);
                        soundMusicSourceComponent.Tracks = dataInit.Tracks;
                        soundMusicSourceComponent.PlayedTrack = dataInit.FirstTrackNumber;
                        soundMusicSourceComponent.FirstTrackNumber = dataInit.FirstTrackNumber;

                    }
                    else if(isSoundComponent.Type == "Effects")
                    {
                        ref var soundEffectSourceComponent = ref _soundEffectsSourceComponentPool.Add(entity);
                        soundEffectSourceComponent.Tracks = dataInit.Tracks;
                        SoundCatchSystem.sounEffectsSourceEntity = entity;
                     
                    }
                }
                _scriptableObjectPool.Del(entity);
            }
        }
    }
}