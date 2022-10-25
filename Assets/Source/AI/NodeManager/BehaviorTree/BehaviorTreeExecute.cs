using System;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.LowLevel.Unsafe.UnsafeUtility;

namespace NodeSystem.BehaviorTree
{
    public struct BehaviorTreeExecute
    {
        public int BehaviorTreeID;
        public int RootNodeId;
        public int ActiveNodeId;
        BehaviorTreeState Data;

        void InitializeActionSequence(in Node node)
        {
            int dataSize = Data.NodeDataOffset[Data.NodeIdToIndex.Count];
            Data.NodeDataOffset[Data.NodeIdToIndex.Count + 1] = dataSize + SizeOf<ActionSequenceNode.ActionSequenceData>();
            Data.NodeDataOffset[Data.NodeIdToIndex.Count + 2] = dataSize + node.DataInit.Length;

            Data.NodeIdToIndex.Add(node.ID, Data.NodeIdToIndex.Count);
            Data.NodeIdToIndex.Add(node.ID + 1, Data.NodeIdToIndex.Count);
        }

        void InitializeTree(in Node node)
        {
            int dataSize = Data.NodeDataOffset[Data.NodeIdToIndex.Count];
            if (node.DataInit != null)
                dataSize += node.DataInit.Length;
            Data.NodeDataOffset[Data.NodeIdToIndex.Count + 1] = dataSize;
            Data.NodeIdToIndex.Add(node.ID, Data.NodeIdToIndex.Count);
            
            if (node.Children == null)
                return;
            
            for (int i = 0; i < node.Children.Length; i++)
            {
                Node child = GameState.NodeManager.Get(node.Children[i]);
                if (child.Type == NodeType.ActionSequence)
                {
                    InitializeActionSequence(in child);
                    continue;
                }
                InitializeTree(in child);
            }
        }

        void InitializeDataTree(in Node node)
        {
            int index = Data.NodeIdToIndex[node.ID];
            int dataOffset = Data.NodeDataOffset[index];
            if (node.DataInit != null) 
                Array.Copy(node.DataInit, 0, Data.NodeData, dataOffset, node.DataInit.Length);

            if (node.Children == null || node.Type == NodeType.ActionSequence)
                return;

            for (int i = 0; i < node.Children.Length; i++)
            {
                Node child = GameState.NodeManager.Get(node.Children[i]);
                InitializeDataTree(in child);
            }
        }

        public BehaviorTreeExecute(int rootID, int agentID, int treeID)
        {
            BehaviorTreeID = treeID;

            RootNodeId = rootID;
            ActiveNodeId = rootID;
            Node node = GameState.NodeManager.Get(rootID);
            Data = new BehaviorTreeState()
            {
                NodesExecutiondata = new NodeExcutionData[node.SubTreeNodeCount + 1], // Last pos has data size.
                AgentID = agentID,
                BlackboardID = GameState.BlackboardManager.CreateBlackboard()
            };
            InitializeTree(in node);
            int dataSize = Data.NodesExecutiondata[node.SubTreeNodeCount].MemoryOffset + 
                GameState.NodeManager.Get(Data.NodesExecutiondata[node.SubTreeNodeCount].Id).DataInit.Length;
            Data.NodeMemory = new byte[dataSize];
            InitializeDataTree(in node);
        }

        public void UpdateTree()
        {
            for (int i = 0; i < Data.NodesExecutiondata.Length; i++)
            {
                if (Data.NodeStates[i] != NodeState.Running)
                    Data.NodeStartTime[i] = Time.realtimeSinceStartup;
            }

            ref Node node = ref GameState.NodeManager.GetRef(RootNodeId);
            ActionManager.Action action = GameState.ActionManager.Get(node.ActionID);
            ulong ptr = Data.GetAddress();
            Data.NodeStates[0] = action(ptr, RootNodeId);
        }
    }
}
