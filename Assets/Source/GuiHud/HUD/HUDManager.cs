using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KGUI;
using Entitas;

namespace HUD
{
    public class HUDManager
    {
        // GUI Manager
        public GUIManager guiManager;

        // Constructor
        public void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            // Create GUI Manager
            guiManager = new GUIManager();

            // Initialize GUI Manager
            guiManager.Initialize(planet, agentEntity);
        }

        public void Update(AgentEntity agentEntity)
        {
            // Update GUI
            guiManager.Update(agentEntity);
        }

        public void Draw()
        {
            guiManager.Draw();
        }
    }
}
