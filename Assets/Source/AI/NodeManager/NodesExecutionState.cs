using System;
using NodeSystem;
using Unity.Collections.LowLevel.Unsafe;

namespace NodeSystem
{
    public struct NodeExcutionData
    {
        public int Id;                  // Id in NodeManager.
        public int MemoryOffset;        // Offset used to access memory.
        public float ExecutionTime;     // Time in ticks since start running.
    }
    public struct NodesExecutionState
    {
        public int AgentID;
        public byte[] NodeMemory;
        public NodeExcutionData[] NodesExecutiondata;

        public ref T GetNodeData<T>(int index) where T : struct
            => ref NodeSystem.Node.CastTo<T>(NodeMemory, NodesExecutiondata[index].MemoryOffset);

        public ref T GetActionSequenceData<T>(int index) where T : struct
           => ref NodeSystem.Node.CastTo<T>(NodeMemory, NodesExecutiondata[index].MemoryOffset + 1);

        public void ResetNodeData(int index)
        {
            int id = NodesExecutiondata[index].Id;
            ref NodeSystem.Node node = ref GameState.NodeManager.GetRef(id);
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

        // Get pointer to NodesExecutionState.
        public ulong GetAddress()
        {
            unsafe 
            {
                return (ulong)UnsafeUtility.AddressOf<NodesExecutionState>(ref this);
            }
        }

        // Get ref from address.
        public static ref NodesExecutionState GetRef(ulong ptr)
        {
            unsafe
            {
                void* dataPointer = (void*)ptr;
                return ref UnsafeUtility.AsRef<NodesExecutionState>(dataPointer);
            }
        }
    }
}
