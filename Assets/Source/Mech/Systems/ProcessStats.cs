namespace Mech
{
    public class ProcessStats
    {
        public void Update()
        {
            ref var planet = ref GameState.Planet;
            ref MechList mechList = ref planet.MechList;

            for (int i = 0; i < mechList.Length; i++)
            {
                MechEntity mech = mechList.Get(i);

                if (mech.mechDurability.Durability <= 0)
                {
                    // Todo: Trigger animation of mech destruction
                    planet.RemoveMech(mech.mechID.Index);
                }
            }
        }
    }
}
