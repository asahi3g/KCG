using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using Enums;

namespace Mech
{
    public class IDComponent : IComponent
    {
        [EntityIndex]
        public int ID;
    }
}
