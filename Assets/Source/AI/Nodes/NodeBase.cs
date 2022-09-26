using Entitas;
using Enums;
using System.Diagnostics;

namespace Node
{ 
    public class NodeBase
    {
        public virtual NodeType Type { get { return NodeType.None; } }

        public virtual void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
        }

        public virtual void OnUpdate(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {      
        }

        /// <summary>
        /// We should always delete actions after executed.
        /// </summary>
        public virtual void OnExit(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            nodeEntity.Destroy();
        }

        public virtual void CheckProceduralPrecondition(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {

        }

        public virtual void ProceduralEffects(ref Planet.PlanetState plane, NodeEntity nodeEntity)
        { 
        }
    }
}
