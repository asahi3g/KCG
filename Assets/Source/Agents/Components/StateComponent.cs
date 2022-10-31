using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Agent
{
    [Agent, FlagPrefix("Is")]
    public class AliveComponent : IComponent
    {
    }
}