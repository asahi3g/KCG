using NodeSystem;
using System;
using System.Linq;
using UnityEngine;

namespace BehaviorTree
{
    // Execute instance of tree.
    public struct BehaviorTreeExecute
    {
        public int BehaviorTreeID;
        public int RootNodeId;
        public int CurrentDepth;
        public int[] StackTree;
        NodeState LastResult;
        NodesExecutionState DataState;

        public BehaviorTreeExecute(int rootID, int agentID, int treeID)
        {
            BehaviorTreeID = treeID;
            RootNodeId = rootID;
            CurrentDepth = 0;
            ref NodeSystem.Node node = ref GameState.NodeManager.GetRef(rootID);
            DataState = new NodesExecutionState()
            {
                NodesExecutiondata = new NodeExcutionData[node.SubTreeNodeCount + 1], // Sum + 1 to count root node node.
                AgentID = agentID,
            };
            LastResult = NodeState.None;

            // Initialize stack tree.
            StackTree = null;
            int depth = 1;
            InitializeStackTree(in node, ref depth);
            StackTree = new int[depth];
            for (int i = 1; i < depth; i++)
            {
                StackTree[i] = BTSpecialChild.NotInitialized;
            }

            // Initialize memory.
            InitializeTree(in node, 0);
            int DataStateSize = DataState.NodesExecutiondata[node.SubTreeNodeCount].MemoryOffset +
                GameState.NodeManager.Get(DataState.NodesExecutiondata[node.SubTreeNodeCount].Id).DataInit.Length;
            DataState.NodeMemory = new byte[DataStateSize];
            DataState.ResetNodeMemoryData();
        }

        public void Update()
        {
            for (int i = 0; i < DataState.NodesExecutiondata.Length; i++)
            {
                if (!StackTree.Contains(i))
                    DataState.NodesExecutiondata[i].ExecutionTime = 0;
                DataState.NodesExecutiondata[i].ExecutionTime += Time.deltaTime;
            }

            ExecuteTree();
        }

        public void ExecuteTree()
        {
            int index = StackTree[CurrentDepth];
            ref NodeSystem.Node currentNode = ref GameState.NodeManager.GetRef(DataState.NodesExecutiondata[index].Id);
            ulong ptr = DataState.GetAddress();

            if (LastResult == NodeState.Running)
            {
                ActionManager.Action action = GameState.ActionManager.Get(currentNode.ActionID);
                LastResult = action(ptr, index);
                if (LastResult != NodeState.Running)
                    CurrentDepth -= 1;
                return;
            }

            // Test condition.
            if (currentNode.ConditionalID != ConditionManager.TrueConditionID)
            {
                ConditionManager.Condition condition = GameState.ConditionManager.Get(currentNode.ConditionalID);
                if (!condition(ptr))
                {
                    CurrentDepth -= 1;
                    LastResult = NodeState.Failure;
                    ExecuteTree();
                    return;
                }
            }

            // Run action.
            int nextChildIndex = 0;
            switch (currentNode.Type)
            {
                case NodeType.Decorator:
                    nextChildIndex = DecoratorNode.NextRoute(GetCurrentChild());
                    break;
                case NodeType.Repeater:
                    nextChildIndex = RepeaterNode.NextRoute(ptr, index, GetCurrentChild());
                    break;
                case NodeType.Sequence:
                    nextChildIndex = SequenceNode.GetNextChildren(ptr, index, LastResult);
                    break;
                case NodeType.Selector:
                    nextChildIndex = SelectorNode.GetNextChildren(ptr, index, LastResult);
                    break;
                default:
                    ActionManager.Action action = GameState.ActionManager.Get(currentNode.ActionID);
                    LastResult = action(ptr, index);
                    for (int i = CurrentDepth + 1; i < StackTree.Length; i++)
                    {
                        StackTree[i] = BTSpecialChild.NotInitialized;
                    }
                    if (LastResult != NodeState.Running)
                        CurrentDepth -= 1;
                    return;
            }

            if (nextChildIndex == BTSpecialChild.ReturnToParent)
            {
                CurrentDepth--;
                ExecuteTree();
            }
            else
            {
                StackTree[CurrentDepth + 1] = GetIndexFromChildIndex(nextChildIndex);
                CurrentDepth++;
                ExecuteTree();
            }
        }

        void InitializeTree(in NodeSystem.Node node, int index)
        {
            DataState.NodesExecutiondata[index].Id = node.ID;
            int DataStateSize = DataState.NodesExecutiondata[index].MemoryOffset;
            
            if (index == DataState.NodesExecutiondata.Length - 1)
                return;

            if (node.DataInit != null)
                DataStateSize += node.DataInit.Length;

            int childIndex = index + 1;
            DataState.NodesExecutiondata[childIndex].MemoryOffset = DataStateSize;
            
            if (node.Children == null || node.Type == NodeType.ActionSequence)
                return;
            
            for (int i = 0; i < node.Children.Length; i++)
            {
                ref NodeSystem.Node child = ref GameState.NodeManager.GetRef(node.Children[i]);
                InitializeTree(in child, childIndex);
                childIndex += child.SubTreeNodeCount + 1;
            }
        }

        void InitializeStackTree(in NodeSystem.Node node, ref int depth)
        {
            if (node.Children == null)
                return;

            depth++;
            for (int i = 0; i < node.Children.Length; i++)
            {
                int childDeth = depth;
                ref NodeSystem.Node child = ref GameState.NodeManager.GetRef(node.Children[i]);
                InitializeStackTree(in child, ref childDeth);
                if (childDeth > depth)
                    depth = childDeth;
            }
        }

        ref NodeSystem.Node GetNodeFromIndex(int index)
        {
            int id = DataState.NodesExecutiondata[index].Id;
            return ref GameState.NodeManager.GetRef(DataState.NodesExecutiondata[index].Id);
        }

        int GetIndexFromChildIndex(int childIndex)
        {
            int currentIndex = StackTree[CurrentDepth];
            int index = currentIndex + 1;
            for (int i = 0; i < childIndex; i++)
            {
                ref NodeSystem.Node node = ref GetNodeFromIndex(index);
                index += node.SubTreeNodeCount + 1;
            }
            return index;
        }

        int GetChildIndex(int parent, int child)
        {
            ref NodeSystem.Node parentNode = ref GetNodeFromIndex(parent);
            int childIndex = parent + 1;
            for (int i = 0; i < parentNode.Children.Length; i++)
            {
                if (child == childIndex)
                    return i;
                ref NodeSystem.Node childNode = ref GetNodeFromIndex(childIndex);
                childIndex += childNode.SubTreeNodeCount + 1;
            }

            return BTSpecialChild.NotInitialized;
        }

        int GetCurrentChild() => GetChildIndex(StackTree[CurrentDepth], StackTree[CurrentDepth + 1]);
    }
}
