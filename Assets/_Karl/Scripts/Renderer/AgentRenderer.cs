
using Inventory;
using UnityEngine.Events;

public class AgentRenderer : BaseMonoBehaviour
{
    private AgentEntity _agent;

    public class Event : UnityEvent<AgentRenderer> { }

    public AgentEntity GetAgent() => _agent;


    public void SetAgent(AgentEntity agent)
    {
        _agent = agent;
        if (_agent.hasAgentModel3D)
        {
            _agent.agentModel3D.GameObject.transform.parent = transform;
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
}
