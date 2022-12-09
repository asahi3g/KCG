namespace AI.SquadBehvaior
{
    public class SquadUpdateSystem
    {
        public void Update()
        {
            for (int id = 0; id < GameState.SquadManager.Length; id++)
            {
                GameState.MovementPositionScoreSystem.Update(id);
            }
        }
    }
}
