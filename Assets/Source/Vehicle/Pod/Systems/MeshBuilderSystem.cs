//import UnityEngine

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

                UnityEngine.Vector4 RightPanelCoords = GameState.SpriteAtlasManager.GetSprite(entity.vehiclePodSprite2D.SpriteId, Enums.AtlasType.Vehicle).TextureCoords;

                var panelX = 0;
                var panelY = 0;

                var podWidth = entity.vehiclePodPhysicsState2D.Scale.X;
                var podHeight = entity.vehiclePodPhysicsState2D.Scale.Y;

                // Update UVs
                Mesh.UpdateUV(RightPanelCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), x + panelX, y + panelY, podWidth, podHeight,
                    entity.vehiclePodPhysicsState2D.Rotation);
            }
        }
    }
}
