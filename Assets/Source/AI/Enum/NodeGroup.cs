namespace Enums
{
    public enum NodeGroup
    {
        /// <summary> Nodes with multiple children.</summary>
        CompositeNode,
        /// <summary> Nodes with single child.</summary>
        DecoratorNode,
        /// <summary> Action AI can use.</summary>
        ActionNode,
        /// <summary> Actions available only for the player. Ex: "Creative" mode tool actions</summary>
        PlayerAction,
    }
}
