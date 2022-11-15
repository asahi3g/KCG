//import UnityEngine

using UnityEngine;

namespace Vehicle.Pod
{
    public class MeshBuilderSystem
    {
        public Utility.FrameMesh Mesh;

        public void Initialize(UnityEngine.Material material, UnityEngine.Transform transform, int drawOrder = 0)
        {
            Mesh = new Utility.FrameMesh("podsGameObject", material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Vehicle), drawOrder);
        }

        public void UpdateMesh()
        {
            var PodsWithSprite = GameState.Planet.EntitasContext.pod.GetGroup(PodMatcher.AllOf(PodMatcher.VehiclePodID));

            Mesh.Clear();
            int index = 0;
            foreach (var entity in PodsWithSprite)
            {
                UnityEngine.Vector4 textureCoords = GameState.SpriteAtlasManager.GetSprite(entity.vehiclePodSprite2D.SpriteId, Enums.AtlasType.Vehicle).TextureCoords;

                var x = entity.vehiclePodPhysicsState2D.Position.X;
                var y = entity.vehiclePodPhysicsState2D.Position.Y;
                var width = entity.vehiclePodSprite2D.Size.X;
                var height = entity.vehiclePodSprite2D.Size.Y;

                if (!Utility.ObjectMesh.isOnScreen(x, y))
                    continue;

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), x, y, width, height, entity.vehiclePodPhysicsState2D.Rotation);

                if (entity.vehiclePodStatus.Exploded)
                {
                    if(!entity.vehiclePodStatus.RightPanelCollided)
                    {
                        entity.vehiclePodStatus.RightPanelPos.X += 1.0f;
                        entity.vehiclePodStatus.RightPanelPos.Y += 0.5f;
                    }

                    if(!entity.vehiclePodStatus.LeftPanelCollided)
                    {
                        entity.vehiclePodStatus.LeftPanelPos.X -= 1.0f;
                        entity.vehiclePodStatus.LeftPanelPos.Y -= 0.5f;
                    }

                    if (!entity.vehiclePodStatus.BottomPanelCollided)
                    {
                        entity.vehiclePodStatus.BottomPanelPos.X -= 0.5f;
                        entity.vehiclePodStatus.BottomPanelPos.Y -= 1.0f;
                    }

                    if(!entity.vehiclePodStatus.TopPanelCollided)
                    {
                        entity.vehiclePodStatus.TopPanelPos.X += 0.5f;
                        entity.vehiclePodStatus.TopPanelPos.Y += 1.0f;
                    }
                }
                else
                {
                    entity.vehiclePodStatus.RightPanelPos.X = entity.vehiclePodPhysicsState2D.Position.X + entity.vehiclePodStatus.RightPanelOffset.X;
                    entity.vehiclePodStatus.RightPanelPos.Y = entity.vehiclePodPhysicsState2D.Position.Y + entity.vehiclePodStatus.RightPanelOffset.Y;

                    entity.vehiclePodStatus.LeftPanelPos.X = entity.vehiclePodPhysicsState2D.Position.X + entity.vehiclePodStatus.LeftPanelOffset.X;
                    entity.vehiclePodStatus.LeftPanelPos.Y = entity.vehiclePodPhysicsState2D.Position.Y + entity.vehiclePodStatus.LeftPanelOffset.Y;

                    entity.vehiclePodStatus.TopPanelPos.X = entity.vehiclePodPhysicsState2D.Position.X + entity.vehiclePodStatus.TopPanelOffset.X;
                    entity.vehiclePodStatus.TopPanelPos.Y = entity.vehiclePodPhysicsState2D.Position.Y + entity.vehiclePodStatus.TopPanelOffset.Y;

                    entity.vehiclePodStatus.BottomPanelPos.X = entity.vehiclePodPhysicsState2D.Position.X + entity.vehiclePodStatus.BottomPanelOffset.X;
                    entity.vehiclePodStatus.BottomPanelPos.Y = entity.vehiclePodPhysicsState2D.Position.Y + entity.vehiclePodStatus.BottomPanelOffset.Y;
                }

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), entity.vehiclePodStatus.RightPanelPos.X, entity.vehiclePodStatus.RightPanelPos.Y, entity.vehiclePodStatus.RightPanelWidth, entity.vehiclePodStatus.RightPanelHeight,
                    entity.vehiclePodPhysicsState2D.Rotation);

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), entity.vehiclePodStatus.LeftPanelPos.X, entity.vehiclePodStatus.LeftPanelPos.Y, entity.vehiclePodStatus.LeftPanelWidth, entity.vehiclePodStatus.LeftPanelHeight,
                    entity.vehiclePodPhysicsState2D.Rotation);

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), entity.vehiclePodStatus.TopPanelPos.X, entity.vehiclePodStatus.TopPanelPos.Y, entity.vehiclePodStatus.TopPanelWidth, entity.vehiclePodStatus.TopPanelHeight,
                    entity.vehiclePodPhysicsState2D.Rotation);

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), entity.vehiclePodStatus.BottomPanelPos.X, entity.vehiclePodStatus.BottomPanelPos.Y, entity.vehiclePodStatus.BottomPanelWidth, entity.vehiclePodStatus.BottomPanelHeight,
                    entity.vehiclePodPhysicsState2D.Rotation);

            }
        }
    }
}
