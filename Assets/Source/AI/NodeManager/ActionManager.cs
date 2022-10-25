using System.Collections.Generic;

namespace NodeSystem
{
    public class ActionManager
    {
        public delegate NodeState Action(object data, int id);
        public Action[] Actions = new Action[1024];
        public string[] Names = new string[1024];
        private Dictionary<string, int> NameIDPairs = new Dictionary<string, int>();
        private NodeState DefaultAction(object data, int id) => NodeState.Success;
        private int Length = 1;
        
        public ActionManager()
        {
            RegisterAction(NodeType.Decorator.ToString(), DecoratorNode.Execute);
            RegisterAction(NodeType.Repeater.ToString(), RepeaterNode.Execute);
            RegisterAction(NodeType.Sequence.ToString(), SequenceNode.Execute);
            RegisterAction(NodeType.Selector.ToString(), SelectorNode.Execute);
            RegisterAction(NodeType.ActionSequence.ToString(), ActionSequenceNode.Execute);
            RegisterAction("Default", DefaultAction);
        }

        public Action Get(int id)
        { 
            return Actions[id];
        }

        public Action Get(string name)
        {
            return Get(NameIDPairs[name]);
        }

        public int GetID(string name) => NameIDPairs[name];

        // Register Action function
        public int RegisterAction(string name, Action action)
        {
            Actions[Length] = action;
            Names[Length] = name;
            NameIDPairs.Add(name, Length);
            return Length++;
        }

        public int RegisterActionSequence(string name, Action onEnter = null, Action onUpdate = null, Action onSuccess = null, Action onFailure = null)
        {
            if (onEnter == null)
                onEnter = DefaultAction;
            if (onUpdate == null)
                onUpdate = DefaultAction;
            if (onSuccess == null)
                onSuccess = DefaultAction;
            if (onFailure == null)
                onFailure = DefaultAction;

            NameIDPairs.Add(name, Length);
            Names[Length] = name + "Enter";
            Names[Length + 1] = name + "Update";
            Names[Length + 2] = name + "Success";
            Names[Length + 3] = name + "Failure";
            Actions[Length++] = onEnter;
            Actions[Length++] = onUpdate;
            Actions[Length++] = onSuccess;
            Actions[Length++] = onFailure;
            return Length;
        }
    }
}
