//imports UnityEngine

using Collisions;
using KMath;
using PlanetTileMap;
using Utility;
using Physics;
using Enums.PlanetTileMap;

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
        private void Update(AgentEntity entity, float deltaTime)
        {       
            ref var planet = ref GameState.Planet;
            PhysicsStateComponent physicsState = entity.agentPhysicsState;
            Box2DColliderComponent box2DCollider = entity.physicsBox2DCollider;
            AABox2D entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.Position.Y) + box2DCollider.Offset, box2DCollider.Size);

            
            if (entityBoxBorders.IsCollidingBottom(physicsState.Velocity))
            {
                bool isPlatform = true; // if all colliding blocks are plataforms.
                for(int i = (int)entityBoxBorders.xmin; i <= (int)entityBoxBorders.xmax; i++)
                {
                    var tile = planet.TileMap.GetTile(i, (int)entityBoxBorders.ymin);
                    var property = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);

                    if (!property.IsAPlatform)
                    {
                        isPlatform = false;
                    }
                }


                if (!isPlatform || !physicsState.Droping)
                {
                    physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);
                    physicsState.Velocity.Y = 0.0f;
                    physicsState.Acceleration.Y = 0.0f;
                    physicsState.OnGrounded = true;
                    if (!isPlatform)
                        physicsState.Droping = false;
                }
                else
                {
                    physicsState.OnGrounded = false;
                }
            }
            else
            {
                physicsState.OnGrounded = false;
                physicsState.Droping = false;
            }


            if (entityBoxBorders.IsCollidingTop(physicsState.Velocity))
            {   
                physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);
                physicsState.Velocity.Y = 0.0f;
                physicsState.Acceleration.Y = 0.0f;
            }

            entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y) + box2DCollider.Offset, box2DCollider.Size);

            if (entityBoxBorders.IsCollidingLeft(physicsState.Velocity))
            {
                physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);
                physicsState.Velocity.X = 0.0f;
                physicsState.Acceleration.X = 0.0f;
                entity.SlideLeft();
            }
            else if (entityBoxBorders.IsCollidingRight(physicsState.Velocity))
            {
                physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);
                physicsState.Velocity.X = 0.0f;
                physicsState.Acceleration.X = 0.0f;
                entity.SlideRight();
            }

            Vec2f position = physicsState.Position;
            position.X += box2DCollider.Size.X / 2.0f;
            //position.Y -= box2DCollider.Size.Y / 2.0f;

            if ((int)position.X > 0 && (int)position.X + 1 < planet.TileMap.MapSize.X &&
            (int)position.Y > 0 && (int)position.Y < planet.TileMap.MapSize.Y)
            {
                if (planet.TileMap.GetFrontTileID((int)position.X + 1, (int)position.Y)== TileID.Air)
                {
                    if (physicsState.MovementState == Enums.AgentMovementState.SlidingRight)
                        physicsState.MovementState = Enums.AgentMovementState.None;
                }
            }

            if ((int)position.X > 0 && (int)position.X - 1 < planet.TileMap.MapSize.X &&
            (int)position.Y > 0 && (int)position.Y < planet.TileMap.MapSize.Y)
            {
                if (planet.TileMap.GetFrontTileID((int)position.X - 1, (int)position.Y) == TileID.Air)
                {
                    if (physicsState.MovementState == Enums.AgentMovementState.SlidingLeft)
                        physicsState.MovementState = Enums.AgentMovementState.None;
                }
            }

            entityBoxBorders.DrawBox();
        }

        public void Update()
        {
            float deltaTime = UnityEngine.Time.deltaTime;
            var agentEntitiesWithBox = GameState.Planet.EntitasContext.agent.GetGroup(AgentMatcher.AllOf(AgentMatcher.PhysicsBox2DCollider, AgentMatcher.AgentPhysicsState));

            foreach (var agentEntity in agentEntitiesWithBox)
            {
                Update(agentEntity, deltaTime); 
            }
        }
    }
}
