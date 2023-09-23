using System;
using Runtime.AudioSystem;
using UnityEngine;

namespace Runtime.EnemySystem
{
    public class EnemyDestroyHandler
    {
        private readonly EnemyFacade _enemyFacade;
        
        private readonly EnemyDestroyData _enemyDestroyData;
        
        private readonly AudioPlayer _audioPlayer;
        
        
        public EnemyDestroyHandler(
            EnemyFacade enemyFacade, 
            EnemyDestroyData enemyDestroyData,
            AudioPlayer audioPlayer)
        {
            _enemyFacade = enemyFacade;
            _enemyDestroyData = enemyDestroyData;
            _audioPlayer = audioPlayer;
        }
        
        public void Destroy()
        {
            _audioPlayer.Play(_enemyDestroyData.DestroySound, _enemyDestroyData.DestroySoundVolume);
            _enemyFacade.Dispose();
        }
        
        [Serializable]
        public class EnemyDestroyData
        {
            public AudioClip DestroySound;
            
            public float DestroySoundVolume;
        }
    }
}