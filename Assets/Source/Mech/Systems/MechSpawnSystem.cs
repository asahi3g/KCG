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
        
        public MechEntity Spawn(Contexts entitasContext, int spriteId, int width, int height, Vec2f position,
            int mechID, MechType mechType)
        {
            var spriteSize = new Vec2f(width / 32f, height / 32f);

            ref MechProperties mechProperties = ref MechCreationApi.GetRef((int)mechType);

            var entity = entitasContext.mech.CreateEntity();
            entity.AddMechID(mechID);
            entity.AddMechSprite2D(spriteId, spriteSize);
            entity.AddMechPositionLimits(mechProperties.XMin, mechProperties.XMax, mechProperties.YMin, mechProperties.YMax);
            entity.AddMechPosition2D(position);
            entity.AddMechType(mechType);

            if (mechType == MechType.Planter)
                entity.AddMechPlanterPlanter(false, PlantType.Lily, 0.0f, 100.0f, 0.0f, 100.0f, 0);

            return entity;
        }

        public MechEntity Spawn(Contexts entitasContext, Vec2f position, int mechID, MechType mechType)
        {
            ref MechProperties mechProperties = ref MechCreationApi.GetRef((int)mechType);

            var spriteSize = mechProperties.SpriteSize;

            var spriteId = mechProperties.SpriteID;

            var entity = entitasContext.mech.CreateEntity();
            entity.AddMechID(mechID);
            entity.AddMechSprite2D(spriteId, spriteSize);
            entity.AddMechPositionLimits(mechProperties.XMin, mechProperties.XMax, mechProperties.YMin, mechProperties.YMax);
            entity.AddMechPosition2D(position);
            entity.AddMechType(mechType);

            if (mechType == MechType.Planter)
                entity.AddMechPlanterPlanter(false, PlantType.Lily, 0.0f, 100.0f, 0.0f, 100.0f, 0);

            return entity;
        }

    }
}