using Engine3D;
using UnityEngine;

public class AgentEquippedItemRenderer : BaseMonoBehaviour
{
    [SerializeField] private ItemModelType _modelType;

    public ItemModelType GetModelType() => _modelType;
}
