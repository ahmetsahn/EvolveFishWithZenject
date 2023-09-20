using System.Collections.Generic;

namespace Runtime.BaitSystem
{
    public class BaitRegistry
    {
        private readonly List<BaitFacade> _baits = new();

        public IEnumerable<BaitFacade> Baits => _baits;

        public void AddBait(BaitFacade bait)
        {
            _baits.Add(bait);
        }

        public void RemoveBait(BaitFacade bait)
        {
            _baits.Remove(bait);
        }
    }
}