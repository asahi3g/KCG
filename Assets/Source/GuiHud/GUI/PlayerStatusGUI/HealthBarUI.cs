using UnityEngine;
using KGUI.Elements;

namespace KGUI.PlayerStatus
{
    public class HealthBarUI : GUIManager
    {
        // Init Condition
        private static bool Init;

        // Fill Value
        float fillValue;

        // Div Global Sprites
        private Sprite barDiv1Sprite;
        private Sprite barDiv2Sprite;

        // Icon
        private Image Icon;

        // Border
        private Image Border;

        // Div's
        private Image BarDiv1;
        private Image BarDiv2;
        private Image BarDiv3;

        // Hover Text
        private Text infoText = new Text();

        // Health Bar
        private ProgressBar Bar;

        public override void Initialize(Planet.PlanetState planet, AgentEntity agentEntity)
        {
            // Set Width and Height
            int IconWidth = 19;
            int IconHeight = 19;
            Vector2Int iconPngSize = new Vector2Int(IconWidth, IconHeight);

            // Load image from file
            var iconSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\UserInterface\\Icons\\Health\\hud_hp_icon.png", IconWidth, IconHeight);

            // Set Sprite ID from Sprite Atlas
            int iconID = GameState.SpriteAtlasManager.CopySpriteToAtlas(iconSheet, 0, 0, Enums.AtlasType.Particle);

            // Set Sprite Data
            byte[] iconSpriteData = new byte[iconPngSize.x * iconPngSize.y * 4];

            // Get Sprite Bytes
            GameState.SpriteAtlasManager.GetSpriteBytes(iconID, iconSpriteData, Enums.AtlasType.Particle);

            // Set Texture
            Texture2D iconTex = Utility.Texture.CreateTextureFromRGBA(iconSpriteData, iconPngSize.x, iconPngSize.y);

            // Create the sprite
            Sprites.Sprite icon = new Sprites.Sprite
            {
                Texture = iconTex,
                TextureCoords = new Vector4(0, 0, 1, 1)
            };

            // Set Width and Height
            int FillWidth = 5;
            int FillHeight = 5;
            Vector2Int fillPngSize = new Vector2Int(FillWidth, FillHeight);

            // Load image from file
            var fillSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_fill.png", FillWidth, FillHeight);

            // Set Sprite ID from Sprite Atlas
            int fillID = GameState.SpriteAtlasManager.CopySpriteToAtlas(fillSheet, 0, 0, Enums.AtlasType.Particle);

            // Set Sprite Data
            byte[] fillSpriteData = new byte[fillPngSize.x * fillPngSize.y * 4];

            // Get Sprite Bytes
            GameState.SpriteAtlasManager.GetSpriteBytes(fillID, fillSpriteData, Enums.AtlasType.Particle);

            // Set Texture
            Texture2D fillTex = Utility.Texture.CreateTextureFromRGBA(fillSpriteData, fillPngSize.x, fillPngSize.y);

            // Create the sprite
            Sprites.Sprite fill = new Sprites.Sprite
            {
                Texture = fillTex,
                TextureCoords = new Vector4(0, 0, 1, 1)
            };

            // Set Width and Height
            int BarBorderWidth = 6;
            int BarBorderHeight = 8;
            Vector2Int BarBorderPngSize = new Vector2Int(BarBorderWidth, BarBorderHeight);

            // Load image from file
            var BarBorderSheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_border.png", BarBorderWidth, BarBorderHeight);

            // Set Sprite ID from Sprite Atlas
            int BarBorderID = GameState.SpriteAtlasManager.CopySpriteToAtlas(BarBorderSheet, 0, 0, Enums.AtlasType.Particle);

            // Set Sprite Data
            byte[] BarBorderSpriteData = new byte[BarBorderPngSize.x * BarBorderPngSize.y * 4];

            // Get Sprite Bytes
            GameState.SpriteAtlasManager.GetSpriteBytes(BarBorderID, BarBorderSpriteData, Enums.AtlasType.Particle);

            // Set Texture
            Texture2D BarBorderTex = Utility.Texture.CreateTextureFromRGBA(BarBorderSpriteData, BarBorderPngSize.x, BarBorderPngSize.y);

            // Create the sprite
            Sprites.Sprite barBorder = new Sprites.Sprite
            {
                Texture = BarBorderTex,
                TextureCoords = new Vector4(0, 0, 1, 1)
            };

            // Set Width and Height
            int BarDiv1Width = 1;
            int BarDiv1Height = 6;
            Vector2Int BarDiv1PngSize = new Vector2Int(BarDiv1Width, BarDiv1Height);

            // Load image from file
            var BarDiv1Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_div1.png", BarDiv1Width, BarDiv1Height);

            // Set Sprite ID from Sprite Atlas
            int BarDiv1ID = GameState.SpriteAtlasManager.CopySpriteToAtlas(BarDiv1Sheet, 0, 0, Enums.AtlasType.Particle);

            // Set Sprite Data
            byte[] BarDiv1SpriteData = new byte[BarDiv1PngSize.x * BarDiv1PngSize.y * 4];

            // Get Sprite Bytes
            GameState.SpriteAtlasManager.GetSpriteBytes(BarDiv1ID, BarDiv1SpriteData, Enums.AtlasType.Particle);

            // Set Texture
            Texture2D BarDiv1Tex = Utility.Texture.CreateTextureFromRGBA(BarDiv1SpriteData, BarDiv1PngSize.x, BarDiv1PngSize.y);

            // Create the sprite
            Sprites.Sprite barDiv1 = new Sprites.Sprite
            {
                Texture = BarDiv1Tex,
                TextureCoords = new Vector4(0, 0, 1, 1)
            };

            // Set Width and Height
            int BarDiv2Width = 1;
            int BarDiv2Height = 6;
            Vector2Int BarDiv2PngSize = new Vector2Int(BarDiv2Width, BarDiv2Height);

            // Load image from file
            var BarDiv2Sheet = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\UserInterface\\Bars\\HealthBar\\hud_hp_bar_div2.png", BarDiv2Width, BarDiv2Height);

            // Set Sprite ID from Sprite Atlas
            int BarDiv2ID = GameState.SpriteAtlasManager.CopySpriteToAtlas(BarDiv2Sheet, 0, 0, Enums.AtlasType.Particle);

            // Set Sprite Data
            byte[] BarDiv2SpriteData = new byte[BarDiv2PngSize.x * BarDiv2PngSize.y * 4];

            // Get Sprite Bytes
            GameState.SpriteAtlasManager.GetSpriteBytes(BarDiv2ID, BarDiv2SpriteData, Enums.AtlasType.Particle);

            // Set Texture
            Texture2D BarDiv2Tex = Utility.Texture.CreateTextureFromRGBA(BarDiv2SpriteData, BarDiv2PngSize.x, BarDiv2PngSize.y);

            // Create the sprite
            Sprites.Sprite barDiv2 = new Sprites.Sprite
            {
                Texture = BarDiv2Tex,
                TextureCoords = new Vector4(0, 0, 1, 1)
            };

            // Create Icon Sprite
            Sprite iconBar = Sprite.Create(icon.Texture, new Rect(0.0f, 0.0f, IconWidth, IconHeight), new Vector2(0.5f, 0.5f));

            // Create Border Sprite
            Sprite borderSprite = Sprite.Create(barBorder.Texture, new Rect(0.0f, 0.0f, BarBorderWidth, BarBorderHeight), new Vector2(0.5f, 0.5f));

            // Create Fill Sprite
            Sprite fillSprite = Sprite.Create(fill.Texture, new Rect(0.0f, 0.0f, FillWidth, FillHeight), new Vector2(0.5f, 0.5f));

            // Create Bar Divide 1 Sprite
            barDiv1Sprite = Sprite.Create(barDiv1.Texture, new Rect(0.0f, 0.0f, BarDiv1Width, BarDiv1Height), new Vector2(0.5f, 0.5f));

            // Create Bar Divide 2 Sprite
            barDiv2Sprite = Sprite.Create(barDiv2.Texture, new Rect(0.0f, 0.0f, BarDiv2Width, BarDiv2Height), new Vector2(0.5f, 0.5f));

            // Create Icon
            Icon = new Image("Health Bar", iconBar);

            // Create Fill Value
            fillValue = agentEntity.agentStats.Health;

            // Create Border
            Border = new Image("Border", Icon.GetTransform(), borderSprite);

            // Create Bar
            Bar = new ProgressBar("Health Bar", Icon.GetTransform(), fillSprite, fillValue / 100, agentEntity);

            // Create Bar Div 1
            BarDiv1 = new Image("BarDiv1", Icon.GetTransform(), barDiv1Sprite);

            // Create Bar Div 2
            BarDiv2 = new Image("BarDiv2", Icon.GetTransform(), barDiv1Sprite);

            // Create Bar Div 3
            BarDiv3 = new Image("BarDiv3", Icon.GetTransform(), barDiv1Sprite);

            // Set Icon Position Based On Aspect Ratio
            if (Camera.main.aspect >= 1.7f)
            {
                Icon.SetPosition(new Vector3(-377.3f, 183.0f, 4.873917f));
                Border.SetPosition(new Vector3(287f, 7f, 0));
                Bar.SetPosition(new Vector3(287f, 7f, 0f));
                BarDiv1.SetPosition(new Vector3(187.0f, 6f, 0f));
                BarDiv2.SetPosition(new Vector3(287.0f, 6f, 0f));
                BarDiv3.SetPosition(new Vector3(387.0f, 6f, 0f));
            }
            else if (Camera.main.aspect >= 1.5f)
            {
                Icon.SetPosition(new Vector3(-335.6f, 180.6f, 4.873917f));
                Border.SetPosition(new Vector3(287f, 7f, 0));
                Bar.SetPosition(new Vector3(287f, 7f, 0f));
                BarDiv1.SetPosition(new Vector3(187.0f, 6f, 0f));
                BarDiv2.SetPosition(new Vector3(287.0f, 6f, 0f));
                BarDiv3.SetPosition(new Vector3(387.0f, 6f, 0f));
            }
            else
            {
                Icon.SetPosition(new Vector3(-362.8f, 254.3f, 4.873917f));
                Border.SetPosition(new Vector3(287f, 7f, 0));
                Bar.SetPosition(new Vector3(287f, 7f, 0f));
                BarDiv1.SetPosition(new Vector3(187.0f, 6f, 0f));
                BarDiv2.SetPosition(new Vector3(287.0f, 6f, 0f));
                BarDiv3.SetPosition(new Vector3(387.0f, 6f, 0f));
            }

            // Set Icon Scale
            Icon.SetScale(new Vector3(0.6f, -0.6f, 0.5203559f));

            // Set Border Scale
            Border.SetScale(new Vector3(4.069521f, 0.3654834f, 1));

            // Set Bar Scale
            Bar.SetScale(new Vector3(3.933964f, 0.27499f, 1));

            // Set BarDiv1 Scale
            BarDiv1.SetScale(new Vector3(0.06024375f, 0.2906317f, 1));

            // Set BarDiv2 Scale
            BarDiv2.SetScale(new Vector3(0.06024375f, 0.2906317f, 1));

            // Set BarDiv3 Scale
            BarDiv3.SetScale(new Vector3(0.06024375f, 0.2906317f, 1));

            Init = true;
        }

        public override void Update(AgentEntity agentEntity)
        {
            if(Init)
            {
                // Update Object Position
                ObjectPosition = new KMath.Vec2f(Icon.GetTransform().position.x, Icon.GetTransform().position.y);

                // Update Fill Amount
                fillValue = agentEntity.agentStats.Health;

                // Water Bar Update Fill Amount
                Bar.Update(fillValue / 100);

                // Info Text Update
                infoText.Update();

                // Update Div Textures for under 25
                if (fillValue < 25)
                {
                    BarDiv1.SetImage(barDiv2Sprite);
                }
                else
                {
                    BarDiv1.SetImage(barDiv1Sprite);
                }

                // Update Div Textures for under 50
                if (fillValue < 50)
                {
                    BarDiv2.SetImage(barDiv2Sprite);
                }
                else
                {
                    BarDiv2.SetImage(barDiv1Sprite);
                }

                // Update Div Textures for under 75
                if (fillValue < 75)
                {
                    BarDiv3.SetImage(barDiv2Sprite);
                }
                else
                {
                    BarDiv3.SetImage(barDiv1Sprite);
                }

                // Set Icon Position Based On Aspect Ratio
                if (Camera.main.aspect >= 1.7f)
                {
                    Icon.SetPosition(new Vector3(-377.3f, 183.0f, 4.873917f));
                    Border.SetPosition(new Vector3(287f, 7f, 0));
                    Bar.SetPosition(new Vector3(287f, 7f, 0f));
                    BarDiv1.SetPosition(new Vector3(187.0f, 6f, 0f));
                    BarDiv2.SetPosition(new Vector3(287.0f, 6f, 0f));
                    BarDiv3.SetPosition(new Vector3(387.0f, 6f, 0f));
                }
                else if (Camera.main.aspect >= 1.5f)
                {
                    Icon.SetPosition(new Vector3(-335.6f, 180.6f, 4.873917f));
                    Border.SetPosition(new Vector3(287f, 7f, 0));
                    Bar.SetPosition(new Vector3(287f, 7f, 0f));
                    BarDiv1.SetPosition(new Vector3(187.0f, 6f, 0f));
                    BarDiv2.SetPosition(new Vector3(287.0f, 6f, 0f));
                    BarDiv3.SetPosition(new Vector3(387.0f, 6f, 0f));
                }
                else
                {
                    Icon.SetPosition(new Vector3(-362.8f, 254.3f, 4.873917f));
                    Border.SetPosition(new Vector3(287f, 7f, 0));
                    Bar.SetPosition(new Vector3(287f, 7f, 0f));
                    BarDiv1.SetPosition(new Vector3(187.0f, 6f, 0f));
                    BarDiv2.SetPosition(new Vector3(287.0f, 6f, 0f));
                    BarDiv3.SetPosition(new Vector3(387.0f, 6f, 0f));
                }
            }
        }

        // Health Bar OnMouseClick Event
        public override void OnMouseClick(AgentEntity agentEntity)
        {
            Debug.LogWarning("Health Bar Clicked");
        }

        // Health Bar OnMouseEnter Event
        public override void OnMouseEnter()
        {
            Debug.LogWarning("Health Bar Mouse Enter");

            // If Health level less than 50
            if (fillValue < 50)
            {
                // Create Hover Text
                infoText.Create("Health Indicator", "Health Bar\nStatus: Low", Icon.GetTransform(), 2.0f);

                // Set Size Delta
                infoText.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoText.SetPosition(new Vector3(700.0f, 0, 0));
            }
            else
            {
                // Create Hover Text
                infoText.Create("Health DeIndicator", "Health Bar\nStatus: Normal", Icon.GetTransform(), 2.0f);

                // Set Size Delta
                infoText.SetSizeDelta(new Vector2(250, 50));

                // Set Position
                infoText.SetPosition(new Vector3(700.0f, 0, 0));
            }
        }

        // Health Bar OnMouseStay Event
        public override void OnMouseStay()
        {
            Debug.LogWarning("Health Bar Mouse Stay");
        }

        // Health Bar OnMouseExit Event
        public override void OnMouseExit()
        {
            Debug.LogWarning("Health Bar Mouse Exit");

            // Start Life Time
            infoText.startLifeTime = true;
        }
    }
}
