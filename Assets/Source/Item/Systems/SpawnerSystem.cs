﻿using UnityEngine;
using System.Collections.Generic;

using Enums;

namespace Item
{
    public class SpawnerSystem
    {
        public Contexts EntitasContext;

        private static int ItemID;

        public SpawnerSystem(Contexts entitasContext)
        {
            EntitasContext = entitasContext;
        }

        public GameEntity SpawnItem(ItemType itemType, Vector2 position)
        {
            var entityAttribute = EntitasContext.game.GetEntityWithItemAttributes(itemType);
            Vector2 size = entityAttribute.itemAttributeSize.Size;

            var entity = EntitasContext.game.CreateEntity();
            entity.AddItemID(ItemID, itemType);
            entity.AddPhysicsPosition2D(position, position);
            entity.AddPhysicsBox2DCollider(size, Vector2.zero);
            entity.AddPhysicsMovable(0f, Vector2.zero, Vector2.zero, 0f);

            ItemID++;
            return entity;
        }

        public GameEntity SpawnIventoryItem(ItemType itemType)
        {
            var entity = EntitasContext.game.CreateEntity();
            entity.AddItemID(ItemID, itemType);

            ItemID++;
            return entity;
        }
    }
}
