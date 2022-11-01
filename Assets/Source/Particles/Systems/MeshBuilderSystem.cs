//import UnityEngine

using KMath;

namespace Particle
{
    public class MeshBuilderSystem
    {
        public Utility.FrameMesh Mesh;

        public void Initialize(UnityEngine.Material material, UnityEngine.Transform transform, int drawOrder = 0)
        {
            Mesh = new Utility.FrameMesh("ParticlesGameObject", material, transform,
                GameState.SpriteAtlasManager.GetSpriteAtlas(Enums.AtlasType.Particle), drawOrder);
        }

        public void UpdateMesh()
        {
            var entities = GameState.Planet.EntitasContext.particle.GetGroup(ParticleMatcher.AllOf(ParticleMatcher.ParticleSprite2D));
            
            Mesh.Clear();
            int index = 0;
            foreach (var entity in entities)
            {
                int spriteId = entity.particleSprite2D.SpriteId;

                if (entity.hasParticleAnimation)
                {
                    var animation = entity.particleAnimation;
                    spriteId = animation.State.GetSpriteId();
                }

                
                if (spriteId >= 0)
                {
                   UnityEngine.Vector4 textureCoords = new UnityEngine.Vector4();
                   textureCoords = GameState.SpriteAtlasManager.GetSprite(spriteId, Enums.AtlasType.Particle).TextureCoords;

                   var pos = entity.particlePhysicsState.Position;
                    
                    var width = entity.particleSprite2D.Size.X;
                    var height = entity.particleSprite2D.Size.Y;
                    var x = pos.X;
                    var y = pos.Y;

                    if (!Utility.ObjectMesh.isOnScreen(x, y))
                        continue;

                    // Update UVs
                    Mesh.UpdateUV(textureCoords, (index) * 4);
                    // Update Vertices
                    Mesh.UpdateVertex((index * 4), x, y, width, height, entity.particlePhysicsState.Rotation);

                }
                else
                {
                    Vec2f[] textureCoords = entity.particleSprite2D.TextureCoords;


                    var pos = entity.particlePhysicsState.Position;
                    var x = pos.X;
                    var y = pos.Y;
                    var width = entity.particleSprite2D.Size.X;
                    var height = entity.particleSprite2D.Size.Y;

                    if (!Utility.ObjectMesh.isOnScreen(x, y))
                        continue;

                    // Update UVs
                    Mesh.UpdateUV(textureCoords);
                    // Update Vertices
                    Mesh.UpdateVertex(entity.particleSprite2D.Vertices, x, y, entity.particlePhysicsState.Rotation);
                }

                index++;
            }
        }
    }
}
