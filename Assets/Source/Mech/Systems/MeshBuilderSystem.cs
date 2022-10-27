//import UnityEngine


namespace Mech
{
    public class MeshBuilderSystem
    {
        public Utility.FrameMesh Mesh;

        public void Initialize(UnityEngine.Material material, UnityEngine.Transform transform, int drawOrder = 0)
        {
            Mesh = new Utility.FrameMesh("MechsGameObject", material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Mech), drawOrder);
        }

        public void UpdateMesh()
        {
            var MechsWithSprite = GameState.Planet.EntitasContext.mech.GetGroup(MechMatcher.AllOf(MechMatcher.MechSprite2D));

            int index = 0;
            Mesh.Clear();
            foreach (var entity in MechsWithSprite)
            {
                int spriteId = entity.mechSprite2D.SpriteId;

                //#397; TEMP ATLAS TYPE AGENT; SHOULD BE MECH
                UnityEngine.Vector4 textureCoords = GameState.SpriteAtlasManager.GetSprite(spriteId, Enums.AtlasType.Mech).TextureCoords;

                var x = entity.mechPosition2D.Value.X;
                var y = entity.mechPosition2D.Value.Y;
                var width = entity.mechSprite2D.Size.X;
                var height = entity.mechSprite2D.Size.Y;

                if (!Utility.ObjectMesh.isOnScreen(x, y))
                    continue;

                // Update UVs
                Mesh.UpdateUV(textureCoords, (index) * 4);
                // Update Vertices
                Mesh.UpdateVertex((index * 4), x, y, width, height);
                index++;
            }
        }
    }
}
