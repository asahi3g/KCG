namespace Gui
{


    public class GuiResourceManager
    {

        public UnityEngine.Font RodinFont;
        public UnityEngine.Font BigSpace;
        public int ActiveButtonSprite;
        public int HoverButtonSprite;
        public int PressedButtonSprite;


        public void InitStage1()
        {

        }

        public void InitStage2()
        {
            LoadResources();
        }


        private void LoadResources()
        {
            int activeButtonSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\gui\\Button_Active.png", 245, 140);
            ActiveButtonSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(activeButtonSheet, 0, 0, Enums.AtlasType.Gui);

            int hoverButtonSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\gui\\Button_Hover.png", 245, 140);
            HoverButtonSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(hoverButtonSheet, 0, 0, Enums.AtlasType.Gui);

            int pressedButtonSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\gui\\Button_Pressed.png", 245, 140);
            PressedButtonSprite = GameState.SpriteAtlasManager.CopySpriteToAtlas(pressedButtonSheet, 0, 0, Enums.AtlasType.Gui);

            RodinFont = (UnityEngine.Font)UnityEngine.Resources.Load("Font\\FOTRodin Pro DB");
            BigSpace = (UnityEngine.Font)UnityEngine.Resources.Load("Font\\BigSpace-rPKx");
        }
    }


}