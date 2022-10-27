//imports UnityEngine

using Entitas;
using KMath;

namespace FloatingText
{
    [FloatingText]
    public class GameObjectComponent : IComponent
    {
        public UnityEngine.GameObject GameObject; // used for unity rendering
    }
}
