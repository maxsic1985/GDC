using System;



namespace MSuhininTestovoe.B2B
{
    public interface ITimeService
    {
        float DeltaTime { get; }
        float InGameTime { get; }
        DateTime UtcNow { get; }

        void Pause();
        void Resume();
        
    }
}