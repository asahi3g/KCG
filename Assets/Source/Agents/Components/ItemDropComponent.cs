using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Agent
{
    [Agent]
    public class ItemDropComponent : IComponent
    {
        public Enums.ItemType[] Drops; // items to drop
        public int[] MaxDropCount;
        public float[] DropRate; // item drop rate
    }
}