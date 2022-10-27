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

            
            var bottomCollision = TileCollisions.HandleCollisionCircleBottom(entity,  delta, planet);

            var topCollision = TileCollisions.HandleCollisionCircleTop(entity, delta, planet);

            //var sidesCollidion = TileCollisions.HandleCollisionSides(entity, delta, planet);



            if (bottomCollision.MinTime <= topCollision.MinTime)
            {
                minTime = bottomCollision.MinTime;
                minNormal = bottomCollision.MinNormal;
            }
            else /*if (topCollision.MinTime <= sidesCollidion.MinTime)*/
            {
               minTime = topCollision.MinTime;
                minNormal = topCollision.MinNormal;
            }
            /*else
            {
                minTime = sidesCollidion.MinTime;
                minNormal = sidesCollidion.MinNormal;
            }*/



            
            
            float epsilon = 0.01f;
            physicsState.Position = physicsState.PreviousPosition + delta * (minTime - epsilon);
            Vec2f deltaLeft = delta  * (1.0f - (minTime - epsilon));

            if (minTime < 1.0 && (minNormal.X != 0 || minNormal.Y != 0))
            {

               // physicsState.Position -= delta.Normalize() * 0.02f;
               float coefficientOfRest = 0.2f;
               Vec2f velocity = delta;


               float N = velocity.X * velocity.X + velocity.Y * velocity.Y; // length squared
               Vec2f normalized = new Vec2f(velocity.X / N, velocity.Y / N); // normalized

               normalized = normalized - 2.0f * Vec2f.Dot(normalized, minNormal) * minNormal;
                Vec2f reflectVelocity = normalized * coefficientOfRest * N;
                //physicsState.Velocity = physicsState.Velocity - 1.0f * Vec2f.Dot(physicsState.Velocity, minNormal) * minNormal;
               physicsState.Position += reflectVelocity;
             //physicsState.Velocity = reflectVelocity * 60.0f;
               //physicsState.Position += new Vec2f(minNormal.Y, -minNormal.X) * deltaLeft;
               //physicsState.Position += deltaLeft - 1.0f * Vec2f.Dot(deltaLeft, minNormal) * minNormal;
            }



            bool collidingBottom = false;
            bool collidingLeft = false;
            bool collidingRight = false;
            bool collidingTop = false;



            Vec2f collisionDirection = -minNormal;


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

            bool slidingLeft = collidingLeft && (topCollision.GeometryTileShape == Enums.GeometryTileShape.SB_R0 ||
             bottomCollision.GeometryTileShape == Enums.GeometryTileShape.SB_R0);

             bool slidingRight = collidingRight && (topCollision.GeometryTileShape == Enums.GeometryTileShape.SB_R0 ||
             bottomCollision.GeometryTileShape == Enums.GeometryTileShape.SB_R0);

             slidingLeft = false;
             slidingRight = false;



             var rs = TileCollisions.RaycastGround(entity, planet);

             if (rs.MinTime < 1.0f)
             {
                physicsState.GroundNormal = rs.MinNormal;
                if (physicsState.Velocity.Y < 10.0f)
                {
                    physicsState.OnGrounded = true;
                }
                else
                {
                    physicsState.OnGrounded = false;
                }
             }
             else
             {
                physicsState.GroundNormal = new Vec2f(0.0f, 1.0f);
                physicsState.OnGrounded = false;
             }

             UnityEngine.Debug.Log("velocity : " + physicsState.Velocity);

            // physicsState.GroundNormal = new Vec2f(-1.0f, 1.0f).Normalized;

           /*  UnityEngine.Debug.Log(rs.MinTime);            
             UnityEngine.Debug.Log(physicsState.GroundNormal);
             UnityEngine.Debug.Log(rs.GeometryTileShape);*/

             //physicsState.GroundNormal = new Vec2f(-1.0f, 1.0f).Normalized;

          

          /*  if (collidingBottom)UnityEngine.Debug.Log("collidingBottom " + collidingBottom);
            if (collidingLeft)UnityEngine.Debug.Log("collidingLeft " + collidingLeft);
            if (collidingRight)UnityEngine.Debug.Log("collidingRight " + collidingRight);
            if (collidingTop)UnityEngine.Debug.Log("collidingTop " + collidingTop);


*/
           // physicsState.OnGrounded = true;
            //return ;


            
            /*Vec2f colliderPosition = physicsState.Position + box2DCollider.Offset;
            int X1 = (int)(colliderPosition.X + box2DCollider.Size.X * 0.5f);
            int Y1 = (int)(colliderPosition.Y);

            planet.AddDebugLine(new Line2D(new Vec2f((float)X1, (float)Y1), new Vec2f((float)X1 + 1.0f, (float)Y1)), Color.blue);

            TileID id = tileMap.GetFrontTileID(X1, Y1);

            var properties = GameState.TileCreationApi.GetTileProperty(id);
            if (properties.BlockShapeType == Enums.GeometryTileShape.TB_R0)
            {
                physicsState.GroundNormal = new Vec2f(-1.0f, 1.0f).Normalized;
            }
            else if (properties.BlockShapeType == Enums.GeometryTileShape.SB_R0)
            {
                physicsState.GroundNormal = new Vec2f(0.0f, 1.0f);
            }
            else
            {
                physicsState.GroundNormal = new Vec2f(0.0f, 1.0f);
            }


            int X2 = (int)(colliderPosition.X + box2DCollider.Size.X * 0.5f + 1.0f);
            int Y2 = (int)(colliderPosition.Y);

            planet.AddDebugLine(new Line2D(new Vec2f((float)X2, (float)Y2), new Vec2f((float)X2 + 1.0f, (float)Y2)), Color.blue);

            id = tileMap.GetFrontTileID(X2, Y2);

            properties = GameState.TileCreationApi.GetTileProperty(id);
            if (properties.BlockShapeType == Enums.GeometryTileShape.TB_R0)
            {
                physicsState.GroundNormal += new Vec2f(-1.0f, 1.0f).Normalized;
  
            }
            else if (properties.BlockShapeType == Enums.GeometryTileShape.SB_R0)
            {
                physicsState.GroundNormal += new Vec2f(0.0f, 1.0f);
            }
            else
            {
                physicsState.GroundNormal += new Vec2f(0.0f, 1.0f);
            }

              physicsState.GroundNormal.Normalize();*/



            //TileCollisions.StaticCheck(entity, planet);


            if (collidingBottom)
            {
                physicsState.OnGrounded = true;
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
                  //  physicsState.OnGrounded = true;
                    if (!isPlatform)
                        physicsState.Droping = false;
                }
                else
                {
                 //   physicsState.OnGrounded = false;
                }
            }
            else
            {
             //   physicsState.OnGrounded = false;
                physicsState.Droping = false;
            }


            if (collidingTop)
            {   
                //physicsState.Position = new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y);
                physicsState.Velocity.Y = 0.0f;
                physicsState.Acceleration.Y = 0.0f;
            }

            entityBoxBorders = new AABox2D(new Vec2f(physicsState.Position.X, physicsState.PreviousPosition.Y) + box2DCollider.Offset, box2DCollider.Size);


           // if (physicsState.Position.Y <= 16.0f)physicsState.Position.Y = 16.0f;

           if (collidingLeft)
            {
                //physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);
                physicsState.Velocity.X = 0.0f;
                physicsState.Acceleration.X = 0.0f;
                if (slidingLeft)
                {
                    entity.SlideLeft();
                }
           }
            else if (collidingRight)
            {
               // physicsState.Position = new Vec2f(physicsState.PreviousPosition.X, physicsState.Position.Y);
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

            //entityBoxBorders.DrawBox();
        }

        public void Update(AgentContext agentContext, Planet.PlanetState planet)
        {
            float deltaTime = UnityEngine.Time.deltaTime;
            var agentEntitiesWithBox = agentContext.GetGroup(AgentMatcher.AllOf(AgentMatcher.PhysicsBox2DCollider, AgentMatcher.AgentPhysicsState));

            foreach (var agentEntity in agentEntitiesWithBox)
            {
                Update(planet, agentEntity, deltaTime); 
            }
        }
    }
}
