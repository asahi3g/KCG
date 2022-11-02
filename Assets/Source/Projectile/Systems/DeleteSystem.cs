namespace Projectile
{
    public class DeleteSystem
    {
        // Remove projectile at the end of the frame.
        // Should be called at the end of the frame.
        public void Update()
        {
            ref var planet = ref GameState.Planet;
            for (int i = 0; i < planet.ProjectileList.Length; i++)
            {
                ProjectileEntity entityP = planet.ProjectileList.Get(i);

                if (entityP.isProjectileDelete)
                {
                    planet.RemoveProjectile(entityP.projectileID.Index);
                }
            }
        }
    }
}