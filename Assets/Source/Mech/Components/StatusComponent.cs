using Entitas;
using System.ComponentModel.DataAnnotations;

namespace Mech
{
    /// <summary>
    /// Makes item destructable.
    /// </summary>
    [Mech]
    public class StatusComponent : IComponent
    {
        [Range(0,100)]
        public int Health;

        [Range(0, 5)]
        public int TreeSize;
    }
}
