namespace AI.SquadBehvaior
{
    public class SquadManager
    {
        Squad[] Squads;
        string[] Names;
        public int Length;
        public int MAX_POSITION = 256;
        public int MAX_AGENT_COUNT = 32;

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
            squad.PositionsLength = 0;
            squad.PositionsScore = new int[MAX_POSITION];
            squad.CombatPositions = new KMath.Vec2f[MAX_POSITION];
            squad.MemberIDs = new int[MAX_AGENT_COUNT];
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
