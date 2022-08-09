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
        private void Update(ref TileMap tileMap, PhysicsStateComponent physicsState, Box2DColliderComponent box2DCollider, float deltaTime)
        {       
            AABox2D entityBoxBorders = new AABox2D(new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y) + box2DCollider.Offset, box2DCollider.Size);

            
            if (entityBoxBorders.IsCollidingBottom(tileMap, physicsState.Velocity))
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

                if (!isPlatform || !physicsState.Droping)
                {
                    physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);
                    physicsState.Velocity.Y = 0.0f;
                    physicsState.Acceleration.Y = 0.0f;
                    physicsState.OnGrounded = true;
                }
                else if (isPlatform && physicsState.Droping)
                {
                    if (!physicsState.WantToDrop)
                    {
                        physicsState.Droping = false;
                    }
                    else
                    {
                        physicsState.OnGrounded = false;
                        physicsState.Acceleration.Y = -100.0f;
                    }
                }
            }
            else
            {
                physicsState.WantToDrop = false;
                physicsState.Droping = false;
                physicsState.OnGrounded = false;
            }


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
                physicsState.MovementState = Enums.AgentMovementState.SlidingLeft;
            }
            else if (entityBoxBorders.IsCollidingRight(tileMap, physicsState.Velocity))
            {
                physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);
                physicsState.Velocity.X = 0.0f;
                physicsState.Acceleration.X = 0.0f;
                physicsState.MovementState = Enums.AgentMovementState.SlidingRight;
            }

            Vec2f position = physicsState.Position;
            position.X += box2DCollider.Size.X / 2.0f;
            //position.Y -= box2DCollider.Size.Y / 2.0f;

            if ((int)position.X > 0 && (int)position.X + 1 < tileMap.MapSize.X &&
            (int)position.Y > 0 && (int)position.Y < tileMap.MapSize.Y)
            {
                if (tileMap.GetFrontTileID((int)position.X + 1, (int)position.Y)== TileID.Air)
                {
                    if (physicsState.MovementState == Enums.AgentMovementState.SlidingRight)
                        physicsState.MovementState = Enums.AgentMovementState.None;
                }
            }

            if ((int)position.X > 0 && (int)position.X - 1 < tileMap.MapSize.X &&
            (int)position.Y > 0 && (int)position.Y < tileMap.MapSize.Y)
            {
                if (tileMap.GetFrontTileID((int)position.X - 1, (int)position.Y) == TileID.Air)
                {
                    if (physicsState.MovementState == Enums.AgentMovementState.SlidingLeft)
                        physicsState.MovementState = Enums.AgentMovementState.None;
                }
            }

            entityBoxBorders.DrawBox();
        }

        public void Update(AgentContext agentContext, ref PlanetTileMap.TileMap tileMap)
        {
            float deltaTime = Time.deltaTime;
            var agentEntitiesWithBox = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.PhysicsBox2DCollider, AgentMatcher.AgentPhysicsState));

            foreach (var agentEntity in agentEntitiesWithBox)
            {
                var physicsState = agentEntity.agentPhysicsState;
                var box2DCollider = agentEntity.physicsBox2DCollider;

                Update(ref tileMap, physicsState, box2DCollider, deltaTime); 
            }
        }
    }
}
