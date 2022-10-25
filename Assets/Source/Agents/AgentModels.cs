using Engine3D;

namespace Agent
{
    public class AgentModels
    {
        private ModelLoader modelLoader;
        private MaterialLoader materialLoader;

        public AgentModels(ModelLoader modelLoader, MaterialLoader materialLoader)
        {
            this.modelLoader = modelLoader;
            this.materialLoader = materialLoader;
        }

        public void LoadModels()
        {
            modelLoader.Load("DefaultHumanoid", ModelType.DefaultHumanoid);
            modelLoader.Load("SmallInsect", ModelType.SmallInsect);
            modelLoader.Load("HeavyInsect", ModelType.HeavyInsect);
            modelLoader.Load("Stander", ModelType.Stander);
            modelLoader.Load("MediumMarinePrefab", ModelType.SpaceMarine);
        }

        public void LoadMaterials()
        {

        }
    }
}
