using UnityEngine;
using KGUI.Elements;
using KMath;

namespace KGUI
{
    public class FoodElementUI : UIElement
    {
        public CircleProgressBar ProgressBar;
        
        private float fillValue;
        private Text infoText = new();

        public override void Init()
        {
            base.Init();
            
            ID = UIElementID.FoodElement;
            
            Vec2i iconPngSize = new Vec2i(19, 19);
            var iconSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\UserInterface\\Icons\\Food\\hud_status_food.png", iconPngSize.X, iconPngSize.Y);
            int iconID = GameState.SpriteAtlasManager.CopySpriteToAtlas(iconSheet, 0, 0, Enums.AtlasType.Particle);
            byte[] iconSpriteData = new byte[iconPngSize.X * iconPngSize.Y * 4];
            GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, Enums.AtlasType.Particle);
            Texture2D iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, iconPngSize.X, iconPngSize.Y);
            Icon.sprite = Sprite.Create(iconTex, new Rect(0.0f, 0.0f, iconPngSize.X, iconPngSize.Y), new Vector2(0.5f, 0.5f));

            var fillPngSize = new Vec2i(19, 19);
            var fillSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\UserInterface\\Bars\\CircleBar\\hud_status_fill.png", fillPngSize.X, fillPngSize.Y);
            int fillID = GameState.SpriteAtlasManager.CopySpriteToAtlas(fillSheet, 0, 0, Enums.AtlasType.Particle);
            var fillSpriteData = new byte[fillPngSize.X * fillPngSize.Y * 4];
            GameState.SpriteAtlasManager.GetSpriteBytes(fillID, fillSpriteData, Enums.AtlasType.Particle);
            var fillTex = Utility.Texture.CreateTextureFromRGBA(fillSpriteData, fillPngSize.X, fillPngSize.Y);
            var bar = Sprite.Create(fillTex, new Rect(0.0f, 0.0f, fillPngSize.X, fillPngSize.Y), new Vector2(0.5f, 0.5f));
            fillValue = GameState.GUIManager.AgentEntity != null ? GameState.GUIManager.AgentEntity.agentStats.Food : 0.0f;
            ProgressBar = new CircleProgressBar("Food Bar", transform, bar, fillValue / 100);
            
            HitBox = new AABox2D(new Vec2f(Position.x, Position.y), new Vec2f(Size.x, Size.y));
        }

        public void Update()
        {
            fillValue = GameState.GUIManager.AgentEntity != null ? GameState.GUIManager.AgentEntity.agentStats.Food : 0.0f;
            ProgressBar.Update(fillValue / 100);
            infoText.Update();
        }
        
        public override void OnMouseClick()
        {
            Debug.LogWarning("Food Bar Clicked");
        }
        public override void OnMouseEntered()
        {
            base.OnMouseEntered();
            
            Debug.LogWarning("Food Bar Mouse Enter");

            // If Water level less than 50
            if (fillValue < 50)
            {
                // Create Hover Text
                infoText.Create("Food Indicator", "Hunger Bar\nStatus: Low", Icon.transform, 2.0f);

                // Set Size Delta
                infoText.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoText.SetPosition(new Vector3(Position.x + Size.x + 20f, 0, 0));
                
                infoText.Draw();
            }
            else 
            {
                // Create Hover Text
                infoText.Create("Food DeIndicator", "Hunger Bar\nStatus: Normal", Icon.transform, 2.0f);

                // Set Size Delta
                infoText.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoText.SetPosition(new Vector3(Position.x + Size.x + 20f, 0, 0));
                
                infoText.Draw();
            }
        }
        public override void OnMouseStay()
        {
            Debug.LogWarning("Food Bar Mouse Stay");
        }
        public override void OnMouseExited()
        {
            base.OnMouseExited();
            
            Debug.LogWarning("Food Bar Mouse Exit");

            infoText.startLifeTime = true;
        }
    }
}
