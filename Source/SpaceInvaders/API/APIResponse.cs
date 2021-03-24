using SpaceInvaders.Objects;
using System.Collections.Generic;

namespace SpaceInvaders.API
{
    public class APIResponse<T> : IAPIResponse<T>
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<T> Results { get; set; }
    }
}
