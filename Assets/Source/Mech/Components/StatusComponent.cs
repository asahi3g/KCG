using Entitas;
using System.ComponentModel.DataAnnotations;

namespace Mech
{
    // Makes item destructable.
    [Mech]
    public class StatusComponent : IComponent
    {
        [Range(0,100)]
        public int Health;

        [Range(0, 5)]
        public int TreeSize;
    }
}
