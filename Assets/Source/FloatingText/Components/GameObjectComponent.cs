using Entitas;
using UnityEngine;
using KMath;

namespace FloatingText
{
    [FloatingText]
    public class GameObjectComponent : IComponent
    {
        public GameObject GameObject; // used for unity rendering
    }
}
