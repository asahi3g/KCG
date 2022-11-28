using System.Collections.Generic;

namespace NodeSystem
{
    public class ActionManager
    {
        public delegate NodeState Action(object data, int id);
        public Action[] Actions = new Action[1024];
        public string[] Names = new string[1024];
        private Dictionary<string, int> NameIDPairs = new Dictionary<string, int>();

        public const int SuccessActionID = 0; // Id of default condition.
        private NodeState SuccessAction(object data, int id) => NodeState.Success;
        private int Length = 0;
        
        public ActionManager()
        {
            RegisterAction("Success", SuccessAction);
            RegisterAction("ActionSequence", ActionSequenceNode.Action);
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
            if (NameIDPairs.ContainsKey(name))
            {
                return Length;
            }
            else
            {
                Actions[Length] = action;
                Names[Length] = name;
                NameIDPairs.Add(name, Length);
                return Length++;
            }
        }

        public int RegisterActionSequence(string name, Action onEnter = null, Action onUpdate = null, Action onSuccess = null, Action onFailure = null)
        {
            if (NameIDPairs.ContainsKey(name))
            {
                return Length;
            }
            else
            {
                if (onEnter == null)
                    onEnter = SuccessAction;
                if (onUpdate == null)
                    onUpdate = SuccessAction;
                if (onSuccess == null)
                    onSuccess = SuccessAction;
                if (onFailure == null)
                    onFailure = SuccessAction;

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
}
