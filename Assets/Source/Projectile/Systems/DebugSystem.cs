using System;
using UnityEngine;


namespace Projectile
{
    public class DebugSystem
    {
        public void Update(ref Planet.PlanetState planet)
        {
#if UNITY_EDITOR
            for (int i = 0; i < planet.ProjectileList.Length; i++)
            {
                ProjectileEntity entityP = planet.ProjectileList.Get(i);

                Debug.DrawLine(new Vector3(entityP.projectileStart.StarPos.X, entityP.projectileStart.StarPos.Y, 0), 
                    new Vector3(entityP.projectilePhysicsState.Position.X,
                    entityP.projectilePhysicsState.Position.Y, 0), Color.red, 2.0f, false);
            }
#endif
        }
    }
}
