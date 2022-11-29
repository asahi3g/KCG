//imports UnityEngine

using Animancer;

namespace KGui
{
    public class CharacterModelDisplay
    {

        UnityEngine.Camera OffscreenCamera; 

        UnityEngine.Material Material;
        UnityEngine.RenderTexture _Texture;
        UnityEngine.Texture2D GuiTexture;

        public bool Enabled;
        public bool DebugDraw;

        public float x;
        public float y;
        public float width;
        public float height;


        UnityEngine.GameObject Model;
        AnimancerComponent AnimancerComponent;

        public CharacterModelDisplay()
        {

            Material = UnityEngine.Resources.Load<UnityEngine.Material>("CamFeedMaterial");
            _Texture = UnityEngine.Resources.Load<UnityEngine.RenderTexture>("CamFeedTexture");

            Material.mainTexture = _Texture;

            UnityEngine.Camera OffscreenCamera = UnityEngine.Object.Instantiate(UnityEngine.Camera.main);
            OffscreenCamera.backgroundColor = new UnityEngine.Color(0.2f, 0.2f, 0.2f, 0.0f);
            OffscreenCamera.name = "OffScreen camera";

            foreach (var comp in OffscreenCamera.GetComponents<UnityEngine.Component>())
            {
                if (!(comp is UnityEngine.Transform) && !(comp is UnityEngine.Camera))
                {
                    UnityEngine.Object.Destroy(comp);
                }
            }


            OffscreenCamera.transform.position = new UnityEngine.Vector3(-1000.0f, -1000.0f, -10.0f);

            OffscreenCamera.targetTexture = _Texture;

            Enabled = false;
            DebugDraw = false;

            x = 100;
            y = 100;
            width = 128;
            height = 200;
        }

        public void SetModel(UnityEngine.GameObject gameObject)
        {
            if (Model != null)
            {
                UnityEngine.Object.Destroy(Model);
            }

            Model = UnityEngine.Object.Instantiate(gameObject);
            Model.transform.position = new UnityEngine.Vector3(-1000.0f - 2.5f, -1000.0f - 4.5f, 20.0f);
            Model.transform.localScale = new UnityEngine.Vector3(1.5f * 4, 1.5f * 4, 1.5f * 4);

            UnityEngine.GameObject animancerComponentGO = new UnityEngine.GameObject("AnimancerComponent", typeof(AnimancerComponent));
            animancerComponentGO.transform.parent = Model.transform;
            // get the animator component from the game object
            // this component is used by animancer
            AnimancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
            AnimancerComponent.Animator = Model.GetComponent<UnityEngine.Animator>();

            Model.transform.rotation = UnityEngine.Quaternion.Euler(0, 135, 0);
            if (AnimancerComponent)
            {
                UnityEngine.AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineIdle);
                AnimancerComponent.Play(animation);
            }
        }

        public void Update()
        {
            if (Enabled)
            {
                if (GuiTexture != null)
                {
                    UnityEngine.Object.Destroy(GuiTexture);
                }

                GuiTexture = new UnityEngine.Texture2D((int)width, (int)height, UnityEngine.TextureFormat.RGBA32, false);
                UnityEngine.RenderTexture.active = _Texture;
                GuiTexture.ReadPixels(new UnityEngine.Rect(0, 0, width, height), 0, 0);
                GuiTexture.Apply();
          
            }
            
        }

        public void Draw()
        {
            if (Enabled)
            {
                UnityEngine.GUI.DrawTexture(new UnityEngine.Rect(x, y - height * 0.5f, width, height) , GuiTexture);
            }
        }
    }
}