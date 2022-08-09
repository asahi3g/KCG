using Entitas;
using UnityEngine;
using Animancer;

namespace Agent
{
    [Agent]
    public class Model3DComponent : IComponent
    {
        public GameObject GameObject;
        public GameObject Hand;
        public Model3DWeapon CurrentWeapon;
        public GameObject Weapon;
        public AnimancerComponent AnimancerComponent;
    }

        
}
