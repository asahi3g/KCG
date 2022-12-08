using Entitas;
using UnityEditor;

namespace Agent
{
    [Agent]
    public class StaggerComponent : IComponent
    {
        public bool Stagger;
        public float StaggerAffectTime;
        public float elapsed;

        public bool ImpactEffect;
        public float ImpactAffectTime;
    }
}