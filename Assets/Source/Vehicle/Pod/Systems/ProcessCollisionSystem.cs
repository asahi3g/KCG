using Collisions;
using KMath;
using UnityEngine;

namespace Pod
{
    public class ProcessCollisionSystem
    {
        public void Update(ref Planet.PlanetState planet)
        {
            // Get Delta Time
            float deltaTime = Time.deltaTime;

            // Get Vehicle Physics Entity
            var entities = planet.EntitasContext.pod.GetGroup(PodMatcher.AllOf(PodMatcher.PhysicsBox2DCollider, PodMatcher.PodPhysicsState2D));

            foreach (var entity in entities)
            {
                // Set Vehicle Physics to variable
                var pos = entity.podPhysicsState2D;

                var size = entity.physicsBox2DCollider.Size;

                // Create Box Borders
                var entityBoxBorders = new AABox2D(new Vec2f(pos.Position.X, pos.Position.Y) + entity.physicsBox2DCollider.Offset, size);

                // If is colliding bottom-top stop y movement
                if (entityBoxBorders.IsCollidingTop(planet.TileMap, pos.angularVelocity))
                {
                    pos.angularVelocity = new Vec2f(pos.angularVelocity.X, 0.15f);
                }
                else if (entityBoxBorders.IsCollidingBottom(planet.TileMap, pos.angularVelocity))
                {
                    pos.angularVelocity = new Vec2f(pos.angularVelocity.X, 0.15f);
                }

                pos = entity.podPhysicsState2D;

                size = entity.physicsBox2DCollider.Size;

                entityBoxBorders = new AABox2D(new Vec2f(pos.Position.X, pos.Position.Y) + entity.physicsBox2DCollider.Offset, size);

                // If is colliding left-right stop x movement
                if (entityBoxBorders.IsCollidingLeft(planet.TileMap, pos.angularVelocity))
                {
                    pos.angularVelocity = new Vec2f(0.15f, pos.angularVelocity.Y);
                }
                else if (entityBoxBorders.IsCollidingRight(planet.TileMap, pos.angularVelocity))
                {
                    pos.angularVelocity = new Vec2f(0.15f, pos.angularVelocity.Y);
                }
            }
        }

        public void DrawGizmos(ref Planet.PlanetState planet)
        {
            // Get Delta Time
            float deltaTime = Time.deltaTime;

            // Get Vehicle Physics Entity
            var entities = planet.EntitasContext.vehicle.GetGroup(VehicleMatcher.AllOf(VehicleMatcher.PhysicsBox2DCollider, VehicleMatcher.VehiclePhysicsState2D));

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

