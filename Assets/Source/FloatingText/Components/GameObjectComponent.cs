//imports UnityEngine

using Entitas;

namespace FloatingText
{
    [FloatingText]
    public class GameObjectComponent : IComponent
    {
        public UnityEngine.GameObject GameObject; // used for unity rendering
    }
}
