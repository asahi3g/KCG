using Enums;
using System;
using System.Collections.Generic;

namespace Node
{ 
    public class NodeBase
    {
        public virtual NodeType Type => NodeType.None;
        public virtual NodeGroup NodeGroup => NodeGroup.PlayerAction;

        /// <summary>
        /// List of blackboard entries used in the node.
        /// This allows states parameters to show in AI visual tool.
        /// </summary>
        public virtual List<Tuple<string, Type>> RegisterEntries() => null;

        // 
        protected Tuple<string, Type> CreateEntry(string name, Type type) => new(name, type);

        /// <summary> Run once at the beggining of the action. </summary>
        public virtual void OnEnter(NodeEntity nodeEntity)
        {
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        /// <summary> Run once per frame until action state is changed to sucess or fail. </summary>
        public virtual void OnUpdate(NodeEntity nodeEntity)
        {
            nodeEntity.nodeExecution.State = NodeState.Success;
        }

        /// <summary> Run once if action succeeded</summary>
        public virtual void OnExit(NodeEntity nodeEntity)
        {
        }

        /// <summary> Run once if action failed</summary>
        public virtual void OnFail(NodeEntity nodeEntity)
        {
        }
    }
}
