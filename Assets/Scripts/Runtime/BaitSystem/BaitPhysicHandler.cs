using UnityEngine;

namespace Runtime.BaitSystem
{
    public class BaitPhysicHandler
    {
        private const string GROUND_TAG = "Ground";
        
        private readonly BaitFacade _facade;
        
        public BaitPhysicHandler(BaitFacade facade)
        {
            _facade = facade;
        }
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GROUND_TAG))
            {
                _facade.Dispose();
            }
        }
    }
}