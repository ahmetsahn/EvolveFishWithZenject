using System;
using Runtime.AudioSystem;
using UnityEngine;

namespace Runtime.BaitSystem
{
    public class BaitDestroyHandler
    {
        private readonly BaitFacade _facade;
        
        private readonly Settings _settings;
        
        private readonly AudioPlayer _audioPlayer;
        
        
        public BaitDestroyHandler(
            BaitFacade facade, 
            Settings settings,
            AudioPlayer audioPlayer)
        {
            _facade = facade;
            _settings = settings;
            _audioPlayer = audioPlayer;
        }
        
        public void Destroy()
        {
            _audioPlayer.Play(_settings.DestroySound, _settings.DestroySoundVolume);
            _facade.Dispose();
        }
        
        [Serializable]
        public class Settings
        {
            public AudioClip DestroySound;
            
            public float DestroySoundVolume;
        }
    }
}