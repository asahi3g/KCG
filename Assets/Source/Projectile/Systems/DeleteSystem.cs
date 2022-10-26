namespace Projectile
{
    public class DeleteSystem
    {
        // Remove projectile only at the end of the frame.

        /// <summary> Should be called at the end of the frame.</summary>
        public void Update()
        {
            for (int i = 0; i < GameState.Planet.ProjectileList.Length; i++)
            {
                ProjectileEntity entityP = GameState.Planet.ProjectileList.Get(i);

                if (entityP.isProjectileDelete)
                {
                    GameState.Planet.RemoveProjectile(entityP.projectileID.Index);
                }
            }
        }
    }
}