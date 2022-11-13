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

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), x + entity.vehiclePodStatus.RightPanel.X, y + entity.vehiclePodStatus.RightPanel.Y, entity.vehiclePodStatus.RightPanelWidth, entity.vehiclePodStatus.RightPanelHeight,
                    entity.vehiclePodPhysicsState2D.Rotation);

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), x + entity.vehiclePodStatus.LeftPanel.X, y + entity.vehiclePodStatus.LeftPanel.Y, entity.vehiclePodStatus.LeftPanelWidth, entity.vehiclePodStatus.LeftPanelHeight,
                    entity.vehiclePodPhysicsState2D.Rotation);

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), x + entity.vehiclePodStatus.TopPanel.X, y + entity.vehiclePodStatus.TopPanel.Y, entity.vehiclePodStatus.TopPanelWidth, entity.vehiclePodStatus.TopPanelHeight,
                    entity.vehiclePodPhysicsState2D.Rotation);

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);

                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), x + entity.vehiclePodStatus.BottomPanel.X, y + entity.vehiclePodStatus.BottomPanel.Y, entity.vehiclePodStatus.BottomPanelWidth, entity.vehiclePodStatus.BottomPanelHeight,
                    entity.vehiclePodPhysicsState2D.Rotation);

            }
        }
    }
}
