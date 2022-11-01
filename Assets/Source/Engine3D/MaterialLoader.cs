//imports UnityEngine

using System;
using System.Collections.Generic;


namespace Engine3D
{
    public class MaterialLoader
    {
        public UnityEngine.Material[] ObjectArray;
        public Dictionary<string, int> ObjectID;

        public MaterialLoader()
        {
            ObjectArray = new UnityEngine.Material[1024];
            ObjectID = new Dictionary<string, int>();
        }

        public void Load(string filename, MaterialType materialType)
        {
            int index = (int)materialType;
            if (index < ObjectArray.Length)
            {
 
            }
            else
            {
                Array.Resize(ref ObjectArray, index * 2);
            }

            ObjectID.Add(filename, index);
            UnityEngine.Material prefab = (UnityEngine.Material)UnityEngine.Resources.Load(filename);
            ObjectArray[index] = prefab;
        }

        public ref UnityEngine.Material GetMaterial(MaterialType type)
        {
            return ref ObjectArray[(int)type];
        }
    }
}
