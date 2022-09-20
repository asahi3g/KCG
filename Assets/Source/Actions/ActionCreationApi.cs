using System;
using Agent;
using Enums;

namespace Action
{
    public class ActionCreationApi
    {
        private ActionType CurrentIndex;
        private ActionProperties[] PropertiesArray;

        public ActionCreationApi()
        {
            CurrentIndex = ActionType.None;
            PropertiesArray = new ActionProperties[Enum.GetNames(typeof(ActionType)).Length];
        }

        public void CreateActionPropertyType(ActionType actionType, string name)
        {
            CurrentIndex = actionType;
            PropertiesArray[(int)CurrentIndex].TypeID = actionType;
            PropertiesArray[(int)CurrentIndex].Name = name;
        }

        public void CreateActionPropertyType(ActionType actionType)
        {
            CurrentIndex = actionType;
            PropertiesArray[(int)CurrentIndex].TypeID = actionType;
            PropertiesArray[(int)CurrentIndex].Name = actionType.ToString();
        }

        public ActionProperties Get(ActionType actionType)
        {
            return PropertiesArray[(int)actionType];
        }

        public void SetDescription(string str)
        {
            PropertiesArray[(int)CurrentIndex].Descripition = str;
        }

        public void SetLogicFactory(ActionCreator actionFactory)
        {
            PropertiesArray[(int)CurrentIndex].ActionFactory = actionFactory;
        }

        public void SetTime(float duration)
        {
            PropertiesArray[(int)CurrentIndex].Duration = duration;
        }

        public void SetCoolDown(float coolDown)
        {
            PropertiesArray[(int)CurrentIndex].CoolDownTime = coolDown;
        }

        public void SetData(object data)
        {
            PropertiesArray[(int)CurrentIndex].ObjectData = data;
        }


        /// <summary>
        /// Todo: Data should be in component instead of attributes. This should be an empty component.
        /// ActionProperty that are or causes movement. Exemple: take cover.
        /// </summary>
        /*public void Movement()
        {
            ActionPropertyEntity.isActionPropertyMovement = true;
        }

        public void SetGoap(AI.GoapState preCondition, AI.GoapState effects, int cost)
        {
            ActionPropertyEntity.AddActionPropertyGoap(preCondition, effects, cost);
        }
        */

        public void EndActionPropertyType()
        {
            CurrentIndex = ActionType.None;
        }
    }
}
