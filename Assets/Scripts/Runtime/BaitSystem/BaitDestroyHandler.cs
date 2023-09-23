using System;
using Runtime.AudioSystem;
using UnityEngine;

namespace Runtime.BaitSystem
{
    public class BaitDestroyHandler
    {
        private readonly BaitFacade _baitFacade;
        
        private readonly BaitDestroyData _baitDestroyData;
        
        private readonly AudioPlayer _audioPlayer;
        
        
        public BaitDestroyHandler(
            BaitFacade baitFacade, 
            BaitDestroyData baitDestroyData,
            AudioPlayer audioPlayer)
        {
            _baitFacade = baitFacade;
            _baitDestroyData = baitDestroyData;
            _audioPlayer = audioPlayer;
        }
        
        public void Destroy()
        {
            _audioPlayer.Play(_baitDestroyData.DestroySound, _baitDestroyData.DestroySoundVolume);
            _baitFacade.Dispose();
        }
        
        [Serializable]
        public class BaitDestroyData
        {
            public AudioClip DestroySound;
            
            public float DestroySoundVolume;
        }
    }
}