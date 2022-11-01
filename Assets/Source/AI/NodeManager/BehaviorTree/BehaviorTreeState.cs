using System;
using Unity.Collections.LowLevel.Unsafe;

namespace NodeSystem.BehaviorTree
{
    public struct NodeExcutionData
    {
        public int Id;              // Id in NodeManager.
        public int MemoryOffset;    // Offset used to access memory.
        public float ExecutionTime;   // Time in ticks since start running.
    }
    public struct BehaviorTreeState
    {
        public int AgentID;
        public byte[] NodeMemory;
        public NodeExcutionData[] NodesExecutiondata;
        public int BlackboardID; // -1 if there is no blackboard.

        public ref T GetNodeData<T>(int index) where T : struct
            => ref Node.CastTo<T>(NodeMemory, NodesExecutiondata[index].MemoryOffset);

        public ref T GetActionSequenceData<T>(int index) where T : struct
           => ref Node.CastTo<T>(NodeMemory, NodesExecutiondata[index].MemoryOffset + 1);

        public bool HasBlackboard() => (BlackboardID != -1);
        public void ResetNodeData(int index)
        {
            int id = NodesExecutiondata[index].Id;
            ref Node node = ref GameState.NodeManager.GetRef(id);
            if (node.DataInit == null)
                return;
            Array.Copy(node.DataInit, 0, NodeMemory, NodesExecutiondata[index].MemoryOffset, node.DataInit.Length);
        }

        public void ResetNodeMemoryData()
        {
            for (int i = 0; i < NodesExecutiondata.Length; i++)
            {
                ResetNodeData(i);
            }
        }

        // Get pointer to BehaviorTreeState.
        public ulong GetAddress()
        {
            unsafe 
            {
                return (ulong)UnsafeUtility.AddressOf<BehaviorTreeState>(ref this);
            }
        }

        // Get ref from address.
        public static ref BehaviorTreeState GetRef(ulong ptr)
        {
            unsafe
            {
                void* dataPointer = (void*)ptr;
                return ref UnsafeUtility.AsRef<BehaviorTreeState>(dataPointer);
            }
        }
    }
}
