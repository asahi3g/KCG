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

        public int CreateNode(string Name, ItemUsageActionType  type)
        {
            currentID = Length++;
            Names[currentID] = Name;
            ref Node node = ref Nodes[currentID];
            node.ID = currentID;
            node.Type = type;

            switch (type)
            {
                case ItemUsageActionType .Decorator:
                    SetCondition(ConditionManager.TrueConditionID);
                    break;
                case ItemUsageActionType .Repeater:
                    SetCondition(ConditionManager.TrueConditionID);
                    break;
                case ItemUsageActionType .Sequence:
                    SetCondition(ConditionManager.TrueConditionID);
                    SetData(BTSpecialChild.NotInitialized);
                    break;
                case ItemUsageActionType .Selector:
                    SetCondition(ConditionManager.TrueConditionID);
                    SetData(BTSpecialChild.NotInitialized);
                    break;
                case ItemUsageActionType .ActionSequence:
                    Nodes[currentID].ActionID = GameState.ActionManager.GetID("ActionSequence");
                    SetCondition(ConditionManager.TrueConditionID);
                    SetData(new ActionSequenceNode.ActionSequenceData());
                    break;
                case ItemUsageActionType .Action:
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

        public string PrintNode(int id) => Names[id] + "[" + Nodes[id].Type.ToString() + "]";

        public void SetCondition(int conditionID) => Nodes[currentID].ConditionalID = conditionID;

        public void SetAction(int actionID)
        {
            if (Nodes[currentID].Type == ItemUsageActionType .Action)
                Nodes[currentID].ActionID = actionID;
            else if (Nodes[currentID].Type == ItemUsageActionType .ActionSequence)
            {
                int id = currentID;
                int childId = CreateNode(Names[id] + "Enter" , ItemUsageActionType .Action);
                SetAction(actionID);
                EndNode();
                CreateNode(Names[id] + "Update", ItemUsageActionType .Action);
                SetAction(actionID + 1);
                EndNode();
                CreateNode(Names[id] + "Success", ItemUsageActionType .Action);
                SetAction(actionID + 2);
                EndNode();
                CreateNode(Names[id] + "Failure", ItemUsageActionType .Action);
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
            Assert.IsTrue(Nodes[currentID].Type != ItemUsageActionType .Action, "Action node can't have children.");
            Children[childrenCount++] = nodeId;
            Nodes[currentID].SubTreeNodeCount += Nodes[nodeId].SubTreeNodeCount;
        }
        public void EndNode()
        {
            if (childrenCount > 0)
            {
                Nodes[currentID].Children = new int[childrenCount];
                Array.Copy(Children, Nodes[currentID].Children, childrenCount);
                Nodes[currentID].SubTreeNodeCount += (Nodes[currentID].Type != ItemUsageActionType .ActionSequence) ? childrenCount : 0; 
                childrenCount = 0;
            }
            currentID = -1;
        }
    }
}
