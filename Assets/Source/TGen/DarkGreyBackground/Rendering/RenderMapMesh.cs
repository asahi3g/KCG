//imports UnityEngine

using Enums.PlanetTileMap;
using System;
using Utility;

namespace TGen.DarkGreyBackground
{
    public class RenderMapMesh
    {
        FrameMesh Mesh;
        public static readonly int LayerCount = Enum.GetNames(typeof(MapLayerType)).Length;
        public static bool TileCollisionDebugging = false;

        public void Initialize(UnityEngine.Material material, UnityEngine.Transform transform, int drawOrder)
        {
            Mesh = new FrameMesh(("PlaceableBackground"), material, transform,
                    GameState.TileSpriteAtlasManager.GetSpriteAtlas(0), drawOrder);           
        }

        public void UpdateMesh(TGen.DarkGreyBackground.BackgroundGrid grid)
        {
        }

        public int GetSpriteIDFromIsotype(BlockTypeAndRotation isotype)
        {
            return (int)isotype - 1;
        }

        public void Draw()
        {
            GameState.Renderer.DrawFrame(ref Mesh, GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.TGen));
        }
    }
}