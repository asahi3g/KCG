using System;

namespace Physics
{
    static class PhysicsFormulas
    {
        public static float GetSpeedToJump(float height) => MathF.Sqrt(height * Constants.Gravity * 2);
        public static float JumpHeightFromVelocity(float initialVelocity) => initialVelocity * initialVelocity / (Physics.Constants.Gravity * 2);
    }
}
