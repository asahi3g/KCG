using System;
using UnityEngine;


namespace Projectile
{
    public class DebugSystem
    {
        public void Update(ref Planet.PlanetState planet)
        {
            if (planet.ProjectileList == null)
                return;
                
    #if UNITY_EDITOR
                for (int i = 0; i < planet.ProjectileList.Length; i++)
                {
                    ProjectileEntity entityP = planet.ProjectileList.Get(i);
                    Vector3 startPos = new Vector3(entityP.projectileStart.StarPos.X, entityP.projectileStart.StarPos.Y, 0);
                    startPos.x += entityP.physicsBox2DCollider.Size.X / 2.0f;
                    startPos.y += entityP.physicsBox2DCollider.Size.Y / 2.0f;
                    Vector3 endPos = new Vector3(entityP.projectilePhysicsState.Position.X, entityP.projectilePhysicsState.Position.Y, 0);
                    endPos.x += entityP.physicsBox2DCollider.Size.X / 2.0f;
                    endPos.y += entityP.physicsBox2DCollider.Size.Y / 2.0f;
                    Debug.DrawLine(startPos, endPos, Color.red, 2.0f, false);
                }
                #endif
        }
    }
#endif
}
