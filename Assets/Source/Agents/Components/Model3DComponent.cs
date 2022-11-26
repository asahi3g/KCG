//import UnityEngine

using Entitas;
using Animancer;

namespace Agent
{
    [Agent]
    public class Model3DComponent : IComponent
    {
        public UnityEngine.GameObject GameObject;
        public UnityEngine.GameObject LeftHand;
        public UnityEngine.GameObject RightHand;
        public Model3DWeaponType CurrentWeapon;
        public UnityEngine.GameObject Weapon;
        public AnimancerComponent AnimancerComponent;
        public Enums.AgentAnimationType AnimationType;
        public Enums.ItemAnimationSet ItemAnimationSet;
        public KMath.Vec3f ModelScale;
        public KMath.Vec2f AimTarget;
    }

        
}
