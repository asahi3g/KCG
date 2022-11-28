using System.Collections.Generic;
using Engine3D;
using UnityEngine;

namespace Loader
{
    public class ModelLoaderAgents : ModelLoader<AgentModelType, AgentRenderer>
    {
        
        public ModelLoaderAgents(ResourcesLoader<AgentRenderer> resourcesLoader) : base(resourcesLoader) { }
        
        protected override void OnLoadAll(IEnumerable<AgentRenderer> values, Dictionary<AgentModelType, AgentRenderer> map)
        {
            foreach (AgentRenderer value in values)
            {
                AgentModelType modelType = value.GetModelType();

                if (modelType == AgentModelType.Unknown)
                {
                    Debug.LogWarning(DebugExtensions.Format(GetType(),$"prefab '{value.gameObject.name}' modelType is '{modelType}' which is not supported, skipping.."));
                    continue;
                }
 
                if (map.ContainsKey(modelType))
                {
                    Debug.LogWarning(DebugExtensions.Format(GetType(),$"multiple prefabs with modelType '{modelType}' detected, skipping.."));
                    continue;
                }
                
                map.Add(modelType, value);
            }
        }
    }
}
