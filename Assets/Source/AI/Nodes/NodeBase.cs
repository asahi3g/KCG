using AI.Movement;
using Entitas;
using Enums;
using System;
using System.Collections.Generic;

namespace Node
{ 
    public class NodeBase
    {
        public virtual NodeType Type { get { return NodeType.None; } }
        public virtual NodeGroup NodeGroup { get { return NodeGroup.PlayerAction; } }

        /// <summary>
        /// List of states used in the node.
        /// This allows states parameters to show in AI visual tool.
        /// </summary>
        public virtual List<Tuple<string, Type>> RegisterStates() => null;
        protected Tuple<string, Type> CreateEntry(string name, Type type) => new Tuple<string, Type>(name, type);

        public virtual void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        public virtual void OnUpdate(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            nodeEntity.nodeExecution.State = NodeState.Success;
        }

        /// <summary> We should always delete actions after executed.</summary>
        public virtual void OnExit(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
        }

        public virtual void CheckProceduralPrecondition(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {

        }

        public virtual void ProceduralEffects(ref Planet.PlanetState plane, NodeEntity nodeEntity)
        { 
        }
    }
}
