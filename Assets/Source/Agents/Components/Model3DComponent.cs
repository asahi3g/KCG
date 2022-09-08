using Entitas;
using UnityEngine;
using Animancer;

namespace Agent
{
    [Agent]
    public class Model3DComponent : IComponent
    {
        public GameObject GameObject;
        public GameObject LeftHand;
        public GameObject RightHand;
        public Model3DWeapon CurrentWeapon;
        public GameObject Weapon;
        public AnimancerComponent AnimancerComponent;
        public Enums.AgentAnimationType AnimationType;
    }

        
}
