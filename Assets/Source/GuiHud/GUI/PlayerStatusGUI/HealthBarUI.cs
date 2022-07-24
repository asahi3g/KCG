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
        Sprite barDiv1Sprite;
        Sprite barDiv2Sprite;

        // Icon
        Image Icon;

        // Border
        Image Border;

        // Div's
        Image BarDiv1;
        Image BarDiv2;
        Image BarDiv3;

        // Health Bar
        ProgressBar Bar;

        public override void Initialize(Contexts contexts, AgentEntity agentEntity)
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

            // Add Components and setup agent object
            Sprite iconBar = Sprite.Create(icon.Texture, new Rect(0.0f, 0.0f, IconWidth, IconHeight), new Vector2(0.5f, 0.5f));
            Sprite borderSprite = Sprite.Create(barBorder.Texture, new Rect(0.0f, 0.0f, BarBorderWidth, BarBorderHeight), new Vector2(0.5f, 0.5f));
            Sprite fillSprite = Sprite.Create(fill.Texture, new Rect(0.0f, 0.0f, FillWidth, FillHeight), new Vector2(0.5f, 0.5f));
            barDiv1Sprite = Sprite.Create(barDiv1.Texture, new Rect(0.0f, 0.0f, BarDiv1Width, BarDiv1Height), new Vector2(0.5f, 0.5f));
            barDiv2Sprite = Sprite.Create(barDiv2.Texture, new Rect(0.0f, 0.0f, BarDiv2Width, BarDiv2Height), new Vector2(0.5f, 0.5f));

            Icon = new Image("Health Bar", iconBar);
            fillValue = agentEntity.agentStats.Health;
            Border = new Image("Border", Icon.GetTransform(), borderSprite);
            Bar = new ProgressBar("Health Bar", Icon.GetTransform(), fillSprite, fillValue / 100, agentEntity);
            BarDiv1 = new Image("BarDiv1", Icon.GetTransform(), barDiv1Sprite);
            BarDiv2 = new Image("BarDiv2", Icon.GetTransform(), barDiv1Sprite);
            BarDiv3 = new Image("BarDiv3", Icon.GetTransform(), barDiv1Sprite);


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

            Icon.SetScale(new Vector3(0.6f, -0.6f, 0.5203559f));
            Border.SetScale(new Vector3(4.069521f, 0.3654834f, 1));
            Bar.SetScale(new Vector3(3.933964f, 0.27499f, 1));
            BarDiv1.SetScale(new Vector3(0.06024375f, 0.2906317f, 1));
            BarDiv2.SetScale(new Vector3(0.06024375f, 0.2906317f, 1));
            BarDiv3.SetScale(new Vector3(0.06024375f, 0.2906317f, 1));

            Init = true;
        }

        public override void Update(AgentEntity agentEntity)
        {
            if(Init)
            {
                ObjectPosition = new KMath.Vec2f(Icon.GetTransform().position.x, Icon.GetTransform().position.y);

                fillValue = agentEntity.agentStats.Health;
                Bar.Update(fillValue / 100);

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

        public override void OnMouseClick(AgentEntity agentEntity)
        {
            Debug.LogWarning("Health Bar Clicked");
        }

        public override void OnMouseEnter()
        {
            Debug.LogWarning("Health Bar Mouse Enter");
        }

        public override void OnMouseStay()
        {
            Debug.LogWarning("Health Bar Mouse Stay");
        }

        public override void OnMouseExit()
        {
            Debug.LogWarning("Health Bar Mouse Exit");
        }
    }
}
