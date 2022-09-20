namespace Action
{
    public struct ActionProperties
    {
        public Enums.ActionType TypeID;
        public string Name;
        public string Descripition;

        public float CoolDownTime;
        
        /// <summary>How long it takes to execute the action in miliseconds </summary>
        public float Duration;
        public ActionCreator ActionFactory;

        public object ObjectData;

        // Goap
        //public AI.GoapState PreConditions;
        //public AI.GoapState Effects;
        //public int Cost;
    }
}
