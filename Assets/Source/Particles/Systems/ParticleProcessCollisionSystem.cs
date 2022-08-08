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
            var entitiesWithBox = context.GetGroup(ParticleMatcher.AllOf(ParticleMatcher.ParticleBox2DCollider));

            foreach (var entity in entitiesWithBox)
            {
                var physicsState = entity.particlePhysicsState;
                var box2DCollider = entity.particleBox2DCollider;

                var entityBoxBorders = new AABox2D(new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y) + box2DCollider.Offset, box2DCollider.Size);

                if (entityBoxBorders.IsCollidingBottom(tileMap, physicsState.Velocity))
                {
                    var tile = tileMap.GetTile((int)physicsState.Position.X, (int)physicsState.Position.Y);
                    var property = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);

                    entityBoxBorders.DrawBox();

                    physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);
                    physicsState.Velocity.Y = 0.0f;
                    physicsState.Acceleration.Y = 0.0f;
                    physicsState.Velocity.X = 0.0f;
                    physicsState.Acceleration.X = 0.0f;

                }
                else if (entityBoxBorders.IsCollidingTop(tileMap, physicsState.Velocity))
                {   
                    physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);
                    physicsState.Velocity.Y = 0.0f;
                    physicsState.Acceleration.Y = 0.0f;
                }

                entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y) + box2DCollider.Offset, box2DCollider.Size);

                if (entityBoxBorders.IsCollidingLeft(tileMap, physicsState.Velocity))
                {
                    physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);
                    physicsState.Velocity.X = 0.0f;
                    physicsState.Acceleration.X = 0.0f;
                }
                else if (entityBoxBorders.IsCollidingRight(tileMap, physicsState.Velocity))
                {
                    physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);
                    physicsState.Velocity.X = 0.0f;
                    physicsState.Acceleration.X = 0.0f;
                }

                entityBoxBorders.DrawBox();
            }
        }
    }
}
