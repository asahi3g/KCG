using System;
using UnityEngine.SocialPlatforms;

namespace Projectile
{
    public class ProcessState
    {
        public void Update(ref Planet.PlanetState planet)
        {
            for (int i = 0; i < planet.ProjectileList.Length; i++)
            {
                ProjectileEntity entityP = planet.ProjectileList.Get(i);

                if (entityP.hasProjectileRange)
                {
                    if ((entityP.projectilePhysicsState.Position - entityP.projectileStart.StarPos).Magnitude > entityP.projectileRange.Range)
                        entityP.isProjectileDelete = true;
                }
            }
        }
    }
}
