using UnityEngine;
using Entitas;
using KGUI.Elements;

namespace KGUI.PlayerStatus
{
    public class OxygenBarUI : GUIManager
    {
        // Init
        private static bool Init;

        // Oxygen Bar Icon Sprite
        Sprites.Sprite icon;
        Sprites.Sprite fill;

        // Bar
        public CircleProgressBar oxygenBar;

        // Icon
        private Image iconCanvas;
        
        // Hover Text
        private Text infoText = new Text();
        
        // Fill Amount Value
        private float fillValue;
        
        public override void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            // Set Width and Height
            int IconWidth = 19;
            int IconHeight = 19;
            Vector2Int iconPngSize = new Vector2Int(IconWidth, IconHeight);

            // Load image from file
            var iconSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\UserInterface\\Icons\\Oxygen\\hud_status_oxygen.png", IconWidth, IconHeight);

            // Set Sprite ID from Sprite Atlas
            int iconID = GameState.SpriteAtlasManager.CopySpriteToAtlas(iconSheet, 0, 0, Enums.AtlasType.Particle);

            // Set Sprite Data
            byte[] iconSpriteData = new byte[iconPngSize.x * iconPngSize.y * 4];

            // Get Sprite Bytes
            GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, Enums.AtlasType.Particle);

            // Set Texture
            Texture2D iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, iconPngSize.x, iconPngSize.y);

            // Create the sprite
            icon = new Sprites.Sprite
            {
                Texture = iconTex,
                TextureCoords = new Vector4(0, 0, 1, 1)
            };

            // Set Width and Height
            int FillWidth = 19;
            int FillHeight = 19;
            Vector2Int FillPngSize = new Vector2Int(FillWidth, FillHeight);

            // Load image from file
            var FillSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\UserInterface\\Bars\\CircleBar\\hud_status_fill.png", FillWidth, FillHeight);

            // Set Sprite ID from Sprite Atlas
            int FillID = GameState.SpriteAtlasManager.CopySpriteToAtlas(FillSheet, 0, 0, Enums.AtlasType.Particle);

            // Set Sprite Data
            byte[] FillSpriteData = new byte[FillPngSize.x * FillPngSize.y * 4];

            // Get Sprite Bytes
            GameState.SpriteAtlasManager.GetSpriteBytes(FillID, FillSpriteData, Enums.AtlasType.Particle);

            // Set Texture
            Texture2D FillTex = Utility.Texture.CreateTextureFromRGBA(FillSpriteData, FillPngSize.x, FillPngSize.y);

            // Create the sprite
            fill = new Sprites.Sprite
            {
                Texture = FillTex,
                TextureCoords = new Vector4(0, 0, 1, 1)
            };

            // Add Components and setup agent object
            Sprite iconBar = Sprite.Create(icon.Texture, new Rect(0.0f, 0.0f, IconWidth, IconHeight), new Vector2(0.5f, 0.5f));

            // Oxygen Bar Initializon
            iconCanvas = new Image("Oxygen Icon", iconBar);
            iconCanvas.SetImageTopLeft();

            // Set Icon Position Based On Aspect Ratio
            iconCanvas.SetPosition(new Vector3(-425.5f, 20f, 4.873917f));

            // Set Icon Scale
            iconCanvas.SetScale(new Vector3(0.6f, -0.6f, 0.5203559f));

            // Add Components and setup agent object
            Sprite bar = Sprite.Create(fill.Texture, new Rect(0.0f, 0.0f, FillWidth, FillHeight), new Vector2(0.5f, 0.5f));

            // Set Fill Amount Value
            if(agentEntity != null)
                fillValue = agentEntity.agentStats.Oxygen;
            else
                fillValue = 0.0f;

            // Oxygen Bar Initializon
            oxygenBar = new CircleProgressBar("Oxygen Bar", iconCanvas.GetTransform(), bar, fillValue / 100);

            // Oxygen Bar Set Position
            oxygenBar.SetPosition(new Vector3(-0.4f, -0.1f, 4.873917f));

            // Oxygen Bar Set Scale
            oxygenBar.SetScale(new Vector3(0.8566527f, 0.8566527f, 0.3714702f));

            // Initializon Done
            Init = true;
        }

        public override void Update(AgentEntity agentEntity)
        {
            if (Init)
            {
                // Update Object Position
                ObjectPosition = new KMath.Vec2f(iconCanvas.GetTransform().position.x, iconCanvas.GetTransform().position.y);

                // Update Fill Amount Value
                if(agentEntity != null)
                    fillValue = agentEntity.agentStats.Oxygen;
                else
                    fillValue = 0.0f;

                // Oxygen Bar Update Fill Amount
                oxygenBar.Update(fillValue / 100);

                // Info Text Update
                infoText.Update();
            }
        }

        public override void Draw()
        {
            iconCanvas.Draw();
            oxygenBar.Draw();
        }

        // Oxygen Bar OnMouseClick Event
        public override void OnMouseClick(AgentEntity agentEntity)
        {
            Debug.LogWarning("Oxygen Bar Clicked");
        }

        // Oxygen Bar OnMouseEnter Event
        public override void OnMouseEnter()
        {
            Debug.LogWarning("Oxygen Bar Mouse Enter");

            // If Oxygen level less than 50
            if (fillValue < 50)
            {
                // Create Hover Text
                infoText.Create("Oxygen Indicator", "Oxygen Bar\nStatus: Low", iconCanvas.GetTransform(), 2.0f);

                // Set Size Delta
                infoText.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoText.SetPosition(new Vector3(260.0f, 0, 0));
            }
            else
            {
                // Create Hover Text
                infoText.Create("Oxygen DeIndicator", "Oxygen Bar\nStatus: Normal", iconCanvas.GetTransform(), 2.0f);

                // Set Size Delta
                infoText.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoText.SetPosition(new Vector3(260.0f, 0, 0));
            }

        }

        // Oxygen Bar OnMouseStay Event
        public override void OnMouseStay()
        {
            Debug.LogWarning("Oxygen Bar Mouse Stay");
        }

        // Oxygen Bar OnMouseExit Event
        public override void OnMouseExit()
        {
            Debug.LogWarning("Oxygen Bar Mouse Exit");

            // Start Life Time Countdown
            infoText.startLifeTime = true;
        }
    }
}
