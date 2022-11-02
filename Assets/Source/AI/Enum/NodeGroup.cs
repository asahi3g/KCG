namespace Enums
{
    public enum NodeGroup
    {
        // Nodes with multiple children.
        CompositeNode,
        // Nodes with single child.
        DecoratorNode,
        // Action AI can use.
        ActionNode,
        // Actions available only for the player. Ex: "Creative" mode tool actions
        PlayerAction,
    }
}
