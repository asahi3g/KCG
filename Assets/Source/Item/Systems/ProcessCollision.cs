using Collisions;
using KMath;
using PlanetTileMap;
using UnityEngine;
using Utility;
using Physics;
using Enums.Tile;

namespace Item
{
    //TODO: Collision calculation should internally cache the chunks around player
    //TODO: (up left, up right, bottom left, bottom right) instead of doing GetTile for each tile.
    //TODO: Implement Prediction Movement Collision
    //TODO: Create broad-phase for getting tiles
    // https://www.flipcode.com/archives/Raytracing_Topics_Techniques-Part_4_Spatial_Subdivisions.shtml
    // http://www.cs.yorku.ca/~amana/research/grid.pdf
    public class ProcessCollisionSystem
    {
        private void Update(ref TileMap tileMap, PhysicsStateComponent physicsState, Box2DColliderComponent box2DCollider, float deltaTime)
        {
            var entityBoxBorders = new AABox2D(new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y) + box2DCollider.Offset, box2DCollider.Size);

            if (entityBoxBorders.IsCollidingBottom(tileMap, physicsState.Velocity))
            {
                var tile = tileMap.GetTile((int)physicsState.Position.X, (int)physicsState.Position.Y);
                var property = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);

                physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);
                physicsState.Velocity.Y = 0.0f;
                physicsState.Acceleration.Y = 0.0f;
                physicsState.OnGrounded = true;
            }
            else
                physicsState.OnGrounded = false;

            if (entityBoxBorders.IsCollidingTop(tileMap, physicsState.Velocity))
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

            Vec2f physicsStateition = physicsState.Position;
            physicsStateition.X += box2DCollider.Size.X / 2.0f;
            //physicsStateition.Y -= box2DCollider.Size.Y / 2.0f;

            entityBoxBorders.DrawBox();
        }

        public void Update(ItemParticleContext itemContext, ref PlanetTileMap.TileMap tileMap)
        {
            float deltaTime = Time.deltaTime;
            var entitiesWithBox = itemContext.GetGroup(ItemParticleMatcher.AllOf(ItemParticleMatcher.PhysicsBox2DCollider, ItemParticleMatcher.ItemPhysicsState));

            foreach (var entity in entitiesWithBox)
            {
                var physicsState = entity.itemPhysicsState;
                var box2DColider = entity.physicsBox2DCollider;

                Update(ref tileMap, physicsState, box2DColider, deltaTime);
            }
        }
    }
}
