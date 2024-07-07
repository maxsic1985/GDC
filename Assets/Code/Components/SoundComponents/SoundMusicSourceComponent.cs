using UnityEngine;

namespace MSuhininTestovoe.B2B
{
    public struct SoundMusicSourceComponent
    {
        public AudioSource Source;
        public AudioClip[] Tracks;
        public int PlayedTrack;
        
        public  int FirstTrackNumber;
    }
}