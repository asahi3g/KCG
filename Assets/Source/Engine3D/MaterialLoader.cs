using UnityEngine;
using System;
using System.Collections.Generic;
using Enums.Tile;
using KMath;


namespace Engine3D
{
    public class MaterialLoader
    {
        public Material[] ObjectArray;
        public Dictionary<string, int> ObjectID;

        public MaterialLoader()
        {
            ObjectArray = new Material[1024];
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
            Material prefab = (Material)Resources.Load(filename);
            ObjectArray[index] = prefab;
        }

        public ref Material GetMaterial(MaterialType type)
        {
            return ref ObjectArray[(int)type];
        }
    }
}
