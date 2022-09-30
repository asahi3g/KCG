using Entitas;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mech
{
    /// <summary>
    /// Makes item destructable.
    /// </summary>
    [Mech]
    public class TreeComponent : IComponent
    {
        [Range(0,100)]
        public int Health;

        [Range(0, 5)]
        public int TreeSize;
    }
}
