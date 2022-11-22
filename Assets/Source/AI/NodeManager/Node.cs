﻿using System;
using Unity.Collections.LowLevel.Unsafe;

namespace NodeSystem
{
    public struct Node
    {
        public delegate int GetNextChild(in NodeManager nodeManager, ref Node node, ArraySegment<byte> data, NodeState lastResult);

        public int ID;
        public ItemUsageActionType  Type;
        public int ActionID;
        public int ConditionalID;
        public int[] Children;
        public int SubTreeNodeCount;

        public byte[] DataInit;

        // Used to set specific data for a node type.
        public void SetData<T>(ref T data) where T : struct
        {
            unsafe
            {
                DataInit = new byte[UnsafeUtility.SizeOf<T>()];
                Array.Fill<byte>(DataInit, 0);
                fixed (byte* dataPointer = &DataInit[0])
                {
                    UnsafeUtility.CopyStructureToPtr<T>(ref data, dataPointer);
                }
            }
        }
        // Add more data after SetData.
        public void AddData<T>(ref T data) where T : struct
        {
            int offset = DataInit.Length;
            unsafe
            {
                int dataLength = UnsafeUtility.SizeOf<T>() + offset;
                Array.Resize(ref DataInit, dataLength);
                for (int i = offset; i < dataLength; i++)
                {
                    DataInit[i] = 0;
                }
                fixed (byte* dataPointer = &DataInit[offset])
                {
                    UnsafeUtility.CopyStructureToPtr<T>(ref data, dataPointer);
                }
            }
        }

        // Cast byte[] to type T.
        static public ref T CastTo<T>(byte[] data, int offset) where T : struct
        {
            unsafe
            {
                fixed (byte* dataPointer = &data[offset])
                {
                    return ref UnsafeUtility.AsRef<T>(dataPointer);
                }
            }
        }
    }

    // special value for child indices
    public class BTSpecialChild
    {
        public const int ReturnToParent = -2; // return to parent node
        public const int NotInitialized = -1;
    }
}
