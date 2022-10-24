//imports UnityEngine

using System.Collections.Generic;
using Enums;
using KMath;
using Planet;
using UnityEngine.UI;
using Text = KGUI.Elements.Text;

namespace KGUI
{
    public class GUIManager
    {
        public bool ShowGUI = true;
        
        public PlanetState Planet;
        public ItemInventoryEntity SelectedInventoryItem;

        public UnityEngine.Sprite ProgressBar;
        public UnityEngine.Sprite WhiteSquareBorder;

        public Dictionary<UIPanelID, UIPanel> UIPanelPrefabList = new();
        public Dictionary<UIPanelID, UIPanel> UIPanelList = new();

        public Vec2f CursorPosition;
        public UIElement CursorElement;
        
        private UnityEngine.Canvas canvas;

        private Text scannerText = new();

        GeometryGUI GeometryGUI;

        // Initialize
        public void InitStage1(PlanetState planet)
        {
            Planet = planet;

            UnityEngine.Cursor.visible = true;
            canvas = UnityEngine.GameObject.Find("Canvas").GetComponent<UnityEngine.Canvas>();

            ProgressBar = GameState.Renderer.CreateSprite(
                "Assets\\StreamingAssets\\UserInterface\\Bars\\CircleBar\\hud_status_fill.png", 19, 19, AtlasType.Gui);
            WhiteSquareBorder = GameState.Renderer.CreateSprite(
                "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", 225, 225, AtlasType.Gui);

            UIPanelPrefabList.Add(UIPanelID.PlayerStatus, UnityEngine.Resources.Load<UIPanel>("GUIPrefabs/PlayerStatusUI"));
            UIPanelPrefabList.Add(UIPanelID.PlacementTool, UnityEngine.Resources.Load<UIPanel>("GUIPrefabs/PlacementToolUI"));
            UIPanelPrefabList.Add(UIPanelID.PlacementMaterialTool, UnityEngine.Resources.Load<UIPanel>("GUIPrefabs/PlacementMaterialToolUI"));
            UIPanelPrefabList.Add(UIPanelID.PotionTool, UnityEngine.Resources.Load<UIPanel>("GUIPrefabs/PotionToolUI"));

            GeometryGUI = new GeometryGUI();

            GeometryGUI.Initialize(ref planet);
        }

        public void InitStage2()
        {
            SetPanelActive(UIPanelID.PlayerStatus);
        }

        public void SetPanelActive(UIPanelID panelID, bool active = true)
        {
            if (UIPanelList.TryGetValue(panelID, out var panel))
            {
                if (!active)
                {
                    panel.OnDeactivate();
                }

                panel.gameObject.SetActive(active);
            }
            else if(UIPanelPrefabList.TryGetValue(panelID, out var panelPrefab))
            {
                if (active)
                {
                    UnityEngine.Object.Instantiate(panelPrefab, canvas.transform);
                }
            }
            else
            {
                UnityEngine.Debug.LogError("No such panel in prefab list");
            }
        }

        public void Update(AgentEntity agentEntity)
        {
            canvas.GetComponent<CanvasScaler>().referenceResolution =
                new UnityEngine.Vector2(UnityEngine.Camera.main.pixelWidth, UnityEngine.Camera.main.pixelHeight);
            
            CursorPosition = new Vec2f(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
            
            scannerText.Update();
            GeometryGUI.Update(ref Planet, agentEntity);
            
            var inventoryID = agentEntity.agentInventory.InventoryID;
            var inventory = Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            var selectedSlot = inventory.inventoryEntity.SelectedSlotID;
            SelectedInventoryItem = GameState.InventoryManager.GetItemInSlot(Planet.EntitasContext, inventoryID, selectedSlot);

            HandleMouseEvents();
        }

        public void Draw()
        {
            foreach (var panel in UIPanelList.Values)
            {
                foreach (var element in panel.UIElementList.Values)
                {
                    element.Draw();
                }
            }
        }

        public void HandleMouseEvents()
        {
            if (CursorElement != null 
                && Collisions.Collisions.PointOverlapRect
                (
                    CursorPosition.X, CursorPosition.Y,
                    CursorElement.HitBox.xmin, CursorElement.HitBox.xmax, CursorElement.HitBox.ymin, CursorElement.HitBox.ymax)
               )
            {
                CursorElement.OnMouseStay();
            }
            else
            {
                CursorElement?.OnMouseExited();
                CursorElement = null;
                foreach (var panel in UIPanelList.Values)
                {
                    if (!panel.gameObject.activeSelf) continue;
                    
                    foreach (var element in panel.UIElementList.Values)
                    {
                        if (!element.gameObject.activeSelf) continue;
                        
                        if (Collisions.Collisions.PointOverlapRect(CursorPosition.X, CursorPosition.Y, element.HitBox.xmin, element.HitBox.xmax, element.HitBox.ymin, element.HitBox.ymax))
                        {
                            CursorElement = element;
                            CursorElement.OnMouseEntered();
                            return;
                        }
                    }
                }
            }
            
            if (UnityEngine.Input.GetMouseButton(0))
            {
                CursorElement?.OnMouseClick();
            }
        }

        public void AddScannerText(string _text, Vec2f canvasPosition, Vec2f hudSize, float lifeTime)
        {
            // Create Scanner Text
            scannerText.Create("TempText", _text, canvas.transform, lifeTime);
            scannerText.SetPosition(new UnityEngine.Vector3(canvasPosition.X, canvasPosition.Y, 0.0f));
            scannerText.SetSizeDelta(new UnityEngine.Vector2(hudSize.X, hudSize.Y));
            scannerText.StartLifeTime = true;
        }

        public Text AddText(string _text, Vec2f canvasPosition, Vec2f hudSize)
        {
            // Add Temporary text
            Text text = new Text();
            text.Create("TempText", _text, canvas.transform, 1);
            text.SetPosition(new UnityEngine.Vector3(canvasPosition.X, canvasPosition.Y, 0.0f));
            text.SetSizeDelta(new UnityEngine.Vector2(hudSize.X, hudSize.Y));

            return text;
        }
    }
}
