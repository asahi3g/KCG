using System;
using UnityEngine.Assertions;

namespace NodeSystem
{
    public class NodeManager
    {
        Node[] Nodes = new Node[1024];
        string[] Names = new string[1024];

        int Length = 0;
        int currentID = -1;
        int[] Children = new int[64];
        int childrenCount = 0;

        public int CreateNode(string Name, NodeType type)
        {
            currentID = Length++;
            Names[currentID] = Name;
            ref Node node = ref Nodes[currentID];
            node.ID = currentID;
            node.Type = type;

            switch (type)
            {
                case NodeType.Decorator:
                    SetCondition(ConditionManager.TrueConditionID);
                    break;
                case NodeType.Repeater:
                    SetCondition(ConditionManager.TrueConditionID);
                    break;
                case NodeType.Sequence:
                    SetCondition(ConditionManager.TrueConditionID);
                    SetData(BTSpecialChild.NotInitialized);
                    break;
                case NodeType.Selector:
                    SetCondition(ConditionManager.TrueConditionID);
                    SetData(BTSpecialChild.NotInitialized);
                    break;
                case NodeType.ActionSequence:
                    Nodes[currentID].ActionID = GameState.ActionManager.GetID("ActionSequence");
                    SetCondition(ConditionManager.TrueConditionID);
                    SetData(new ActionSequenceNode.ActionSequenceData());
                    break;
                case NodeType.Action:
                    SetCondition(ConditionManager.TrueConditionID);
                    node.DataInit = new byte[0];
                    break;
            }

            return node.ID;
        }

        public ref Node GetRef(int id)
        {
            return ref Nodes[id];
        }

        public Node Get(int id)
        { 
            return Nodes[id];
        }

        public string GetName(int id) => Names[id];

        public string PrintNode(int id) => Names[id] + "[" + Nodes[id].Type.ToString() + "]";

        public void SetCondition(int conditionID) => Nodes[currentID].ConditionalID = conditionID;

        public void SetAction(int actionID)
        {
            if (Nodes[currentID].Type == NodeType.Action)
                Nodes[currentID].ActionID = actionID;
            else if (Nodes[currentID].Type == NodeType.ActionSequence)
            {
                int id = currentID;
                int childId = CreateNode(Names[id] + "Enter" , NodeType.Action);
                SetAction(actionID);
                EndNode();
                CreateNode(Names[id] + "Update", NodeType.Action);
                SetAction(actionID + 1);
                EndNode();
                CreateNode(Names[id] + "Success", NodeType.Action);
                SetAction(actionID + 2);
                EndNode();
                CreateNode(Names[id] + "Failure", NodeType.Action);
                SetAction(actionID + 3);
                EndNode();
                currentID = id;
                AddChild(childId);
                AddChild(childId + 1);
                AddChild(childId + 2);
                AddChild(childId + 3);
            }
            else
            {
                Assert.IsTrue(false, "You can't set action in node of type: " + Nodes[currentID].Type.ToString());
            }
        }

        public void SetData<T>(T data) where T : struct => Nodes[currentID].SetData<T>(ref data);

        public void AddData<T>(T data) where T : struct => Nodes[currentID].AddData<T>(ref data);

        public void AddChild(int nodeId)
        {
            Assert.IsTrue(Nodes[currentID].Type != NodeType.Action, "Action node can't have children.");
            Children[childrenCount++] = nodeId;
            Nodes[currentID].SubTreeNodeCount += Nodes[nodeId].SubTreeNodeCount;
        }
        public void EndNode()
        {
            if (childrenCount > 0)
            {
                Nodes[currentID].Children = new int[childrenCount];
                Array.Copy(Children, Nodes[currentID].Children, childrenCount);
                Nodes[currentID].SubTreeNodeCount += (Nodes[currentID].Type != NodeType.ActionSequence) ? childrenCount : 0; 
                childrenCount = 0;
            }
            currentID = -1;
        }
    }
}
