using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KMath;

public class ConcussionGrenadeValues : MonoBehaviour
{

    [SerializeField] int Number_Ticks = 1;
    [SerializeField] float Magnitude = 20.0f;
    [SerializeField] float Radius = 5.0f;
    [SerializeField] int Damage = 20;
    [SerializeField] float GrenadeLaunchVelocity = 20.0f;
   // [SerializeField] float GrenadeAirResistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameState.ProjectileCreationApi.Create((int)Enums.ProjectileType.ConcussionGrenade);
        GameState.ProjectileCreationApi.SetNumberOfTicks(Number_Ticks);
        GameState.ProjectileCreationApi.SetBlastMagnitude(Magnitude);
        GameState.ProjectileCreationApi.SetSpriteId(GameState.ProjectileCreationApi.PlaceholderSprite);
        GameState.ProjectileCreationApi.SetDeltaRotation(180.0f);
        GameState.ProjectileCreationApi.SetSize(new Vec2f(0.75f, 0.75f));
        GameState.ProjectileCreationApi.SetStartVelocity(GrenadeLaunchVelocity);
        GameState.ProjectileCreationApi.SetAffectedByGravity();
        GameState.ProjectileCreationApi.End();


        GameState.ItemCreationApi.CreateItem(Enums.ItemType.ConcussionGrenade, "ConcussionGrenade");
        GameState.ItemCreationApi.SetGroup(Enums.ItemGroups.None);
        GameState.ItemCreationApi.SetTexture(GameState.ItemCreationApi.GrenadeSpriteId);
        GameState.ItemCreationApi.SetInventoryItemIcon(GameState.ItemCreationApi.GrenadeSpriteId);
        GameState.ItemCreationApi.SetExplosion(Radius, Damage, 0.0f);
        GameState.ItemCreationApi.SetAction(Enums.ActionType.ThrowConcussionGrenadeAction);
        GameState.ItemCreationApi.EndItem();


    }


    void OnDrawGizmos()
    {
        for(int i = 0; i < GameState.Planet.ProjectileList.Length; i++)
        {
            ProjectileEntity entity = GameState.Planet.ProjectileList.Get(i);

            if (entity.projectileType.Type == Enums.ProjectileType.ConcussionGrenade)
            {
                Gizmos.color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.2f);
                Gizmos.DrawSphere(new UnityEngine.Vector3(entity.projectilePhysicsState.Position.X, entity.projectilePhysicsState.Position.Y, 0.0f),
                Radius);
            }
        }
    }
}
