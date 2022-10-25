using KMath;
using System;
using System.Runtime.CompilerServices;

namespace Collisions
{
    public static class LineOfSight
    {
        [Flags]
        enum IsBehindFlag : byte
        {
            BottonLeft = 1 << 0, 
            TopLeft = 1 << 1,
            BottonRight = 1 << 2,
            TopRight = 1 << 3
        }

        // Todo(Joao): Remove magic numbers.
        public static bool CanSee(ref Planet.PlanetState planet, int agentID, int targetAgentID)
        {
            AgentEntity agentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(agentID);
            AgentEntity targetAgentEntity = planet.EntitasContext.agent.GetEntityWithAgentID(targetAgentID);

            // This is a temporary solution to get eys position. Todo: Implement a way to get eys pos
            Vec2f agentEyes = agentEntity.agentPhysicsState.Position + agentEntity.physicsBox2DCollider.Size * 0.9f;

            CircleSector visionCone = agentEntity.agentsLineOfSight.ConeSight;
            AABox2D targetBox2D = new AABox2D(
                new Vec2f(targetAgentEntity.agentPhysicsState.Position.X, targetAgentEntity.agentPhysicsState.Position.Y) + targetAgentEntity.physicsBox2DCollider.Offset,
                targetAgentEntity.physicsBox2DCollider.Size);

            if (!AABBIntersectSector(ref targetBox2D, visionCone))
                return false;

            // Todo: better way to get body part.
            Vec2f targetUpLeft = targetAgentEntity.agentPhysicsState.Position 
                + new Vec2f(targetAgentEntity.physicsBox2DCollider.Size.X * 0.1f, targetAgentEntity.physicsBox2DCollider.Size.Y * 0.9f);
            Vec2f targetDownLeft = targetAgentEntity.agentPhysicsState.Position + targetAgentEntity.physicsBox2DCollider.Size * 0.1f;
            Vec2f targetMiddle = targetAgentEntity.agentPhysicsState.Position + targetAgentEntity.physicsBox2DCollider.Size * 0.5f;
            Vec2f targetDownRight = targetAgentEntity.agentPhysicsState.Position 
                + new Vec2f(targetAgentEntity.physicsBox2DCollider.Size.X * 0.9f, targetAgentEntity.physicsBox2DCollider.Size.Y * 0.1f);
            Vec2f targetUpRight = targetAgentEntity.agentPhysicsState.Position + targetAgentEntity.physicsBox2DCollider.Size * 0.9f;

            Line2D toUpLeft = new Line2D(agentEyes, targetUpLeft);
            Line2D toLowLeft = new Line2D(agentEyes, targetDownLeft);
            Line2D toMiddle = new Line2D(agentEyes, targetMiddle);
            Line2D toUpRight = new Line2D(agentEyes, targetUpRight);
            Line2D toLowRight = new Line2D(agentEyes, targetDownRight);

            if (visionCone.Intersect(targetDownLeft))
            {
                RayCastResult result = Collisions.RayCastAgainstTileMap(planet.TileMap, toLowLeft);
                if (!toLowLeft.OnLine(result.Point))
                    return true;
            }

            if (visionCone.Intersect(targetUpRight))
            {
                RayCastResult result = Collisions.RayCastAgainstTileMap(planet.TileMap, toUpRight);
                if (!toUpRight.OnLine(result.Point))
                    return true;
            }

            if (visionCone.Intersect(targetMiddle))
            {
                RayCastResult result = Collisions.RayCastAgainstTileMap(planet.TileMap, toMiddle);
                if (!toMiddle.OnLine(result.Point))
                    return true;
            }

            if (visionCone.Intersect(targetDownRight))
            {
                RayCastResult result = Collisions.RayCastAgainstTileMap(planet.TileMap, toLowRight);
                if (!toLowRight.OnLine(result.Point))
                    return true;
            }

            if (visionCone.Intersect(targetUpLeft))
            {
                RayCastResult result = Collisions.RayCastAgainstTileMap(planet.TileMap, toUpLeft);
                if (!toUpLeft.OnLine(result.Point))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// If true it's inside field of view.
        /// https://legends2k.github.io/2d-circleSector.Fov/design.html#references
        /// </summary>
        public static bool AABBIntersectSector(ref AABox2D box, CircleSector circleSector)
        {
            Circle2D circle = new Circle2D()
            { 
                Radius = circleSector.Radius,
                Center = circleSector.StartPos
            };

            // Broad phase check if box overlap with sector. (Same code of AABBOverlapsSphere)
            if (!circle.InterSectionAABB(ref box))
                return false;

            Vec2f[] points = new Vec2f[4];
            points[0] = new Vec2f(box.xmin, box.ymin);  // Botton left
            points[1] = new Vec2f(box.xmin, box.ymax);  // Top left
            points[2] = new Vec2f(box.xmax, box.ymin);  // Botton Right
            points[3] = new Vec2f(box.xmax, box.ymax);  // Top Right



            IsBehindFlag isBehindFlag = new IsBehindFlag();

            Vec2f leftEdge = Vec2f.Rotate(circleSector.Dir, circleSector.Fov / 2);   // Edge counter clock wise rotation
            Vec2f rightEdge = Vec2f.Rotate(circleSector.Dir, -circleSector.Fov / 2);   // Edge  clock wise rotation

            // Update point state.
            for (int i = 0; i < points.Length; i++)
            {
                Vec2f pointDir = points[i] - circleSector.StartPos;
                if (Vec2f.Dot(pointDir, circleSector.Dir) <= 0)
                {
                    isBehindFlag |= (IsBehindFlag)(1 << i);
                    continue;
                }
                if (pointDir.Magnitude > circleSector.Radius)
                    continue;
                
                if (MathF.Sign(Vec2f.Cross(leftEdge, pointDir)) == MathF.Sign(Vec2f.Cross(pointDir, rightEdge)))
                    return true;
            }

            if (!isBehindFlag.HasFlag(IsBehindFlag.BottonLeft) || !isBehindFlag.HasFlag(IsBehindFlag.TopLeft))
            {
                if (EdgeIntersectSector(new Line2D(points[0], points[1]), circleSector.Radius, leftEdge, rightEdge, circleSector.StartPos))
                    return true;
            }
            if (!isBehindFlag.HasFlag(IsBehindFlag.BottonLeft) || !isBehindFlag.HasFlag(IsBehindFlag.BottonRight))
            {
                if (EdgeIntersectSector(new Line2D(points[0], points[2]), circleSector.Radius, leftEdge, rightEdge, circleSector.StartPos))
                    return true;
            }
            if (!isBehindFlag.HasFlag(IsBehindFlag.TopLeft) || !isBehindFlag.HasFlag(IsBehindFlag.TopRight))
            {
                if (EdgeIntersectSector(new Line2D(points[1], points[3]), circleSector.Radius, leftEdge, rightEdge, circleSector.StartPos))
                    return true;
            }
            if (!isBehindFlag.HasFlag(IsBehindFlag.BottonRight) || !isBehindFlag.HasFlag(IsBehindFlag.TopRight))
            {
                if (EdgeIntersectSector(new Line2D(points[2], points[3]), circleSector.Radius, leftEdge, rightEdge, circleSector.StartPos))
                    return true;
            }

            return false;
        }

        [MethodImpl((MethodImplOptions)256)]
        public static bool EdgeIntersectSector(Line2D line, float radius, Vec2f leftEdge, Vec2f rightEdge, Vec2f startPos)
        {
            // https://www.geometrictools.com/Documentation/IntersectionLine2Circle2.pdf
            // Sector Arc Intersection
            Vec2f delta = line.A - startPos;
            Vec2f diff = line.B - line.A;
            float dLen = delta.Magnitude;
            float diffLen = diff.Magnitude;

            float t1 = Vec2f.Dot(line.B, delta);
            float t2 = t1 * t1 - (diffLen * diffLen * (dLen * dLen - radius * radius));

            float c1 = -1, c2 = -1;
            if (t2 >= 0)
            {
                c1 = (-t1 + MathF.Sqrt(t2))/ (diffLen * diffLen);
            }
            if (t1 > 0)
            {
                c2 = (-t1 - MathF.Sqrt(t2)) / (diffLen * diffLen);
            }

            // If intersects with circle
            if (c1 >= 0 && c1 <= 1)
            {
                // Check if intersection point is inside arc.
                Vec2f intersectPoint = line.A + c1 * diff;
                Vec2f pointDir = intersectPoint - startPos;
                if (MathF.Sign(Vec2f.Cross(leftEdge, pointDir)) == MathF.Sign(Vec2f.Cross(pointDir, rightEdge)))
                   return true;
            }

            if (c2 >= 0 && c2 <= 1)
            {
                // Check if intersection point is inside arc.
                Vec2f intersectPoint = line.A + c2 * diff;
                Vec2f pointDir = intersectPoint - startPos;
                if (MathF.Sign(Vec2f.Cross(leftEdge, pointDir)) == MathF.Sign(Vec2f.Cross(pointDir, rightEdge)))
                   return true;
            }

            // Sector Edge Intersection
            leftEdge *= radius;
            rightEdge *= radius;

            if (line.Intersects(new Line2D(startPos, startPos + leftEdge)))
                return true;
            if (line.Intersects(new Line2D(startPos, startPos + rightEdge)))
                return true;

            return false;
        }
    }
}
