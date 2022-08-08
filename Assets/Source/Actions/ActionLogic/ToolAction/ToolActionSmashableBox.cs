using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action
{
    public class ToolActionSmashableBox : ActionBase
    {
        public ToolActionSmashableBox(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }
    }
    public class ToolActionSmashableBoxCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            // Creation Action Tool
            return new ToolActionSmashableBox(entitasContext, actionID);
        }
    }
}
