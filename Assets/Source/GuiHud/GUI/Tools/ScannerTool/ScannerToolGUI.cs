using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KGUI.Elements;

namespace KGUI.Tools
{
    public class ScannerToolGUI : GUIManager
    {
        public Text text = new Text();

        public bool isActive = true;


        public override void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
        }

        public void ActivateGUIOverlay(bool gotSeed, int lightStatus, float waterStatus, float growthStatus, float lifeTime)
        {
            if(isActive)
            {
                text.Create("ScannerTool", "Got Seed: " + gotSeed.ToString() + " \n" + "Light Status: " + lightStatus + " \n" + "Water Status: " + (int)waterStatus + " \n"
                     + "Growth Status: " + (int)growthStatus, GameObject.Find("Canvas").transform, lifeTime);
                text.SetSizeDelta(new Vector2(350, 120));
                text.SetPosition(new Vector3(-160f, 90.0f, 0.0f));
                text.startLifeTime = true;
            }
        }

        public override void Update(AgentEntity agentEntity)
        {
            // Set Object Position
            if(text != null && text.GetTransform() != null)
            {
                ObjectPosition = new KMath.Vec2f(text.GetTransform().position.x, text.GetTransform().position.y);
                text.Update();
            }
        }

        public override void OnMouseClick(AgentEntity agentEntity)
        {
            
        }

        public override void OnMouseEnter()
        {

        }

        public override void OnMouseStay()
        {

        }

        public override void OnMouseExit()
        {

        }
    }
}
