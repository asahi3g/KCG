//imports UnityEngine

using Agent;
using Animancer;
using System;
using Vehicle;

namespace Engine3D
{
    //TODO(): allow assets to be loaded on gameplay instead of loading at start
    //TODO(): has to print the time to load all the assets
    public class AssetManager
    {

        public static AssetManager AssetManagerSingelton;

        public static AssetManager Singelton
        {
            get
            {
                if (AssetManagerSingelton == null)
                {
                    AssetManagerSingelton = new AssetManager();
                }

                return AssetManagerSingelton;
            }
        }

        AnimationLoader AnimationLoader;
        ModelLoader ModelLoader;
        MaterialLoader MaterialLoader;

        //Agent
        AgentModels agentModels;
        AgentAnimations agentAnimations;

        //Vehicle
        VehicleModels vehicleModels;
        VehicleAnimations vehicleAnimations;

        public AssetManager()
        {
            AnimationLoader = new AnimationLoader();
            ModelLoader = new ModelLoader();
            MaterialLoader = new MaterialLoader();

            // Agent
            agentModels = new AgentModels(ModelLoader, MaterialLoader);
            agentAnimations = new AgentAnimations(AnimationLoader);

            // Vehicle
            vehicleModels = new VehicleModels(ModelLoader, MaterialLoader);
            vehicleAnimations = new VehicleAnimations(AnimationLoader);

            long beginTime = DateTime.Now.Ticks;
            LoadMaterials();
            LoadAnimations();
            LoadModels();
            UnityEngine.Debug.Log("3d Assets Loading Time: " + (DateTime.Now.Ticks - beginTime) / TimeSpan.TicksPerMillisecond + " miliseconds");
        }

        public ref UnityEngine.AnimationClip GetAnimationClip(AnimationType animationType)
        {
            return ref AnimationLoader.GetAnimationClip(animationType);
        }

        public ref UnityEngine.GameObject GetModel(ModelType modelType)
        {
            return ref ModelLoader.GetModel(modelType);
        }

        public ref UnityEngine.Material GetMaterial(MaterialType materialType)
        {
            return ref MaterialLoader.GetMaterial(materialType);
        }

        private void LoadMaterials()
        {
            // Agent
            agentModels.LoadMaterials();

            // Vehicle
            vehicleModels.LoadMaterials();
        }

        private void LoadAnimations()
        {
            // Agent
            agentAnimations.LoadAnimations();

            // Vehicle
            vehicleAnimations.LoadAnimations();
        }

        private void LoadModels()
        {
            // Agent
            agentModels.LoadModels();

            // Vehicle
            vehicleModels.LoadModels();

            ModelLoader.Load("Pistol", ModelType.Pistol);
            ModelLoader.Load("Rapier", ModelType.Rapier);

            ModelLoader.Load("Sword", ModelType.Sword);
            ModelLoader.Load("SpaceGun", ModelType.SpaceGun);
        }

    }
}