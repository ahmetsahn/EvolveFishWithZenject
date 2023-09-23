using UnityEngine;

namespace Runtime.BaitSystem
{
    public class BaitPhysicHandler
    {
        private const string GROUND_TAG = "Ground";
        
        private readonly BaitFacade _baitFacade;
        
        public BaitPhysicHandler(BaitFacade baitFacade)
        {
            _baitFacade = baitFacade;
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GROUND_TAG))
            {
                _baitFacade.Dispose();
            }
        }
    }
}