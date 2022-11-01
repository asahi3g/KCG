using System;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using static Unity.Collections.LowLevel.Unsafe.UnsafeUtility;

namespace NodeSystem.BehaviorTree
{
    public struct BehaviorTreeExecute
    {
        public int BehaviorTreeID;
        public int RootNodeId;
        public int CurrentDepth;
        public int[] StackTree;
        NodeState LastResult;
        BehaviorTreeState Data;

        public BehaviorTreeExecute(int rootID, int agentID, int treeID)
        {
            BehaviorTreeID = treeID;

            RootNodeId = rootID;
            CurrentDepth = 0;
            ref Node node = ref GameState.NodeManager.GetRef(rootID);
            Data = new BehaviorTreeState()
            {
                NodesExecutiondata = new NodeExcutionData[node.SubTreeNodeCount + 1], // Sum + 1 to count root node node.
                AgentID = agentID,
                BlackboardID = GameState.BlackboardManager.CreateBlackboard()
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
            int dataSize = Data.NodesExecutiondata[node.SubTreeNodeCount].MemoryOffset +
                GameState.NodeManager.Get(Data.NodesExecutiondata[node.SubTreeNodeCount].Id).DataInit.Length;
            Data.NodeMemory = new byte[dataSize];
            Data.ResetNodeMemoryData();
        }

        public void Update()
        {
            for (int i = 0; i < Data.NodesExecutiondata.Length; i++)
            {
                if (!StackTree.Contains(i))
                    Data.NodesExecutiondata[i].ExecutionTime = 0;
                Data.NodesExecutiondata[i].ExecutionTime += Time.deltaTime;
            }

            UpdateTree();
        }

        public void UpdateTree()
        {
            int index = StackTree[CurrentDepth];
            ref Node currentNode = ref GameState.NodeManager.GetRef(Data.NodesExecutiondata[index].Id);
            ulong ptr = Data.GetAddress();

            if (LastResult == NodeState.Running)
            {
                ActionManager.Action action = GameState.ActionManager.Get(currentNode.ActionID);
                LastResult = action(ptr, index);
                if (LastResult != NodeState.Running)
                    CurrentDepth -= 1;
                return;
            }

            // Test condition.
            ConditionManager.Condition condition = GameState.ConditionManager.Get(currentNode.ConditionalID);
            if (!condition(ptr))
            {
                CurrentDepth -= 1;
                LastResult = NodeState.Failure;
                UpdateTree();
                return;
            }

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
                UpdateTree();
            }
            else
            {
                StackTree[CurrentDepth + 1] = GetIndexFromChildIndex(nextChildIndex);
                CurrentDepth++;
                UpdateTree();
            }
        }

        void InitializeTree(in Node node, int index)
        {
            Data.NodesExecutiondata[index].Id = node.ID;
            int dataSize = Data.NodesExecutiondata[index].MemoryOffset;
            
            if (index == Data.NodesExecutiondata.Length - 1)
                return;

            if (node.DataInit != null)
                dataSize += node.DataInit.Length;

            int childIndex = index + 1;
            Data.NodesExecutiondata[childIndex].MemoryOffset = dataSize;
            
            if (node.Children == null || node.Type == NodeType.ActionSequence)
                return;
            
            for (int i = 0; i < node.Children.Length; i++)
            {
                ref Node child = ref GameState.NodeManager.GetRef(node.Children[i]);
                InitializeTree(in child, childIndex);
                childIndex += child.SubTreeNodeCount + 1;
            }
        }

        public void InitializeStackTree(in Node node, ref int depth)
        {
            if (node.Children == null)
                return;

            depth++;
            for (int i = 0; i < node.Children.Length; i++)
            {
                int childDeth = depth;
                ref Node child = ref GameState.NodeManager.GetRef(node.Children[i]);
                InitializeStackTree(in child, ref childDeth);
                if (childDeth > depth)
                    depth = childDeth;
            }
        }

        public ref Node GetNodeFromIndex(int index)
        {
            int id = Data.NodesExecutiondata[index].Id;
            return ref GameState.NodeManager.GetRef(Data.NodesExecutiondata[index].Id);
        }

        public int GetIndexFromChildIndex(int childIndex)
        {
            int currentIndex = StackTree[CurrentDepth];
            int index = currentIndex + 1;
            for (int i = 0; i < childIndex; i++)
            {
                ref Node node = ref GetNodeFromIndex(index);
                index += node.SubTreeNodeCount + 1;
            }
            return index;
        }

        public int GetChildIndex(int parent, int child)
        {
            ref Node parentNode = ref GetNodeFromIndex(parent);
            int childIndex = parent + 1;
            for (int i = 0; i < parentNode.Children.Length; i++)
            {
                if (child == childIndex)
                    return i;
                ref Node childNode = ref GetNodeFromIndex(childIndex);
                childIndex += childNode.SubTreeNodeCount + 1;
            }

            return BTSpecialChild.NotInitialized;
        }

        public int GetCurrentChild() => GetChildIndex(StackTree[CurrentDepth], StackTree[CurrentDepth + 1]);
    }
}
