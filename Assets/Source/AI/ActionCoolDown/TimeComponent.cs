using Entitas;

namespace ActionCoolDown
{
    // This should exist only while action is in cooldown.
    [ActionCoolDown]
    public struct TimeComponent : IComponent
    {
        /// <summary> CoolDown End Time. </summary>
        public float EndTime;
    }
}
