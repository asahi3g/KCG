namespace Projectile
{
    public class DeleteSystem
    {
        // Remove projectile only at the end of the frame.

        /// <summary> Should be called at the end of the frame.</summary>
<<<<<<< HEAD
        public void Update(Planet.PlanetState planet)
=======
        public void Update()
>>>>>>> 3b95f36247fe313ba5f5f7bfd4f38797fb5b6059
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