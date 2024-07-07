using System;
using Leopotam.EcsLite;
using UnityEngine;


namespace MSuhininTestovoe.B2B
{
    public class SoundMusicSwitchSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<SoundMusicSourceComponent> _soundMusicSourceComponentPool;
        private EcsPool<IsSwitchMusicComponent> _isSwitchMusicComponentPool;
        private EcsPool<IsStopMusicComponent> _isStopMusicComponentPool;
        private EcsFilter _switchFilter, _stopFilter;
        public static int musicSourceEntity;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _switchFilter = world.Filter<IsSwitchMusicComponent>().End();
            _stopFilter = world.Filter<IsStopMusicComponent>().End();
            _isSwitchMusicComponentPool = world.GetPool<IsSwitchMusicComponent>();
            _isStopMusicComponentPool = world.GetPool<IsStopMusicComponent>();
            _soundMusicSourceComponentPool = world.GetPool<SoundMusicSourceComponent>();
         
        }


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _switchFilter)
            {
                PlayNextTrack();

                _isSwitchMusicComponentPool.Del(entity);
            }

            foreach (var entity in _stopFilter)
            { 
                _soundMusicSourceComponentPool.Get(musicSourceEntity).Source.Stop();
                _isStopMusicComponentPool.Del(entity);
            }
        }

        private void PlayNextTrack()
        {
            ref var soundSwitching = ref _soundMusicSourceComponentPool.Get(musicSourceEntity);
            var audioSource = soundSwitching.Source;

            soundSwitching.PlayedTrack++;
            if (soundSwitching.PlayedTrack >= soundSwitching.Tracks.Length)
            {
                soundSwitching.PlayedTrack = 0;
            }

            audioSource.clip = soundSwitching.Tracks[soundSwitching.PlayedTrack];
            audioSource.Play();
        }

        private void ResetTrack(IEcsSystems ecsSystem)
        {
            ref var soundSwitching = ref ecsSystem
                .GetWorld()
                .GetPool<SoundMusicSourceComponent>()
                .Get(musicSourceEntity);
            var audioSource = soundSwitching.Source;
            soundSwitching.PlayedTrack =soundSwitching.FirstTrackNumber;
            audioSource.clip = soundSwitching.Tracks[soundSwitching.PlayedTrack];
            audioSource.Play();
        }
    }
}