using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using KMath;
using UnityEngine.UIElements;

namespace Mech
{
    public class MechSpawnSystem
    {
        MechCreationApi MechCreationApi;

        public MechSpawnSystem(MechCreationApi mechCreationApi)
        {
            MechCreationApi = mechCreationApi;
        }
        
        public MechEntity Spawn(Contexts entitasContext, int mechID)
        {
            MechProperties mechProperties = MechCreationApi.Get(mechID);

            var entity = entitasContext.mech.CreateEntity();
            entity.AddMechPositionLimits(mechProperties.XMin, mechProperties.XMax, mechProperties.YMin, mechProperties.YMax);

            return entity;
        }
        
    }
}