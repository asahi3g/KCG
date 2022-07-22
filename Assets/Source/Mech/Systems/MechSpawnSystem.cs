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
        
        public MechEntity SpawnMech(Contexts entitasContext, MechType mechType)
        {
            MechProperties mechProperties = MechCreationApi.Get((int)mechType);

            var entity = entitasContext.mech.CreateEntity();

            return entity;
        }
        
    }
}