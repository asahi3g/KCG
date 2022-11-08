//imports UnityEngine

using System.Collections.Generic;
using KMath;
using Utility;

/*

Fields:
    ShowGUI
    
    SelectedInventoryItem - our current selected item on inventory slot
    
    ProgressBar - generic sprite that needed to create a border/imageWrapper for ProgressBar class
    WhiteSquareBorder - generic border sprite that added to atlas, represents white square

    

*/

namespace KGUI
{
    public class GUIManager
    {
        public bool ShowGUI = true;
        
        public ItemInventoryEntity SelectedInventoryItem;

        public UnityEngine.Sprite ProgressBar;
        public UnityEngine.Sprite WhiteSquareBorder;

        
        public Dictionary<PanelEnums, PanelUI> PanelList = new Dictionary<PanelEnums, PanelUI>();

        public Vec2f CursorPosition;
        public ElementUI ElementUnderCursor;
        public PanelUI PanelUnderCursor;
        
        private UnityEngine.Canvas canvas;

        private TextWrapper text = new TextWrapper();
        
        public void InitStage1()
        {
            UnityEngine.Cursor.visible = true;
            canvas = UnityEngine.GameObject.Find("Canvas").GetComponent<UnityEngine.Canvas>();
        }

        public void InitStage2()
        {
            ProgressBar = GameState.Renderer.CreateSprite(
                "Assets\\StreamingAssets\\UserInterface\\Bars\\CircleBar\\hud_status_fill.png", 19, 19, Enums.AtlasType.Gui);
            WhiteSquareBorder = GameState.Renderer.CreateSprite(
                "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", 225, 225, Enums.AtlasType.Gui);
            
            GameState.PrefabManager.InitializeResources();

            SetPanelActive(PanelEnums.PlayerStatus);
        }

        // Inputs panel's ID that we wanna enable or disable
        // UIPanelList.TryGetValue - checks if we have initialized our panel
        // Then if that panel initialized we are disable or enable(bool active)
        // If panel not Initialized, we are checking it in Prefab List(UIPanelPrefabList) with TryGetValue
        // If it's in Prefab List then instantiate it and enable. If we trying to actually disable it(active == false) then don't even instantiate
        // If we not have in UIPanelList or UIPanelPrefabList then error
        public void SetPanelActive(PanelEnums panelID, bool active = true)
        {
            if (PanelList.TryGetValue(panelID, out var panel))
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
                return;
            }
            
            var panelPrefab = GameState.PrefabManager.GetPanelPrefab(panelID);
            
            if(panelPrefab != default)
            {
                if (active)
                {
                    var newPanel = UnityEngine.Object.Instantiate(panelPrefab, canvas.transform);
                    newPanel.gameObject.SetActive(true);
                }
            }
            else
            {
                UnityEngine.Debug.LogError("No such panel in prefab list");
            }
        }

        public void Update(AgentEntity agentEntity)
        {
            canvas.GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution =
                new UnityEngine.Vector2(UnityEngine.Camera.main.pixelWidth, UnityEngine.Camera.main.pixelHeight);
            
            CursorPosition = new Vec2f(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);
            
            text.Update();

            HandleMouseEvents();
        }

        public void Draw()
        {
            foreach (var panel in PanelList.Values)
            {
                foreach (var element in panel.ElementList.Values)
                {
                    element.Draw();
                }
            }
        }

        public void HandleMouseEvents()
        {
            if (ElementUnderCursor != null && Collisions.Collisions.PointOverlapRect(
                    CursorPosition.X, CursorPosition.Y,
                    ElementUnderCursor.HitBox.xmin, ElementUnderCursor.HitBox.xmax, ElementUnderCursor.HitBox.ymin, ElementUnderCursor.HitBox.ymax))
            {
                ElementUnderCursor.OnMouseStay();
            }
            else
            {
                ElementUnderCursor?.OnMouseExited();
                ElementUnderCursor = null;
                PanelUnderCursor = null;
                foreach (var panel in PanelList.Values)
                {
                    if (!panel.gameObject.activeSelf) continue;
                    if (ElementUnderCursor != null) break;
                    
                    foreach (var element in panel.ElementList.Values)
                    {
                        if (!element.gameObject.activeSelf) continue;
                        
                        if (Collisions.Collisions.PointOverlapRect(CursorPosition.X, CursorPosition.Y, element.HitBox.xmin, element.HitBox.xmax, element.HitBox.ymin, element.HitBox.ymax))
                        {
                            ElementUnderCursor = element;
                            PanelUnderCursor = panel;
                            ElementUnderCursor.OnMouseEntered();
                            break;
                        }
                    }
                }
            }

            if (ElementUnderCursor != null && PanelUnderCursor != null && UnityEngine.Input.GetMouseButton(0))
            {
                PanelUnderCursor.HandleClickEvent(ElementUnderCursor.ID);
                ElementUnderCursor.OnMouseClick();
            }
        }

        public TextWrapper AddText(string _text, Vec2f canvasPosition, Vec2f hudSize)
        {
            if (GameState.Planet.TileMap != null)
            {
                TextWrapper textWrapper = new TextWrapper();
                textWrapper.Create("TempText", _text, canvas.transform, 1);
                textWrapper.SetPosition(new UnityEngine.Vector3(canvasPosition.X, canvasPosition.Y, 0.0f));
                textWrapper.SetSizeDelta(new UnityEngine.Vector2(hudSize.X, hudSize.Y));
                return textWrapper;
            }

            return null;
        }
    }
}
