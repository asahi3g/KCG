namespace GameScreen
{


    public class GameScreen
    {


        public virtual void Draw() {}
        public virtual void Update() {}

        public virtual void OnGui() {}

        public virtual void OnDrawGizmos() {}

        public virtual void Init(UnityEngine.Transform sceneTransform) {}
        public virtual void LoadResources() {}
        public virtual void UnloadResources() {}
    }
}