using Collisions;
using KMath;
using UnityEngine;

namespace Vehicle.Pod
{
    public class ProcessCollisionSystem
    {
<<<<<<< HEAD
        public void Update(Planet.PlanetState planet)
=======
        public void Update()
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
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
                if (entityBoxBorders.IsCollidingTop(pos.angularVelocity))
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
                else if (entityBoxBorders.IsCollidingRight(pos.angularVelocity))
                {
                    pos.angularVelocity = new Vec2f(0.15f, pos.angularVelocity.Y);
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

