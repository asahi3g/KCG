namespace AI.SquadBehvaior
{
    public class SquadManager
    {
        Squad[] Squads;
        string[] Names;
        public int Length;

        public SquadManager()
        {
            Length = 0;
            Squads = new Squad[64];
            Names = new string[64];
            Create("Default Squad"); // WIll be removed latter. Used for testing.
        }

        public int Create(string name)
        {
            ref Squad squad = ref Squads[Length];
            Names[Length] = name;
            squad.BlackboardID = GameState.BlackboardManager.CreateSquadBlackboard();
            squad.PositionsLength = 256;
            squad.PositionsScore = new int[squad.PositionsLength];
            squad.CombatPositions = new KMath.Vec2f[squad.PositionsLength];
            return Length++;
        }

        public ref Squad Get(int id)
        {
            return ref Squads[id];
        }

        public string GetName(int id)
        {
            return Names[id];
        }
    }
}
