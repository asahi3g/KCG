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
        private void Update(Planet.PlanetState planet, AgentEntity entity, float deltaTime)
        {       
            PhysicsStateComponent physicsState = entity.agentPhysicsState;
            Box2DColliderComponent box2DCollider = entity.physicsBox2DCollider;
            AABox2D entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.Position.Y) + box2DCollider.Offset, box2DCollider.Size);
            PlanetTileMap.TileMap tileMap = planet.TileMap;


            Vec2f delta = physicsState.Position - physicsState.PreviousPosition;           

           /* var horizontalCollisionResult = TileCollisions.HandleCollisionH(entity, new Vec2f(delta.X, 0.0f), planet, true);
            physicsState.Position.X = physicsState.PreviousPosition.X + delta.X * (horizontalCollisionResult.MinTime - 0.01f);
            physicsState.PreviousPosition.X = physicsState.Position.X;
            var verticalCollisionResult = TileCollisions.HandleCollisionV(entity, new Vec2f(0.0f, delta.Y), planet, true);
            physicsState.Position.Y = physicsState.PreviousPosition.Y + delta.Y * (verticalCollisionResult.MinTime - 0.01f);*/



            float minTime = 1.0f;
            Vec2f minNormal = new Vec2f();

            
            var bottomCollision = TileCollisions.HandleCollisionCircleBottom(entity, delta, planet);

            var topCollision = TileCollisions.HandleCollisionCircleTop(entity, delta, planet);

            if (bottomCollision.MinTime <= topCollision.MinTime)
            {
                minTime = bottomCollision.MinTime;
                minNormal = bottomCollision.MinNormal;
            }
            else
            {
                minTime = topCollision.MinTime;
                minNormal = topCollision.MinNormal;
            }

            
            
            physicsState.Position = physicsState.PreviousPosition + delta * (minTime - 0.01f);

            if (minTime < 1.0)
            {

               // physicsState.Position -= delta.Normalize() * 0.02f;
               float coefficientOfRest = 0.6f;
               Vec2f velocity = delta;


               float N = velocity.X * velocity.X + velocity.Y * velocity.Y; // length squared
               Vec2f normalized = new Vec2f(velocity.X / N, velocity.Y / N); // normalized

               normalized = normalized - 2.0f * Vec2f.Dot(normalized, minNormal) * minNormal;
                Vec2f reflectVelocity = normalized * coefficientOfRest * N;
                physicsState.Position += reflectVelocity;
            }


           // if (isColliding)
           // {
                //TileCollisions.HandleCollision(entity, planet, false);
          //  }

        /*    bool collidingBottom = verticalCollisionResult.isCollidingBottom;
            bool collidingLeft = horizontalCollisionResult.isCollidingLeft ;//physicsState.Velocity.X < 0 && isColliding;
            bool collidingRight = horizontalCollisionResult.isCollidingRight ; //physicsState.Velocity.X > 0 && isColliding;
            bool collidingTop = verticalCollisionResult.isCollidingTop ; //physicsState.Velocity.Y > 0 && isColliding;*/


            bool collidingBottom = bottomCollision.isCollidingBottom || topCollision.isCollidingBottom;
            bool collidingLeft = bottomCollision.isCollidingLeft || topCollision.isCollidingLeft;//physicsState.Velocity.X < 0 && isColliding;
            bool collidingRight = bottomCollision.isCollidingRight || topCollision.isCollidingRight; //physicsState.Velocity.X > 0 && isColliding;
            bool collidingTop = bottomCollision.isCollidingTop || topCollision.isCollidingTop; //physicsState.Velocity.Y > 0 && isColliding;      
            bool slidingLeft = bottomCollision.isSlidingLeft || topCollision.isSlidingLeft;//physicsState.Velocity.X < 0 && isColliding;
            bool slidingRight = bottomCollision.isSlidingRight || topCollision.isSlidingRight; //physicsState.Velocity.X > 0 && isColliding;


            collidingBottom = false;
            collidingLeft = false;
            collidingRight = false;
            collidingTop = false;



            Vec2f collisionDirection = -minNormal;

            UnityEngine.Debug.Log(minNormal);
            UnityEngine.Debug.Log(collisionDirection);


            if (minTime < 1.0)
            {
                if (Mathf.Abs(collisionDirection.X) > Mathf.Abs(collisionDirection.Y))
                {

                    if (collisionDirection.X > 0.0f)
                    {
                        // colliding right
                        collidingRight = true;

                    }
                    else
                    {
                        // colliding left
                        collidingLeft = true;
                    }
                }
                else 
                {
                    if (collisionDirection.Y > 0)
                    {
                        // colliding top
                        collidingTop = true;
                    }
                    else
                    {
                        // colliding bottom
                        collidingBottom = true;
                    }
                }
            }


          

          /*  if (collidingTop)
            UnityEngine.Debug.Log(minNormal);*/
            if (collidingBottom)UnityEngine.Debug.Log("collidingBottom " + collidingBottom);
            if (collidingLeft)UnityEngine.Debug.Log("collidingLeft " + collidingLeft);
            if (collidingRight)UnityEngine.Debug.Log("collidingRight " + collidingRight);
            if (collidingTop)UnityEngine.Debug.Log("collidingTop " + collidingTop);



           // physicsState.OnGrounded = true;
            //return ;



           /* bool collidingBottom = TileCollisions.HandleCollidingBottom(entity, planet, true);
            bool collidingLeft = TileCollisions.HandleCollidingLeft(entity, planet, true);
            bool collidingRight = TileCollisions.HandleCollidingRight(entity, planet, true);
            bool collidingTop = TileCollisions.HandleCollidingTop(entity, planet, true);

            TileCollisions.HandleCollidingBottom(entity, planet, false);
            TileCollisions.HandleCollidingLeft(entity, planet, false);
            TileCollisions.HandleCollidingRight(entity, planet, false);
             TileCollisions.HandleCollidingTop(entity, planet, false);*/





            //TileCollisions.StaticCheck(entity, planet);


            if (collidingBottom)
            {
                bool isPlatform = true; // if all colliding blocks are plataforms.
                for(int i = (int)entityBoxBorders.xmin; i <= (int)entityBoxBorders.xmax; i++)
                {
                    var tile = tileMap.GetTile(i, (int)entityBoxBorders.ymin);
                    var property = GameState.TileCreationApi.GetTileProperty(tile.FrontTileID);

                    if (!property.IsAPlatform)
                    {
                        isPlatform = false;
                    }
                }


                if (!isPlatform || !physicsState.Droping)
                {
                   // physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);
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


            if (collidingTop)
            {   
                /*physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);*/
                physicsState.Velocity.Y = 0.0f;
                physicsState.Acceleration.Y = 0.0f;
            }

            entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y) + box2DCollider.Offset, box2DCollider.Size);


           // if (physicsState.Position.Y <= 16.0f)physicsState.Position.Y = 16.0f;

           if (collidingLeft)
            {
                /*physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);*/
                physicsState.Velocity.X = 0.0f;
                physicsState.Acceleration.X = 0.0f;
                if (slidingLeft)
                {
                    entity.SlideLeft();
                }
           }
            else if (collidingRight)
            {
               /* physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);*/
                physicsState.Velocity.X = 0.0f;
                physicsState.Acceleration.X = 0.0f;
                if (slidingRight)
                {
                    entity.SlideRight();
                }
            }

           // if (physicsState.Position.Y <= 16.0f)physicsState.Position.Y = 16.0f;


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

        public void Update(AgentContext agentContext, Planet.PlanetState planet)
        {
            float deltaTime = Time.deltaTime;
            var agentEntitiesWithBox = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.PhysicsBox2DCollider, AgentMatcher.AgentPhysicsState));

            foreach (var agentEntity in agentEntitiesWithBox)
            {
                Update(planet, agentEntity, deltaTime); 
            }
        }
    }
}
