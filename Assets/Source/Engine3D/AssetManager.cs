//imports UnityEngine

using Agent;
using System;
using Loader;
using UnityEngine;

namespace Engine3D
{
    //TODO(): allow assets to be loaded on gameplay instead of loading at start
    //TODO(): has to print the time to load all the assets
    public class AssetManager
    {

        public static AssetManager AssetManagerSingelton;
        AnimationLoader AnimationLoader;
        MaterialLoader MaterialLoader;
        private ModelLoaderAgents _modelLoaderAgents;
        AgentAnimations agentAnimations;
        private ModelLoaderItems _modelLoaderItems;
        private UIHitpoints _prefabUIHitpoints;

        public static AssetManager Singelton
        {
            get
            {
                if (AssetManagerSingelton == null) AssetManagerSingelton = new AssetManager();
                return AssetManagerSingelton;
            }
        }

        public UIHitpoints GetPrefabUIHitpoints() => _prefabUIHitpoints;


        public AssetManager()
        {
            AnimationLoader = new AnimationLoader();
            MaterialLoader = new MaterialLoader();
            
            _modelLoaderAgents = new ModelLoaderAgents(new ResourcesLoader<AgentRenderer>("Agents"));
            agentAnimations = new AgentAnimations(AnimationLoader);
            _modelLoaderItems = new ModelLoaderItems(new ResourcesLoader<ItemRenderer>("Items"));

            LoadAll();
        }

        public ref UnityEngine.AnimationClip GetAnimationClip(AnimationType animationType)
        {
            return ref AnimationLoader.GetAnimationClip(animationType);
        }

        public bool GetPrefabAgent(AgentModelType modelType, out AgentRenderer agentRenderer)
        {
            return _modelLoaderAgents.Get(modelType, out agentRenderer);
        }
        
        public bool GetPrefabItem(ItemModelType modelType, out ItemRenderer itemRenderer)
        {
            return _modelLoaderItems.Get(modelType, out itemRenderer);
        }

        public ref UnityEngine.Material GetMaterial(MaterialType materialType)
        {
            return ref MaterialLoader.GetMaterial(materialType);
        }

        private void LoadAll()
        {
            long beginTime = DateTime.Now.Ticks;
            
            agentAnimations.LoadAnimations();
            _modelLoaderAgents.LoadAll();
            _modelLoaderItems.LoadAll();
            LoadUIHitpoints();

            UnityEngine.Debug.Log(DebugExtensions.Format(typeof(AssetManager), $"initialized, loading time: {((DateTime.Now.Ticks - beginTime) / TimeSpan.TicksPerMillisecond)} milliseconds"));
        }

        private void LoadUIHitpoints()
        {
            ResourcesLoader<UIHitpoints> resourcesLoader = new ResourcesLoader<UIHitpoints>("UI");
            resourcesLoader.LoadAll();
            resourcesLoader.GetFirst(out _prefabUIHitpoints);
        }

    }
}