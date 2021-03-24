using System.Collections.Generic;

namespace SpaceInvaders.API
{
    public interface IAPIResponse<T>
    {
        string Next { get; set; }
        List<T> Results { get; set; }
    }
}
