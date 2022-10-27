namespace Projectile
{
    public class MeshBuilderSystem
    {
        public Utility.FrameMesh Mesh;

        public void Initialize(UnityEngine.Material material, UnityEngine.Transform transform, int drawOrder = 0)
        {
            Mesh = new Utility.FrameMesh("ProjectilesGameObject", material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Particle), drawOrder);
        }

        public void UpdateMesh()
        {
            var projectilessWithSprite = GameState.Planet.EntitasContext.projectile.GetGroup(ProjectileMatcher.AllOf(ProjectileMatcher.ProjectileSprite2D));

            Mesh.Clear();
            int index = 0;
            foreach (var entity in projectilessWithSprite)
            {
                int spriteId = entity.projectileSprite2D.SpriteId;

                if (entity.hasAnimationState)
                {
                    var animation = entity.animationState;
                    spriteId = animation.State.GetSpriteId();
                }

                UnityEngine.Vector4 textureCoords = GameState.SpriteAtlasManager.GetSprite(spriteId, Enums.AtlasType.Particle).TextureCoords;

               
                var width = entity.projectileSprite2D.Size.X;
                var height = entity.projectileSprite2D.Size.Y;
                var x = entity.projectilePhysicsState.Position.X;
                var y = entity.projectilePhysicsState.Position.Y;

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
