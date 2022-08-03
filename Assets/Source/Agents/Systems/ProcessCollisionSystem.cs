using Collisions;
using KMath;
using PlanetTileMap;
using UnityEngine;
using Utility;
using Physics;
using Enums.Tile;

namespace Agent
{
    //TODO: Collision calculation should internally cache the chunks around player
    //TODO: (up left, up right, bottom left, bottom right) instead of doing GetTile for each tile.
    //TODO: Implement Prediction Movement Collision
    //TODO: Create broad-phase for getting tiles
    // https://www.flipcode.com/archives/Raytracing_Topics_Techniques-Part_4_Spatial_Subdivisions.shtml
    // http://www.cs.yorku.ca/~amana/research/grid.pdf
    public class ProcessCollisionSystem
    {
        private void Update(ref TileMap tileMap, Position2DComponent pos, MovableComponent movable, Box2DColliderComponent box2DCollider, float deltaTime)
        {       
            AABox2D entityBoxBorders = new AABox2D(new Vec2f(pos.PreviousValue.X, pos.Value.Y) + box2DCollider.Offset, box2DCollider.Size);

            
            if (entityBoxBorders.IsCollidingBottom(tileMap, movable.Velocity))
            {
                bool isPlatform = false;
                for(int i = (int)entityBoxBorders.xmin; i <= (int)entityBoxBorders.xmax; i++)
                {
                    var tile = tileMap.GetTile(i, (int)entityBoxBorders.ymin);
                    var property = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);

                    if (property.IsAPlatform)
                    {
                        isPlatform = true;
                    }
                }

                

                entityBoxBorders.DrawBox();

                if (!isPlatform || !movable.Droping)
                {
                    pos.Value = new Vec2f(pos.Value.X, pos.PreviousValue.Y);
                    movable.Velocity.Y = 0.0f;
                    movable.Acceleration.Y = 0.0f;
                    movable.OnGrounded = true;
                }
                else if (isPlatform && movable.Droping)
                {
                    if (!movable.WantToDrop)
                    {
                        movable.Droping = false;
                    }
                    else
                    {
                        movable.OnGrounded = false;
                        movable.Acceleration.Y = -100.0f;
                    }
                    
                }

            }
            else
            {
                movable.WantToDrop = false;
                movable.Droping = false;
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
                movable.SlidingLeft = true;
            }
            else if (entityBoxBorders.IsCollidingRight(tileMap, movable.Velocity))
            {
                pos.Value = new Vec2f(pos.PreviousValue.X, pos.Value.Y);
                movable.Velocity.X = 0.0f;
                movable.Acceleration.X = 0.0f;
                movable.SlidingRight = true;
            }

            Vec2f position = pos.Value;
            position.X += box2DCollider.Size.X / 2.0f;
            //position.Y -= box2DCollider.Size.Y / 2.0f;

            if ((int)position.X > 0 && (int)position.X + 1 < tileMap.MapSize.X &&
            (int)position.Y > 0 && (int)position.Y < tileMap.MapSize.Y)
            {
                if (tileMap.GetFrontTileID((int)position.X + 1, (int)position.Y)== TileID.Air)
                {
                    movable.SlidingRight = false;
                }
            }

            if ((int)position.X > 0 && (int)position.X - 1 < tileMap.MapSize.X &&
            (int)position.Y > 0 && (int)position.Y < tileMap.MapSize.Y)
            {
                if (tileMap.GetFrontTileID((int)position.X - 1, (int)position.Y) == TileID.Air)
                {
                    movable.SlidingLeft = false;
                }
            }

            entityBoxBorders.DrawBox();
        }

        public void Update(AgentContext agentContext, ref PlanetTileMap.TileMap tileMap)
        {
            float deltaTime = Time.deltaTime;
            var agentEntitiesWithBox = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.PhysicsBox2DCollider, AgentMatcher.AgentPosition2D));

            foreach (var agentEntity in agentEntitiesWithBox)
            {
                var pos = agentEntity.agentPosition2D;
                var movable = agentEntity.agentMovable;
                var box2DCollider = agentEntity.physicsBox2DCollider;

                Update(ref tileMap, pos, movable, box2DCollider, deltaTime); 
            }
        }
    }
}
