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

        public static bool ShowGUI = true;

        public static void InitStage1()
        {
            // Create GUI Manager
            guiManager = new GUIManager();
        }

        // Constructor
        public static void InitStage2(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            if (guiManager == null)
                return;

            // Initialize GUI Manager
            guiManager.Initialize(planet, agentEntity);
        }

        public static void Update(AgentEntity agentEntity)
        {
            if (guiManager == null)
                return;

            // Update GUI
            guiManager.Update(agentEntity);
        }

        public static void Draw()
        {
            if (guiManager == null)
                return;

            guiManager.Draw();
        }
    }
}
