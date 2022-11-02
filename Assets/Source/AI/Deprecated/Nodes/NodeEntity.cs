using AI;
using Node;
using System.Collections.Generic;

public partial class NodeEntity
{
    public ItemInventoryEntity GetItem()
    {
        AgentEntity agentEntity = GameState.Planet.EntitasContext.agent.GetEntityWithAgentID(nodeOwner.AgentID);
        return agentEntity.GetItem();
    }

    public bool AddChild(NodeEntity nodeEntity) => AddChild(nodeEntity.nodeID.ID);

    public bool AddChild(int ID)
    {
        if (nodeID.ID == ID)
        {
            UnityEngine.Debug.LogError("Can't Add node to itself.");
            return false;
        }

        if (AISystemState.Nodes[(int)nodeID.TypeID].NodeGroup == Enums.NodeGroup.ActionNode)
        {
            UnityEngine.Debug.LogError("Can't Add child to action node.");
            return false;
        }

        if (hasNodeComposite)
        { 
            if (!nodeComposite.Children.Contains(ID))
            {
                nodeComposite.Children.Add(ID);
                return true;
            }
            return false;
        }
        else
        {
            nodeDecorator.ChildID = ID;
            return true;
        }
    }

    public bool RemoveChild(int ID)
    {
        if (hasNodeComposite)
        {
            nodeComposite.Children.Remove(ID);
            return true;
        }

        UnityEngine.Debug.LogError("Can only remove child from composite nodes.");
        return false;
    }

    public bool RemoveAllChildren()
    {
        if (hasNodeComposite)
        {
            nodeComposite.Children.Clear();
            return true;
        }

        if (hasNodeDecorator)
        {
            nodeDecorator.ChildID = -1;
            return true;
        }

        UnityEngine.Debug.LogError("Action node doesn't have any children.");
        return false;
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

    public NodeBase GetNodeSystem()
    {
        return AISystemState.Nodes[(int)nodeID.TypeID];
    }
}

