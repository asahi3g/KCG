using System;
using KMath;
using Planet;
using UnityEngine;
using System.Collections.Generic;
using Agent;
using Enums;

namespace Action
{
    public class ToolActionUsePotion : ActionBase
    {
        
        public ToolActionUsePotion(Contexts entitasContext, int actionID) : base(entitasContext, actionID)
        {
        }

        public override void OnEnter(ref Planet.PlanetState planet)
        {
            var player = planet.Player;
            player.UsePotion(2.0f);

            ActionEntity.actionExecution.State = Enums.ActionState.Running;
        }

        public override void OnUpdate(float deltaTime, ref Planet.PlanetState planet)
        {
            ActionEntity.actionExecution.State = Enums.ActionState.Success;
        }

        public override void OnExit(ref PlanetState planet)
        {
            base.OnExit(ref planet);
        }
    }

    /// <summary>
    /// Factory Method
    /// </summary>
    public class ToolActionUsePotionCreator : ActionCreator
    {
        public override ActionBase CreateAction(Contexts entitasContext, int actionID)
        {
            return new ToolActionUsePotion(entitasContext, actionID);
        }
    }
}

