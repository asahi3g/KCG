using Enums;
using System;
using System.Collections.Generic;

namespace Node
{ 
    public class NodeBase
    {
        public virtual ActionType Type => ActionType.None;

        // List of blackboard entries used in the node.
        // This allows states parameters to show in AI visual tool.
        public virtual List<Tuple<string, Type>> RegisterEntries() => null;

        protected Tuple<string, Type> CreateEntry(string name, Type type) => new Tuple<string, Type>(name, type);

        // Run once at the beggining of the action.
        public virtual void OnEnter(NodeEntity nodeEntity)
        {
            nodeEntity.nodeExecution.State = NodeState.Running;
        }

        // Run once per frame until action state is changed to sucess or fail.
        public virtual void OnUpdate(NodeEntity nodeEntity)
        {
            nodeEntity.nodeExecution.State = NodeState.Success;
        }

        // Run once if action succeeded.
        public virtual void OnExit(NodeEntity nodeEntity)
        {
        }

        // Run once if action failed
        public virtual void OnFail(NodeEntity nodeEntity)
        {
        }
    }
}
