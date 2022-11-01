//imports UnityEngine

namespace Projectile
{
    public class DebugSystem
    {
        public void Update()
        {
            ref var planet = ref GameState.Planet;
            if (planet.ProjectileList == null)
                return;
                
    #if UNITY_EDITOR
                for (int i = 0; i < planet.ProjectileList.Length; i++)
                {
                    ProjectileEntity entityP = planet.ProjectileList.Get(i);
                UnityEngine.Vector3 startPos = new UnityEngine.Vector3(entityP.projectileStart.StarPos.X, entityP.projectileStart.StarPos.Y, 0);
                    startPos.x += entityP.physicsBox2DCollider.Size.X / 2.0f;
                    startPos.y += entityP.physicsBox2DCollider.Size.Y / 2.0f;
                UnityEngine.Vector3 endPos = new UnityEngine.Vector3(entityP.projectilePhysicsState.Position.X, entityP.projectilePhysicsState.Position.Y, 0);
                    endPos.x += entityP.physicsBox2DCollider.Size.X / 2.0f;
                    endPos.y += entityP.physicsBox2DCollider.Size.Y / 2.0f;
                UnityEngine.Debug.DrawLine(startPos, endPos, UnityEngine.Color.red, 2.0f, false);
                }
    #endif
        }
    }
}
