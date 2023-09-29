using System;
using UnityEngine;


namespace MSuhininTestovoe.B2B
{
    public sealed class UnityTimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
        public float InGameTime => Time.time;
        public DateTime UtcNow => DateTime.UtcNow;
        public long ToUnixTimeSeconds => _timeOffset.ToUnixTimeSeconds();

        private bool gameIsPaused = false;

        private DateTimeOffset _timeOffset;

        public UnityTimeService()
        {
            _timeOffset = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        }

        public void Pause() => Time.timeScale = 0f;

        public void Resume() => Time.timeScale = 1f;
    }
}