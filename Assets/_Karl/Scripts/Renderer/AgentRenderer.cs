﻿using Animancer;
using Engine3D;
using Inventory;
using UnityEngine;
using UnityEngine.Events;

public class AgentRenderer : BaseMonoBehaviour
{
    [SerializeField] private AgentModelType _modelType;
    [SerializeField] private AnimancerComponent _animancer;
    [Space]
    [SerializeField] private Transform _rotation;
    [SerializeField] private Transform _scale;
    [SerializeField] private GameObject _model;
    [SerializeField] private Transform _pivotHead;
    [SerializeField] private Transform _handLeft;
    [SerializeField] private Transform _handRight;
    [SerializeField] private Transform _pivotPistol;
    [SerializeField] private Transform _pivotRifle;
    [SerializeField] private Transform _aimTarget;

    private PlanetRenderer _planetRenderer;
    private AgentEntity _agent;
    private UIHitpoints _hitpoints;

    public class Event : UnityEvent<AgentRenderer> { }

    public AgentModelType GetModelType() => _modelType;
    public AnimancerComponent GetAnimancer() => _animancer;
    public AgentEntity GetAgent() => _agent;
    public GameObject GetModel() => _model;
    public Transform GetPivotHead() => _pivotHead;
    public Transform GetHandLeft() => _handLeft;
    public Transform GetHandRight() => _handRight;
    public Transform GetPivotPistol() => _pivotPistol;
    public Transform GetPivotRifle() => _pivotRifle;
    public Transform GetAimTarget() => _aimTarget;
    

    public void SetAgent(PlanetRenderer planetRenderer, AgentEntity agent)
    {
        _planetRenderer = planetRenderer;
        _agent = agent;
        if (_agent.hasAgent3DModel)
        {
            _agent.Agent3DModel.Renderer.transform.parent = transform;
        }

        // Create UI hitpoints
        if (_agent.hasAgentStats)
        {
            _hitpoints = Instantiate(AssetManager.Singelton.GetPrefabUIHitpoints(), _pivotHead, false);
            _hitpoints.SetHitpoints(_agent.agentStats.Health);

            bool factionFriendly = false;
            if(!agent.isAgentPlayer){
                if (App.Instance.GetPlayer().GetCurrentPlayerAgent(out AgentRenderer agentRenderer))
                {
                    factionFriendly = agent.agentID.Faction == agentRenderer.GetAgent().agentID.Faction;
                }
            }
            _hitpoints.SetFactionFriendly(factionFriendly);
        }
    }

    public bool GetInventory(out InventoryEntityComponent inventoryEntityComponent)
    {
        inventoryEntityComponent = null;
        if (_agent != null)
        {
            if (_agent.hasAgentInventory)
            {
                Agent.InventoryComponent inventoryComponent = _agent.agentInventory;
                inventoryEntityComponent = GameState.Planet.GetInventoryEntityComponent(inventoryComponent.InventoryID);
            }
        }
        return inventoryEntityComponent != null;
    }



    public void SetIsActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        _rotation.rotation = rotation;
    }

    public void SetLocalScale(Vector3 localScale)
    {
        _scale.localScale = localScale;
    }
}
