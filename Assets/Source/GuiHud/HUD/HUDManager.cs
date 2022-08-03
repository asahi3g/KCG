using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KGUI;
using Entitas;

namespace HUD
{
    public static class HUDManager
    {
        // GUI Manager
        public static GUIManager guiManager;

        // Constructor
        public static void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            // Create GUI Manager
            guiManager = new GUIManager();

            // Initialize GUI Manager
            guiManager.Initialize(planet, agentEntity);
        }

        public static void Update(AgentEntity agentEntity)
        {
            // Update GUI
            guiManager.Update(agentEntity);
        }
    }
}
