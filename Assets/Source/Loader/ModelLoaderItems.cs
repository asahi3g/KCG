using System.Collections.Generic;
using Engine3D;
using UnityEngine;

namespace Loader
{
    public class ModelLoaderItems : ModelLoader<ItemModelType, AgentEquippedItemRenderer>
    {
        public ModelLoaderItems(ResourcesLoader<AgentEquippedItemRenderer> resourcesLoader) : base(resourcesLoader) { }

        protected override void OnLoadAll(IEnumerable<AgentEquippedItemRenderer> values, Dictionary<ItemModelType, AgentEquippedItemRenderer> map)
        {
            foreach (AgentEquippedItemRenderer value in values)
            {
                ItemModelType modelType = value.GetModelType();

                if (modelType == ItemModelType.Unknown)
                {
                    Debug.LogWarning(DebugExtensions.Format(GetType(), $"prefab '{value.gameObject.name}' modelType is '{modelType}' which is not supported, skipping.."));
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
