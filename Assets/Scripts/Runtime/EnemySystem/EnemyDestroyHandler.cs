using System;
using Runtime.AudioSystem;
using UnityEngine;

namespace Runtime.EnemySystem
{
    public class EnemyDestroyHandler
    {
        private readonly EnemyFacade _facade;
        
        private readonly Settings _settings;
        
        private readonly AudioPlayer _audioPlayer;
        
        
        public EnemyDestroyHandler(
            EnemyFacade facade, 
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