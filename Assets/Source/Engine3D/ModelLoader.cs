//import UnityEngine

using System;
using System.Collections.Generic;


namespace Engine3D
{
    public class ModelLoader
    {
        public UnityEngine.GameObject[] ObjectArray;
        public Dictionary<string, int> ObjectID;

        public ModelLoader()
        {
            ObjectArray = new UnityEngine.GameObject[1024];
            ObjectID = new Dictionary<string, int>();
        }

        public void Load(string filename, ModelType modelType)
        {
            int index = (int)modelType;
            if (index < ObjectArray.Length)
            {
 
            }
            else
            {
                Array.Resize(ref ObjectArray, index * 2);
            }

            ObjectID.Add(filename, index);
            UnityEngine.GameObject prefab = (UnityEngine.GameObject)UnityEngine.Resources.Load(filename);
            ObjectArray[index] = prefab;
        }

        public ref UnityEngine.GameObject GetModel(ModelType modelType)
        {
            return ref ObjectArray[(int)modelType];
        }
    }
}
