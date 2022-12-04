using AI;
using System.Collections.Generic;

public partial class NodeEntity
{
    public ItemInventoryEntity GetItem()
    {
        AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeOwner.AgentID);
        return agentEntity.GetItem();
    }

    public bool AddChild(int id)
    {
        NodeSystem.Node node = GameState.NodeManager.Get(id);
        if (nodeID.ID == id)
        {
            UnityEngine.Debug.LogError("Can't Add node to itself.");
            return false;
        }

        if (hasNodeComposite)
        { 
            if (!nodeComposite.Children.Contains(id))
            {
                nodeComposite.Children.Add(id);
                return true;
            }
            return false;
        }
        else
        {
            nodeDecorator.ChildID = id;
            return true;
        }
    }

    public List<NodeEntity> GetChildren(NodeContext context)
    { 
        List<NodeEntity> children = new List<NodeEntity>();

        if (hasNodeComposite)
        {
            foreach (int id in nodeComposite.Children)
            {
                children.Add(context.GetEntityWithNodeIDID(id));
            }
        }
        if (hasNodeDecorator)
        {
            if (nodeDecorator.ChildID != -1)
                children.Add(context.GetEntityWithNodeIDID(nodeDecorator.ChildID));
        }

        return children;
    }
}

