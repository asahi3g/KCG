using System.Collections.Generic;
using Entitas;
using KMath;
using UnityEngine;

namespace Agent
{
    public class AgentSpawnerSystem
    {
        public GameEntity SpawnPlayer(Material material, int spriteId, int width, int height, Vec2f position,
        int AgentId, int startingAnimation)
        {
            var entity = Contexts.sharedInstance.game.CreateEntity();

            var spriteSize = new Vec2f(width / 32f, height / 32f);

            entity.isAgentPlayer = true;
            entity.isECSInput = true;
            entity.AddECSInputXY(new Vec2f(0, 0), false);

            entity.AddAgentID(AgentId);
            entity.AddAnimationState(1.0f, new Animation.Animation{Type=startingAnimation});
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
            entity.AddPhysicsPosition2D(position, newPreviousValue: default);
            var size = new Vec2f(spriteSize.X - 0.5f, spriteSize.Y);
            entity.AddPhysicsBox2DCollider(size, new Vec2f(0.25f, .0f));
            entity.AddPhysicsMovable(newSpeed: 1f, newVelocity: Vec2f.Zero, newAcceleration: Vec2f.Zero);
            
            // Add Inventory and toolbar.
            var attacher = Inventory.InventoryAttacher.Instance;
            attacher.AttachInventoryToAgent(6, 5, AgentId);
            attacher.AttachToolBarToPlayer(10, AgentId);

            return entity;
        }

        public GameEntity SpawnAgent(Material material, int spriteId, int width, int height, Vec2f position,
        int AgentId, int startingAnimation)
        {
            var entity = Contexts.sharedInstance.game.CreateEntity();

            var spriteSize = new Vec2f(width / 32f, height / 32f);

            entity.AddAgentID(AgentId);

            Vec2f box2dCollider = new Vec2f(0.5f, 1.5f);
            entity.AddPhysicsBox2DCollider(box2dCollider, new Vec2f(0.25f, 0.0f));
            entity.AddAnimationState(1.0f, new Animation.Animation{Type=startingAnimation});
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
            entity.AddPhysicsPosition2D(position, newPreviousValue: default);
            entity.AddPhysicsMovable(newSpeed: 1f, newVelocity: Vec2f.Zero, newAcceleration: Vec2f.Zero);

            return entity;
        }

        public GameEntity SpawnEnemy(Material material, int spriteId, int width, int height, Vec2f position,
        int AgentId, int startingAnimation)
        {
            var entity = Contexts.sharedInstance.game.CreateEntity();
            
            var spriteSize = new Vec2f(width / 32f, height / 32f);
            
            entity.AddAgentID(AgentId);

            Vec2f box2dCollider = new Vec2f(0.75f, 0.5f);
            entity.AddPhysicsBox2DCollider(box2dCollider, new Vec2f(0.125f, 0.0f));
            entity.AddAnimationState(1.0f, new Animation.Animation{Type=startingAnimation});
            entity.AddAgentSprite2D(spriteId, spriteSize); // adds the sprite  component to the entity
            entity.AddPhysicsPosition2D(position, newPreviousValue: default);
            entity.AddPhysicsMovable(newSpeed: 1f, newVelocity: Vec2f.Zero, newAcceleration: Vec2f.Zero);
            entity.AddAgentEnemy(0, 4.0f);
            entity.AddAgentStats(100.0f, 0.8f);

            return entity;
        }

    }
}
