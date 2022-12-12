//imports UnityEngine


namespace Vehicle
{
    public class MeshBuilderSystem
    {
        public Utility.FrameMesh Mesh;

        public void Initialize(UnityEngine.Material material, UnityEngine.Transform transform, int drawOrder = 0)
        {
            Mesh = new Utility.FrameMesh("vehiclesGameObject", material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Vehicle), drawOrder);
        }

        public void UpdateMesh()
        {
            var VehiclesWithSprite = GameState.Planet.EntitasContext.vehicle.GetGroup(VehicleMatcher.AllOf(VehicleMatcher.VehicleSprite2D));
            
            Mesh.Clear();
            int index = 0;
            foreach (var entity in VehiclesWithSprite)
            {
                UnityEngine.Vector4 textureCoords = GameState.SpriteAtlasManager.GetSprite(entity.vehicleSprite2D.SpriteId, Enums.AtlasType.Vehicle).TextureCoords;

                var x = entity.vehiclePhysicsState2D.Position.X;
                var y = entity.vehiclePhysicsState2D.Position.Y;
                var width = entity.vehicleSprite2D.Size.X;
                var height = entity.vehicleSprite2D.Size.Y;

                if (!Utility.ObjectMesh.isOnScreen(x, y))
                    continue;

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);
                // Update Vertices
                Mesh.UpdateVertex((index++ * 4), x, y, width, height, false, entity.vehiclePhysicsState2D.Rotation);


                if (entity.hasVehicleThruster && entity.hasVehicleThrusterSprite2D)
                {
                    UnityEngine.Vector4 Thruster1textureCoords = GameState.SpriteAtlasManager.GetSprite(entity.vehicleThrusterSprite2D.SpriteId, Enums.AtlasType.Vehicle).TextureCoords;

                    UnityEngine.Vector4 Thruster2textureCoords = GameState.SpriteAtlasManager.GetSprite(entity.vehicleThrusterSprite2D.SpriteId, Enums.AtlasType.Vehicle).TextureCoords;

                    var thruster1X = entity.vehicleThrusterSprite2D.Position1.X;
                    var thruster1Y = entity.vehicleThrusterSprite2D.Position1.Y;

                    var thruster2X = entity.vehicleThrusterSprite2D.Position2.X;
                    var thruster2Y = entity.vehicleThrusterSprite2D.Position2.Y;

                    var thrusterWidth = entity.vehicleThrusterSprite2D.Size.X;
                    var thrusterHeight = entity.vehicleThrusterSprite2D.Size.Y;

                    // Update UVs
                    Mesh.UpdateUV(Thruster1textureCoords, (index) * 4);

                    // Update Vertices
                    Mesh.UpdateVertex((index++ * 4), x + thruster1X, y + thruster1Y, thrusterWidth, thrusterHeight,
                        false, entity.vehiclePhysicsState2D.Rotation);

                    // Update UVs
                    Mesh.UpdateUV(Thruster2textureCoords, (index) * 4);

                    // Update Vertices
                    Mesh.UpdateVertex((index++ * 4), x + thruster2X, y + thruster2Y, thrusterWidth, thrusterHeight,
                        false,entity.vehiclePhysicsState2D.Rotation);

                }
            }
        }
    }
}
