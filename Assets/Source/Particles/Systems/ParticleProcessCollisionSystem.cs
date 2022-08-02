using Collisions;
using KMath;
using PlanetTileMap;
using UnityEngine;
using Utility;
using Enums.Tile;

namespace Particle
{
    public class ParticleProcessCollisionSystem
    {

        public void Update(ParticleContext context, ref PlanetTileMap.TileMap tileMap)
        {
            float deltaTime = Time.deltaTime;
            var entitiesWithBox = context.GetGroup(ParticleMatcher.AllOf(ParticleMatcher.ParticleBox2DCollider, ParticleMatcher.ParticlePosition2D));

            foreach (var entity in entitiesWithBox)
            {
                var pos = entity.particlePosition2D;
                var box2DCollider = entity.particleBox2DCollider;

                var entityBoxBorders = new AABox2D(new Vec2f(pos.PreviousPosition.X, pos.Position.Y) + box2DCollider.Offset, box2DCollider.Size);

                if (entityBoxBorders.IsCollidingBottom(tileMap, pos.Velocity))
                {
                    var tile = tileMap.GetTile((int)pos.Position.X, (int)pos.Position.Y);
                    var property = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);

                    entityBoxBorders.DrawBox();

                    pos.Position = new Vec2f(pos.Position.X, pos.PreviousPosition.Y);
                    pos.Velocity.Y = 0.0f;
                    pos.Acceleration.Y = 0.0f;
                    pos.Velocity.X = 0.0f;
                    pos.Acceleration.X = 0.0f;

                }
                else if (entityBoxBorders.IsCollidingTop(tileMap, pos.Velocity))
                {   
                    pos.Position = new Vec2f(pos.Position.X, pos.PreviousPosition.Y);
                    pos.Velocity.Y = 0.0f;
                    pos.Acceleration.Y = 0.0f;
                }

                entityBoxBorders = new AABox2D(new Vec2f(pos.Position.X, pos.PreviousPosition.Y) + box2DCollider.Offset, box2DCollider.Size);

                if (entityBoxBorders.IsCollidingLeft(tileMap, pos.Velocity))
                {
                    pos.Position = new Vec2f(pos.PreviousPosition.X, pos.Position.Y);
                    pos.Velocity.X = 0.0f;
                    pos.Acceleration.X = 0.0f;
                }
                else if (entityBoxBorders.IsCollidingRight(tileMap, pos.Velocity))
                {
                    pos.Position = new Vec2f(pos.PreviousPosition.X, pos.Position.Y);
                    pos.Velocity.X = 0.0f;
                    pos.Acceleration.X = 0.0f;
                }

                entityBoxBorders.DrawBox();
            }
        }
    }
}
