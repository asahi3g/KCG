using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Animancer;

namespace KGui
{



    public class CharacterModelDisplay
    {
    
        Camera OffscreenCamera; 

        Material Material;
        RenderTexture _Texture;
        Texture2D GuiTexture;

        public bool Enabled;
        public bool DebugDraw;

        public float x;
        public float y;
        public float width;
        public float height;


        GameObject Model;
        AnimancerComponent AnimancerComponent;

        public CharacterModelDisplay()
        {

            Material = Resources.Load<Material>("CamFeedMaterial");
            _Texture = Resources.Load<RenderTexture>("CamFeedTexture");

            Material.mainTexture = _Texture;

            Camera OffscreenCamera = Camera.Instantiate(Camera.main);
            OffscreenCamera.backgroundColor = new Color(0.75f, 0.75f, 0.75f, 0.0f);
            OffscreenCamera.name = "OffScreen camera";

            foreach (var comp in OffscreenCamera.GetComponents<Component>())
            {
                if (!(comp is Transform) && !(comp is Camera))
                {
                    Component.Destroy(comp);
                }
            }


            OffscreenCamera.transform.position = new Vector3(-1000.0f, -1000.0f, -10.0f);

            OffscreenCamera.targetTexture = _Texture;

            Enabled = false;
            DebugDraw = false;

            x = 100;
            y = 100;
            width = 128;
            height = 200;
        }

        public void SetModel(GameObject model)
        {
            if (Model != null)
            {
                GameObject.Destroy(Model);
            }

            Model = GameObject.Instantiate(model);
            Model.transform.position = new Vector3(-1000.0f - 2.5f, -1000.0f - 4.5f, 20.0f);
            Model.transform.localScale = new Vector3(1.5f * 4, 1.5f * 4, 1.5f * 4); 

            GameObject animancerComponentGO = new GameObject("AnimancerComponent", typeof(AnimancerComponent));
            animancerComponentGO.transform.parent = Model.transform;
            // get the animator component from the game object
            // this component is used by animancer
            AnimancerComponent = animancerComponentGO.GetComponent<AnimancerComponent>();
            AnimancerComponent.Animator = Model.GetComponent<Animator>();

            Model.transform.rotation = Quaternion.Euler(0, 140, 0);
            if (AnimancerComponent)
            {
                AnimationClip animation = Engine3D.AssetManager.Singelton.GetAnimationClip(Engine3D.AnimationType.SpaceMarineIdle);
                AnimancerComponent.Play(animation);
            }
        }

        public void Update()
        {
            if (Enabled)
            {
                if (GuiTexture != null)
                {
                    Texture2D.Destroy(GuiTexture);
                }

                GuiTexture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false);
                RenderTexture.active = _Texture;
                GuiTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
                GuiTexture.Apply();
          
            }
            
        }

        public void Draw()
        {
            if (Enabled)
            {
                GUI.DrawTexture(new Rect(x, y - height * 0.5f, width, height) , GuiTexture);
            }
        }
    }
}