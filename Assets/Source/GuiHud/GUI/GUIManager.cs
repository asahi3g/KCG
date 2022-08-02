using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KMath;

namespace KGUI
{
    public class GUIManager
    {
        // Food Bar
        public KGUI.PlayerStatus.FoodBarUI foodBarUI;

        // Water Bar
        public KGUI.PlayerStatus.WaterBarUI waterBarUI;

        // Oxygen Bar
        public KGUI.PlayerStatus.OxygenBarUI oxygenBarUI;

        // Fuel Bar
        public KGUI.PlayerStatus.FuelBarUI fuelBarUI;

        // Health Bar
        public KGUI.PlayerStatus.HealthBarUI healthBarUI;

        // Bedrock
        public KGUI.Tiles.Bedrock bedrockUI;

        // Dirt
        public KGUI.Tiles.Dirt dirtUI;

        // Wire
        public KGUI.Tiles.Wire wireUI;

        // Pipe
        public KGUI.Tiles.Pipe pipeUI;

        // Scanner Tool
        public KGUI.Tools.ScannerToolGUI scannerToolGUI;

        // GUI Elements List
        public List<GUIManager> UIList = new List<GUIManager>();

        // Cursor Screen Position from Unity Input Class
        public Vec2f CursorPosition;

        // Object Screen Position
        public Vec2f ObjectPosition;

        // Run Various Functions One Time
        private bool CanRun = true;

        // UI Scaling
        private float HUDScale = 1.0f;

        // Canvas
        Canvas _Canvas;

        // Initialize
        public virtual void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            foodBarUI = new PlayerStatus.FoodBarUI();
            waterBarUI = new PlayerStatus.WaterBarUI();
            oxygenBarUI = new PlayerStatus.OxygenBarUI();
            fuelBarUI = new PlayerStatus.FuelBarUI();
            healthBarUI = new PlayerStatus.HealthBarUI();
            bedrockUI = new Tiles.Bedrock();
            dirtUI = new Tiles.Dirt();
            wireUI = new Tiles.Wire();
            pipeUI = new Tiles.Pipe();
            scannerToolGUI = new Tools.ScannerToolGUI();

            // Set Canvas
            _Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

            // Set HUD Scale
            _Canvas.scaleFactor = HUDScale;

            // Add Food Bar To Draw Array
            UIList.Add(foodBarUI);

            // Add Water Bar To Draw Array
            UIList.Add(waterBarUI);

            // Add Oxygen Bar To Draw Array
            UIList.Add(oxygenBarUI);

            // Add Fuel Bar To Draw Array
            UIList.Add(fuelBarUI);

            // Add Health Bar To Draw Array
            UIList.Add(healthBarUI);

            // Add Bedrock Tile To Draw Array
            UIList.Add(bedrockUI);

            // Add Dirt Tile To Draw Array
            UIList.Add(dirtUI);

            // Add Wire Tile To Draw Array
            UIList.Add(wireUI);

            // Add Pipe Tile To Draw Array
            UIList.Add(pipeUI);

            // Add Scanner Tool To Draw Array
            UIList.Add(scannerToolGUI);

            // Init Elements
            for (int i = 0; i < UIList.Count; i++)
            {
                UIList[i].Initialize(planet, agentEntity);
            }
        }

        public virtual void Update(AgentEntity agentEntity)
        {
            // Update HUD Scale
            _Canvas.scaleFactor = HUDScale;

            // Update Elements
            for (int i = 0; i < UIList.Count; i++)
            {
                UIList[i].Update(agentEntity);
            }

            // Assign New Cursor Position
            CursorPosition = new Vec2f(Input.mousePosition.x, Input.mousePosition.y);

            // Handle Inputs
            HandleInputs(agentEntity);
        }

        public void HandleInputs(AgentEntity agentEntity)
        {
            // On Mouse Click Event
            OnMouseClick(agentEntity);

            // On Mouse Stay Event
            OnMouseStay();
        }

        public void HandleIndicators()
        {
            if(foodBarUI.foodBar._fillValue < 50)
            {
                // TODO: Show Indicator Icon
            }
            else
            {
                // TODO: Don't show indicator Icon
            }

            if (waterBarUI.waterBar._fillValue < 50)
            {
                // TODO: Show Indicator Icon
            }
            else
            {
                // TODO: Don't show indicator Icon
            }
        }

        public virtual void OnMouseClick(AgentEntity agentEntity)
        {
            // Handle Inputs
            for (int i = 0; i < UIList.Count; i++)
            {
                if (Vector2.Distance(new Vector2(CursorPosition.X, CursorPosition.Y), new Vector2(UIList[i].ObjectPosition.X, UIList[i].ObjectPosition.Y)) < 20.0f)
                {
                    // If Mosue 0 Button Down
                    if (Input.GetMouseButton(0))
                    {
                        // Call All Click Events
                        UIList[i].OnMouseClick(agentEntity);
                    }
                }
            }
        }

        public virtual void OnMouseEnter()
        {
            // Handle Inputs
            for (int i = 0; i < UIList.Count; i++)
            {
                // If Condition Is True
                if(UIList[i].CanRun)
                {
                    // Set Condition Bool
                    UIList[i].CanRun = false;

                    // If Cursor Is In UI Element
                    if (Vector2.Distance(new Vector2(CursorPosition.X, CursorPosition.Y), new Vector2(UIList[i].ObjectPosition.X, UIList[i].ObjectPosition.Y)) < 20.0f)
                    {
                        // Run Mouse Enter Event
                        UIList[i].OnMouseEnter();
                    }
                }
            }
        }

        public virtual void OnMouseStay()
        {
            // Handle Inputs
            for (int i = 0; i < UIList.Count; i++)
            {
                // Check If Cursor Is On UI Element
                if (Vector2.Distance(new Vector2(CursorPosition.X, CursorPosition.Y), new Vector2(UIList[i].ObjectPosition.X, UIList[i].ObjectPosition.Y)) < 20.0f)
                {
                    // Run Mouse Enter Event
                    OnMouseEnter();

                    // Call Mouse Stay Event For Each
                    UIList[i].OnMouseStay();
                }
                else
                {
                    // Set Condition to True
                    UIList[i].CanRun = true;

                    OnMouseExit();
                }
            }
        }

        public virtual void OnMouseExit()
        {
            // Handle Inputs
            for (int i = 0; i < UIList.Count; i++)
            {
                // On Mouse Exit
                UIList[i].OnMouseExit();
            }
        }
    }
}
