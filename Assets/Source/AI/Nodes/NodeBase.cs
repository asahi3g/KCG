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
        /// List of blackboard entries used in the node.
        /// This allows states parameters to show in AI visual tool.
        /// </summary>
        public virtual List<Tuple<string, Type>> RegisterEntries() => null;

        // 
        protected Tuple<string, Type> CreateEntry(string name, Type type) => new Tuple<string, Type>(name, type);

        /// <summary> Run once at the beggining of the action. </summary>
        public virtual void OnEnter(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        /// <summary> Run once per frame until action state is changed to sucess or fail. </summary>
        public virtual void OnUpdate(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
            nodeEntity.nodeExecution.State = NodeState.Success;
        }

        /// <summary> Run once if action succeeded</summary>
        public virtual void OnExit(ref Planet.PlanetState planet, NodeEntity nodeEntity)
        {
        }

        /// <summary> Run once if action failed</summary>
        public virtual void OnFail(ref Planet.PlanetState plane, NodeEntity nodeEntity)
        {
        }
    }
}
