//imports UnityEngine

using System.Collections.Generic;
using KMath;

namespace KGUI.Elements
{
    public class ElementUpdateSystem
    {
        List<UIElementEntity> ToRemoveEntities = new List<UIElementEntity>();

        public void Update(ref Planet.PlanetState planetState, float deltaTime)
        {
            var entities = planetState.EntitasContext.uIElement.GetGroup(UIElementMatcher.AllOf(UIElementMatcher.KGUIElementsPosition2D));

            foreach (var entity in entities)
            {
                if(entity.hasKGUIElementsText)
                {
                    var text = entity.kGUIElementsText;
                    var position = entity.kGUIElementsPosition2D;

                    text.GameObject.Update();
                    text.GameObject.UpdateText(text.Text);
                    text.GameObject.SetPosition(new UnityEngine.Vector3(position.Value.X, position.Value.Y, 0.0f));
                    text.GameObject.SetSizeDelta(new UnityEngine.Vector2(text.SizeDelta.X, text.SizeDelta.Y));

                }

                if (entity.hasKGUIElementsImage)
                {
                    var image = entity.kGUIElementsImage;
                    var position = entity.kGUIElementsPosition2D;

                    image.ImageWrapper.Update();
                    //image.Image.SetPosition(new Vector3(position.Value.X, position.Value.Y, 0.0f));
                    image.ImageWrapper.SetScale(new UnityEngine.Vector3(image.Scale.X, image.Scale.Y, image.Scale.Z));
                }
            }

            foreach(var entity in ToRemoveEntities)
            {
                planetState.RemoveUIElement(entity.kGUIElementsID.Index);
            }
            ToRemoveEntities.Clear();
        }

        public int DefaultCursor_;
        public int AimCursor_;
        public int BuildCursor_;

        public void InitializeResources()
        {
            DefaultCursor_ = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Cursors\\cursors.png", 16, 16);
            AimCursor_ = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Cursors\\cursors.png", 16, 16);
            BuildCursor_ = GameState.SpriteLoader.GetSpriteSheetID("Assets\\StreamingAssets\\Cursors\\cursors.png", 16, 16);

            DefaultCursor_ = GameState.SpriteAtlasManager.CopySpriteToAtlas(DefaultCursor_, 0, 0, Enums.AtlasType.Particle);
            AimCursor_ = GameState.SpriteAtlasManager.CopySpriteToAtlas(AimCursor_, 2, 0, Enums.AtlasType.Particle);
            BuildCursor_ = GameState.SpriteAtlasManager.CopySpriteToAtlas(BuildCursor_, 1, 1, Enums.AtlasType.Particle);
        }
    }
}

