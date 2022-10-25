using System.Collections.Generic;

namespace NodeSystem
{
    public class ConditionManager
    {
        public delegate bool Condition(object ptr);
        public Condition[] Conditions = new Condition[1024];
        public string[] Names = new string[1024];

        private Dictionary<string, int> NameIDPairs = new Dictionary<string, int>();
        private bool DefaultCondition(object ptr) => true;
        private int Length = 1;
        
        public ConditionManager()
        {
            RegisterCondition("Default", DefaultCondition);
        }

        public Condition Get(int id)
        {
            return Conditions[id];
        }

        public Condition Get(string name)
        {
            return Get(NameIDPairs[name]);
        }

        public int GetID(string name) => NameIDPairs[name];

        // Register conditional function
        public int RegisterCondition(string name, Condition condition)
        {
            Conditions[Length] = condition;
            Names[Length] = name;
            NameIDPairs.Add(name, Length);
            return Length++;
        }
    }
}
