using Entitas;
using UnityEngine;

namespace FloatingText
{
    [FloatingText]
    public class TextComponent : IComponent
    {
        public string Text;
        public Color color;
        public int fontSize;
    }
}