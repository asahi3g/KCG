using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using Enums;

namespace Mech
{
    [Mech]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        public int ID;
    }
}
