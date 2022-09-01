using UnityEngine;
using Entitas;
using KGUI.Elements;

namespace KGUI.PlayerStatus
{
    public class WaterBarUI : GUIManager
    {
        // Init
        private static bool Init;

        // Water Bar Icon Sprite
        Sprites.Sprite icon;
        Sprites.Sprite fill;

        // Bar
        public CircleProgressBar waterBar;

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
            var iconSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\UserInterface\\Icons\\Water\\hud_status_water.png", IconWidth, IconHeight);

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

            // Add Components and setup game object
            Sprite iconBar = Sprite.Create(icon.Texture, new Rect(0.0f, 0.0f, IconWidth, IconHeight), new Vector2(0.5f, 0.5f));

            // Water Bar Initializon
            iconCanvas = new Image("Water Icon", iconBar);
            iconCanvas.SetImageTopLeft();

            // Set Icon Position Based On Aspect Ratio
            iconCanvas.SetPosition(new Vector3(-425.5f, 80.8f, 4.873917f));

            // Set Icon Scale
            iconCanvas.SetScale(new Vector3(0.6f, -0.6f, 0.5203559f));

            // Add Components and setup game object
            Sprite bar = Sprite.Create(fill.Texture, new Rect(0.0f, 0.0f, FillWidth, FillHeight), new Vector2(0.5f, 0.5f));

            // Set Fill Amount Value
            if(agentEntity != null)
                fillValue = agentEntity.agentStats.Water;
            else
                fillValue = 0.0f;

            // Water Bar Initializon
            waterBar = new CircleProgressBar("Water Bar", iconCanvas.GetTransform(), bar, fillValue / 100);

            // Water Bar Set Position
            waterBar.SetPosition(new Vector3(-0.4f, -0.1f, 4.873917f));

            // Water Bar Set Scale
            waterBar.SetScale(new Vector3(0.8566527f, 0.8566527f, 0.3714702f));

            // Initializon Done
            Init = true;
        }

        public override void Update(AgentEntity agentEntity)
        {
            if (Init)
            {
                Rect rect = ((RectTransform) iconCanvas.GetTransform()).rect;
                ObjectPosition = new KMath.Vec2f(iconCanvas.GetTransform().position.x + (rect.xMin * iconCanvas.GetTransform().localScale.x), iconCanvas.GetTransform().position.y + (rect.yMin * -iconCanvas.GetTransform().localScale.y));
                ObjectSize = new KMath.Vec2f(rect.width * iconCanvas.GetTransform().localScale.x, rect.height * -iconCanvas.GetTransform().localScale.y);

                // Update Fill Amount
                if(agentEntity != null)
                    fillValue = agentEntity.agentStats.Water;
                else
                    fillValue = 0.0f;

                // Water Bar Update Fill Amount
                waterBar.Update(fillValue / 100);

                // Info Text Update
                infoText.Update();
            }
        }

        public override void Draw()
        {
            iconCanvas.Draw();
            waterBar.Draw();
        }

        // Water Bar OnMouseClick Event
        public override void OnMouseClick(AgentEntity agentEntity)
        {
            Debug.LogWarning("Water Bar Clicked");
        }

        // Water Bar OnMouseEnter Event
        public override void OnMouseEnter()
        {
            Debug.LogWarning("Water Bar Mouse Enter");

            // If Water level less than 50
            if (fillValue < 50)
            {
                // Create Hover Text
                infoText.Create("Water Indicator", "Water Bar\nStatus: Low", iconCanvas.GetTransform(), 2.0f);

                // Set Size Delta
                infoText.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoText.SetPosition(new Vector3(260.0f, 0, 0));
            }
            else
            {
                // Create Hover Text
                infoText.Create("Water DeIndicator", "Water Bar\nStatus: Normal", iconCanvas.GetTransform(), 2.0f);

                // Set Size Delta
                infoText.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoText.SetPosition(new Vector3(260.0f, 0, 0));
            }
        }

        // Water Bar OnMouseStay Event
        public override void OnMouseStay()
        {
            Debug.LogWarning("Water Bar Mouse Stay");
        }

        // Water Bar OnMouseExit Event
        public override void OnMouseExit()
        {
            Debug.LogWarning("Water Bar Mouse Exit");

            infoText.startLifeTime = true;
        }
    }
}
