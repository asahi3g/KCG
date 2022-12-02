using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Agent
{

    [Agent]
    public class IDComponent : IComponent
    {
        [PrimaryEntityIndex]
        // This is not the index of AgentList. It should never reuse values. It should never be changed.
        // Todo use one id instead of two: https://news.ycombinator.com/item?id=17995634
        public int ID;
        public int Index;
        public Enums.AgentType Type;
        public AgentFaction Faction;
        public int SquadID; // If -1 it's not part of any squad.
    }
}