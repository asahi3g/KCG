using System.Collections.Generic;

namespace NodeSystem
{
    public class ConditionManager
    {
        public delegate bool Condition(object ptr);
        public Condition[] Conditions = new Condition[1024];
        public string[] Names = new string[1024];

        private Dictionary<string, int> NameIDPairs = new Dictionary<string, int>();
        public const int TrueConditionID = 0; // Id of default condition.
        private bool TrueCondtion(object ptr) => true;

        private int Length = 1;

        public ConditionManager()
        {
            RegisterCondition("TrueCondition", TrueCondtion);
        }

        public Condition Get(int id)
        {
            return Conditions[id];
        }

        public Condition Get(string name)
        {
            return Get(NameIDPairs[name]);
        }

        public int GetID(string name)
        {
            if (NameIDPairs.ContainsKey(name))
            {
                return NameIDPairs[name];
            }
            else
            {
                return 0;
            }
            
        }

        // Register conditional function
        public int RegisterCondition(string name, Condition condition)
        {
            if (NameIDPairs.ContainsKey(name))
            {
                Conditions[Length] = condition;
                Names[Length] = name;
                NameIDPairs.Add(name, Length);
                return Length++;
            }
            else
            {
                return Length;
            }
        }
    }
}
