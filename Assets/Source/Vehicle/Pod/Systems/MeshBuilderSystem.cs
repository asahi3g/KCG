using System.Collections.Generic;
using UnityEngine;
using Entitas;
using KMath;
using Sprites;

namespace Pod
{
    public class MeshBuilderSystem
    {
        public Utility.FrameMesh Mesh;

        public void Initialize(Material material, Transform transform, int drawOrder = 0)
        {
            Mesh = new Utility.FrameMesh("podsGameObject", material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Vehicle), drawOrder);
        }

        public void UpdateMesh(PodContext contexts)
        {
            var PodsWithSprite = contexts.GetGroup(PodMatcher.AllOf(PodMatcher.PodID));

            Mesh.Clear();
            int index = 0;
            foreach (var entity in PodsWithSprite)
            {
                Vector4 textureCoords = GameState.SpriteAtlasManager.GetSprite(entity.podSprite2D.SpriteId, Enums.AtlasType.Vehicle).TextureCoords;

                var x = entity.podPhysicsState2D.Position.X;
                var y = entity.podPhysicsState2D.Position.Y;
                var width = entity.podSprite2D.Size.X;
                var height = entity.podSprite2D.Size.Y;

                if (!Utility.ObjectMesh.isOnScreen(x, y))
                    continue;

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), x, y, width, height, entity.podPhysicsState2D.Rotation);
            }
        }
    }
}
