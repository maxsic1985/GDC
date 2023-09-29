using System;
using UnityEngine;


namespace MSuhininTestovoe.B2B
{
    [Serializable]
    public sealed class PlayerLivesCharacteristic
    {
        [SerializeField] private int _baseLives;
        [SerializeField] private int _currentLives;
        [SerializeField] private int _maxLives;

        public ref int GetCurrrentLives => ref _currentLives;
        public int GetMaxLives => _maxLives;


        public PlayerLivesCharacteristic(PlayerLivesCharacteristic playerCharacteristic)
        {
            _baseLives = playerCharacteristic._baseLives;
            _maxLives = playerCharacteristic._maxLives;
            _currentLives = playerCharacteristic._baseLives;
        }

      

        internal void LoadInitValue()
        {
            _currentLives = _baseLives;
        }

        public int UpdateLives(int value)
        {
            _currentLives = _currentLives + value;
            if (_currentLives <= 0) return _currentLives = 0;
            if (_currentLives >= _maxLives) return _currentLives = _maxLives;
            return _currentLives;
        }
    }
}