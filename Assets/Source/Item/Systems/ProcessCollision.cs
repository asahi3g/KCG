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
        private void Update(ref TileMap tileMap, Position2DComponent pos, MovementComponent movable, Box2DColliderComponent box2DCollider, float deltaTime)
        {
            var entityBoxBorders = new AABox2D(new Vec2f(pos.PreviousValue.X, pos.Value.Y) + box2DCollider.Offset, box2DCollider.Size);

            if (entityBoxBorders.IsCollidingBottom(tileMap, movable.Velocity))
            {
                var tile = tileMap.GetTile((int)pos.Value.X, (int)pos.Value.Y);
                var property = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);

                entityBoxBorders.DrawBox();

                pos.Value = new Vec2f(pos.Value.X, pos.PreviousValue.Y);
                movable.Velocity.Y = 0.0f;
                movable.Acceleration.Y = 0.0f;
                movable.OnGrounded = true;
            }

            if (entityBoxBorders.IsCollidingTop(tileMap, movable.Velocity))
            {
                pos.Value = new Vec2f(pos.Value.X, pos.PreviousValue.Y);
                movable.Velocity.Y = 0.0f;
                movable.Acceleration.Y = 0.0f;
            }

            entityBoxBorders = new AABox2D(new Vec2f(pos.Value.X, pos.PreviousValue.Y) + box2DCollider.Offset, box2DCollider.Size);

            if (entityBoxBorders.IsCollidingLeft(tileMap, movable.Velocity))
            {
                pos.Value = new Vec2f(pos.PreviousValue.X, pos.Value.Y);
                movable.Velocity.X = 0.0f;
                movable.Acceleration.X = 0.0f;
            }
            else if (entityBoxBorders.IsCollidingRight(tileMap, movable.Velocity))
            {
                pos.Value = new Vec2f(pos.PreviousValue.X, pos.Value.Y);
                movable.Velocity.X = 0.0f;
                movable.Acceleration.X = 0.0f;
            }

            Vec2f position = pos.Value;
            position.X += box2DCollider.Size.X / 2.0f;
            //position.Y -= box2DCollider.Size.Y / 2.0f;

            entityBoxBorders.DrawBox();
        }

        public void Update(ItemParticleContext itemContext, ref PlanetTileMap.TileMap tileMap)
        {
            float deltaTime = Time.deltaTime;
            var entitiesWithBox = itemContext.GetGroup(ItemParticleMatcher.AllOf(ItemParticleMatcher.PhysicsBox2DCollider, ItemParticleMatcher.ItemPosition2D));

            foreach (var entity in entitiesWithBox)
            {
                var pos = entity.itemPosition2D;
                var movable = entity.itemMovement;
                var box2DColider = entity.physicsBox2DCollider;

                Update(ref tileMap, pos, movable, box2DColider, deltaTime);
            }
        }
    }
}
