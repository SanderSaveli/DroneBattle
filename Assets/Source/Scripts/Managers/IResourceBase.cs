using System;
using System.Collections.Generic;

namespace Sander.DroneBattle
{
    public interface IResourceBase
    {
        public IReadOnlyList<Resource> Resources { get; }
        public Action<Resource> OnNewResourceAdded { get; set; }
    }
}
