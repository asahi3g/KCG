using Entitas;
using System.ComponentModel.DataAnnotations;

namespace Mech
{
    // Makes item destructable.
    [Mech]
    public class StatusComponent : IComponent
    {
        public int Health;

        public int TreeSize;
    }
}
