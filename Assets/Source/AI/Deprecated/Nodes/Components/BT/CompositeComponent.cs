using Entitas;
using System.Collections.Generic;

namespace Node
{
    [Node]
    public class CompositeComponent : IComponent
    {
        public List<int> Children;

        public int CurrentID;
    }
}
