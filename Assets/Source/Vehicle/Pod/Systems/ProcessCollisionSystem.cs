using Collisions;
using KMath;
using UnityEngine;

namespace Vehicle.Pod
{
    public class ProcessCollisionSystem
    {
        public void Update()
        {
            // Get Delta Time
            float deltaTime = Time.deltaTime;

            // Get Vehicle Physics Entity
            var entities = GameState.Planet.EntitasContext.pod.GetGroup(PodMatcher.AllOf(PodMatcher.PhysicsBox2DCollider, PodMatcher.VehiclePodPhysicsState2D));

            foreach (var entity in entities)
            {
                // Set Vehicle Physics to variable
                var pos = entity.vehiclePodPhysicsState2D;

                var size = entity.physicsBox2DCollider.Size;

                // Create Box Borders
                var entityBoxBorders = new AABox2D(new Vec2f(pos.Position.X, pos.Position.Y) + entity.physicsBox2DCollider.Offset, size);

                // If is colliding bottom-top stop y movement
                if (entityBoxBorders.IsCollidingTop(GameState.Planet.TileMap, pos.angularVelocity))
                {
                    pos.angularVelocity = new Vec2f(pos.angularVelocity.X, 0.15f);
                }
                else if (entityBoxBorders.IsCollidingBottom(pos.angularVelocity))
                {
                    pos.angularVelocity = new Vec2f(pos.angularVelocity.X, 0.15f);
                }

                pos = entity.vehiclePodPhysicsState2D;

                size = entity.physicsBox2DCollider.Size;

                entityBoxBorders = new AABox2D(new Vec2f(pos.Position.X, pos.Position.Y) + entity.physicsBox2DCollider.Offset, size);

                // If is colliding left-right stop x movement
                if (entityBoxBorders.IsCollidingLeft(pos.angularVelocity))
                {
                    pos.angularVelocity = new Vec2f(0.15f, pos.angularVelocity.Y);
                }
                else if (entityBoxBorders.IsCollidingRight(GameState.Planet.TileMap, pos.angularVelocity))
                {
                    pos.angularVelocity = new Vec2f(0.15f, pos.angularVelocity.Y);
                }

                var RightPanelCollision = new AABox2D(new Vec2f(entity.vehiclePodStatus.RightPanelPos.X, entity.vehiclePodStatus.RightPanelPos.Y), new Vec2f(0.01f, 0.01f));

                var LeftPanelCollision = new AABox2D(new Vec2f(entity.vehiclePodStatus.LeftPanelPos.X, entity.vehiclePodStatus.LeftPanelPos.Y), new Vec2f(0.01f, 0.01f));

                var TopPanelCollision = new AABox2D(new Vec2f(entity.vehiclePodStatus.TopPanelPos.X, entity.vehiclePodStatus.TopPanelPos.Y), new Vec2f(0.01f, 0.01f));

                var BottomPanelCollision = new AABox2D(new Vec2f(entity.vehiclePodStatus.BottomPanelPos.X, entity.vehiclePodStatus.BottomPanelPos.Y), new Vec2f(0.01f, 0.01f));

                if(entity.vehiclePodStatus.Exploded)
                {
                    if(RightPanelCollision.IsCollidingLeft(new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.RightPanelCollided = true;
                    }
                    else if (RightPanelCollision.IsCollidingRight(GameState.Planet.TileMap, new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.RightPanelCollided = true;
                    }
                    else if (RightPanelCollision.IsCollidingTop(GameState.Planet.TileMap, new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.RightPanelCollided = true;
                    }
                    else if (RightPanelCollision.IsCollidingBottom(new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.RightPanelCollided = true;
                    }

                    if (LeftPanelCollision.IsCollidingLeft(new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.LeftPanelCollided = true;
                    }
                    else if (LeftPanelCollision.IsCollidingRight(GameState.Planet.TileMap, new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.LeftPanelCollided = true;
                    }
                    else if (LeftPanelCollision.IsCollidingTop(GameState.Planet.TileMap, new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.LeftPanelCollided = true;
                    }
                    else if (LeftPanelCollision.IsCollidingBottom(new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.LeftPanelCollided = true;
                    }

                    if (TopPanelCollision.IsCollidingLeft(new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.TopPanelCollided = true;
                    }
                    else if (TopPanelCollision.IsCollidingRight(GameState.Planet.TileMap, new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.TopPanelCollided = true;
                    }
                    else if (TopPanelCollision.IsCollidingTop(GameState.Planet.TileMap, new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.TopPanelCollided = true;
                    }
                    else if (TopPanelCollision.IsCollidingBottom(new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.TopPanelCollided = true;
                    }

                    if (BottomPanelCollision.IsCollidingLeft(new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.BottomPanelCollided = true;
                    }
                    else if (BottomPanelCollision.IsCollidingRight(GameState.Planet.TileMap, new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.BottomPanelCollided = true;
                    }
                    else if (BottomPanelCollision.IsCollidingTop(GameState.Planet.TileMap, new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.BottomPanelCollided = true;
                    }
                    else if (BottomPanelCollision.IsCollidingBottom(new Vec2f(entity.vehiclePodPhysicsState2D.angularVelocity.X,
                        entity.vehiclePodPhysicsState2D.angularVelocity.Y)))
                    {
                        entity.vehiclePodStatus.BottomPanelCollided = true;
                    }
                }
            }
        }

        public void DrawGizmos()
        {
            // Get Delta Time
            float deltaTime = Time.deltaTime;

            // Get Vehicle Physics Entity
            var entities = GameState.Planet.EntitasContext.vehicle.GetGroup(VehicleMatcher.AllOf(VehicleMatcher.PhysicsBox2DCollider, VehicleMatcher.VehiclePhysicsState2D));

            foreach (var entity in entities)
            {
                // Set Vehicle Physics to variable
                var pos = entity.vehiclePhysicsState2D;

                var size = entity.physicsBox2DCollider.Size;

                // Create Box Borders
                var entityBoxBorders = new AABox2D(new Vec2f(pos.TempPosition.X, pos.Position.Y) + entity.physicsBox2DCollider.Offset, size);

                Gizmos.color = Color.red;
                Gizmos.DrawCube(new Vector3(entityBoxBorders.center.X, entityBoxBorders.center.Y), new Vector3(entityBoxBorders.halfSize.X * 2, entityBoxBorders.halfSize.Y * 2));
            }
        }
    }
}

