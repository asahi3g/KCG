//imports UnityEngine

using System;
using System.Collections.Generic;

namespace Engine3D
{
    public class AnimationLoader
    {
        public UnityEngine.AnimationClip[] ClipArray;
        public Dictionary<string, int> AnimationID;

        public AnimationLoader()
        {
            ClipArray = new UnityEngine.AnimationClip[1024];
            AnimationID = new Dictionary<string, int>();
        }

        public void Load(string filename, AnimationType animationType)
        {
            int index = (int)animationType;
            if (index < ClipArray.Length)
            {
                
            }
            else
            {
                Array.Resize(ref ClipArray, index * 2);
            }

            AnimationID.Add(filename, index);
            UnityEngine.AnimationClip animation = (UnityEngine.AnimationClip)UnityEngine.Resources.Load(filename, typeof(UnityEngine.AnimationClip));
            ClipArray[index] = animation;
        }

        public ref UnityEngine.AnimationClip GetAnimationClip(AnimationType animationType)
        {
            return ref ClipArray[(int)animationType];
        }
    }
}
