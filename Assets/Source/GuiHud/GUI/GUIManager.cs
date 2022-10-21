using System.Collections.Generic;
using Enums;
using KGUI.Elements;
using KMath;
using Planet;
using UnityEngine;
using UnityEngine.UI;

namespace KGUI
{
    public class GUIManager
    {
        public bool ShowGUI = true;
        
        public PlanetState Planet;
        public ItemInventoryEntity SelectedInventoryItem;

        public Sprite ProgressBar;
        public Sprite WhiteSquareBorder;

        public Dictionary<PanelEnums, PanelUI> PanelPrefabList = new();
        public Dictionary<PanelEnums, PanelUI> PanelList = new();

        public Vec2f CursorPosition;
        public ElementUI Cursor;
        
        private Canvas canvas;

        private TextWrapper text = new();

        GeometryGUI GeometryGUI;

        // Initialize
        public void InitStage1(PlanetState planet)
        {
            Planet = planet;
            
            UnityEngine.Cursor.visible = true;
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

            ProgressBar = GameState.Renderer.CreateSprite(
                "Assets\\StreamingAssets\\UserInterface\\Bars\\CircleBar\\hud_status_fill.png", 19, 19, AtlasType.Gui);
            WhiteSquareBorder = GameState.Renderer.CreateSprite(
                "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", 225, 225, AtlasType.Gui);

            PanelPrefabList.Add(PanelEnums.PlayerStatus, Resources.Load<PanelUI>("GUIPrefabs/PlayerStatusUI"));
            PanelPrefabList.Add(PanelEnums.PlacementTool, Resources.Load<PanelUI>("GUIPrefabs/PlacementToolUI"));
            PanelPrefabList.Add(PanelEnums.PlacementMaterialTool, Resources.Load<PanelUI>("GUIPrefabs/PlacementMaterialToolUI"));
            PanelPrefabList.Add(PanelEnums.PotionTool, Resources.Load<PanelUI>("GUIPrefabs/PotionToolUI"));

            GeometryGUI = new GeometryGUI();

            GeometryGUI.Initialize(ref planet);
        }

        public void InitStage2()
        {
            SetPanelActive(PanelEnums.PlayerStatus);
        }

        // Inputs panel's ID that we wanna enable or disable
        // UIPanelList.TryGetValue - checks if we have initialized our panel
        // Then if that panel initialized we are disable or enable(bool active)
        // If panel not Initialized, we are checking it in Prefab List(UIPanelPrefabList) with TryGetValue
        // If it's in Prefab List then instantiate it and enable. If we trying to actually disable it(active == false) then don't even instantiate
        // If we not have in UIPanelList or UIPanelPrefabList then error
        public void SetPanelActive(PanelEnums panelEnums, bool active = true)
        {
            if (PanelList.TryGetValue(panelEnums, out var panel))
            {
                if (!active)
                {
                    panel.OnDeactivate();
                }

                panel.gameObject.SetActive(active);

                if (active)
                {
                    panel.OnActivate();
                }
            }
            else if(PanelPrefabList.TryGetValue(panelEnums, out var panelPrefab))
            {
                if (active)
                {
                    var newPanel = Object.Instantiate(panelPrefab, canvas.transform);
                    newPanel.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogError("No such panel in prefab list");
            }
        }

        public void Update(AgentEntity agentEntity)
        {
            canvas.GetComponent<CanvasScaler>().referenceResolution =
                new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
            
            CursorPosition = new Vec2f(Input.mousePosition.x, Input.mousePosition.y);
            
            text.Update();
            GeometryGUI.Update(ref Planet, agentEntity);
            
            var inventoryID = agentEntity.agentInventory.InventoryID;
            var inventory = Planet.EntitasContext.inventory.GetEntityWithInventoryID(inventoryID);
            var selectedSlot = inventory.inventoryEntity.SelectedSlotID;
            SelectedInventoryItem = GameState.InventoryManager.GetItemInSlot(Planet.EntitasContext, inventoryID, selectedSlot);

            HandleMouseEvents();
        }

        public void Draw()
        {
            foreach (var panel in PanelList.Values)
            {
                foreach (var element in panel.UIElementList.Values)
                {
                    element.Draw();
                }
            }
        }

        public void HandleMouseEvents()
        {
            if (Cursor != null 
                && Collisions.Collisions.PointOverlapRect
                (
                    CursorPosition.X, CursorPosition.Y,
                    Cursor.HitBox.xmin, Cursor.HitBox.xmax, Cursor.HitBox.ymin, Cursor.HitBox.ymax)
               )
            {
                Cursor.OnMouseStay();
            }
            else
            {
                Cursor?.OnMouseExited();
                Cursor = null;
                foreach (var panel in PanelList.Values)
                {
                    if (!panel.gameObject.activeSelf) continue;
                    
                    foreach (var element in panel.UIElementList.Values)
                    {
                        if (!element.gameObject.activeSelf) continue;
                        
                        if (Collisions.Collisions.PointOverlapRect(CursorPosition.X, CursorPosition.Y, element.HitBox.xmin, element.HitBox.xmax, element.HitBox.ymin, element.HitBox.ymax))
                        {
                            Cursor = element;
                            Cursor.OnMouseEntered();
                            return;
                        }
                    }
                }
            }
            
            if (Input.GetMouseButton(0))
            {
                Cursor?.OnMouseClick();
            }
        }

        public void AddTemporaryText(string _text, Vec2f canvasPosition, Vec2f hudSize, float lifeTime)
        {
            text.Create("TempText", _text, canvas.transform, lifeTime);
            text.SetPosition(new Vector3(canvasPosition.X, canvasPosition.Y, 0.0f));
            text.SetSizeDelta(new Vector2(hudSize.X, hudSize.Y));
            text.StartLifeTime = true;
        }

        public TextWrapper AddText(string _text, Vec2f canvasPosition, Vec2f hudSize)
        {
            TextWrapper textWrapper = new TextWrapper();
            textWrapper.Create("TempText", _text, canvas.transform, 1);
            textWrapper.SetPosition(new Vector3(canvasPosition.X, canvasPosition.Y, 0.0f));
            textWrapper.SetSizeDelta(new Vector2(hudSize.X, hudSize.Y));

            return textWrapper;
        }
    }
}
