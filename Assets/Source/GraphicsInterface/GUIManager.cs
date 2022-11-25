//imports UnityEngine

using System.Collections.Generic;
using KMath;
using Utility;

/*

Fields:
    ShowGUI
    
    SelectedInventoryItem   - our current selected item on inventory slot
    
    ProgressBar             - generic sprite that needed to create a border/imageWrapper for ProgressBar class
    WhiteSquareBorder       - generic border sprite that added to atlas, represents white square

    PanelList               - list of initialized panels

    ElementUnderCursor      - current hovered element under cursor
    PanelUnderCursor        - current hovered element under cursor
    
    Canvas                  - unity gameObject that corresponded for the User Interface. Every User Interface GameObjects should be child of that canvas
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

        // TODO: move out CursorPosition to InputProcess, i guess
        public Vec2f CursorPosition;
        
        public ElementUI ElementUnderCursor;
        public PanelUI PanelUnderCursor;
        
        private UnityEngine.Canvas canvas;

        public void InitStage1()
        {
            UnityEngine.Cursor.visible = true;
            canvas = UnityEngine.GameObject.Find("Canvas").GetComponent<UnityEngine.Canvas>();
        }

        public void InitStage2()
        {
            ProgressBar = GameState.Renderer.CreateUnitySprite(
                "Assets\\StreamingAssets\\UserInterface\\Bars\\CircleBar\\hud_status_fill.png", 19, 19, Enums.AtlasType.Gui);
            WhiteSquareBorder = GameState.Renderer.CreateUnitySprite(
                "Assets\\StreamingAssets\\Items\\AdminIcon\\Tools\\white_square.png", 225, 225, Enums.AtlasType.Gui);
            
            GameState.PrefabManager.InitializeResources();

            SetPanelActive(PanelEnums.PlayerStatus);
        }
        
        /*

            Initialize and Activate Panel
            
            If panel already initialized
            Then Activate or Deactivate panel with corresponding events
            
            If panel not initialized
            Instantiate panel from PrefabManager
            Do not instantiate panel if we trying to Deactivate it
            After Instantiation OnActivate Event will be called automatically
            
            If panel not initialized and not in PrefabManager
            Then Error

         */
        public void SetPanelActive(PanelEnums panelID, bool active = true)
        {
            if (PanelList.TryGetValue(panelID, out var panel))
            {
                if (!active)
                {
                    ElementUnderCursor = null;
                    PanelUnderCursor = null;
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
                UnityEngine.Debug.LogWarning("No such panel in prefab list");
            }
        }

        public void Update()
        {
            canvas.GetComponent<UnityEngine.UI.CanvasScaler>().referenceResolution =
                new UnityEngine.Vector2(UnityEngine.Camera.main.pixelWidth, UnityEngine.Camera.main.pixelHeight);
            
            CursorPosition = new Vec2f(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y);

            HandleMouseEvents();
        }

        // Activate all elements in all panels
        // Will no be drawn if Panel not active
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

        /*
            Initializing ElementUnderCursor and PanelUnderCursor
            Call OnMouseStay     event when cursor position still hovering last element
            Call OnMouseExited   event when cursor position not hovering last element anymore
            Call OnMouseEntered  event when cursor position entered element on some panel
            
            Check if we clicked on ElementUnderCursor
            Send click event to Panel with Element ID
            Call OnMouseClicked event on ElementUnderCursor
        */
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
