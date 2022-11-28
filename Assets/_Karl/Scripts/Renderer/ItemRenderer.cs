using Engine3D;
using UnityEngine;

public class ItemRenderer : BaseMonoBehaviour
{
    [SerializeField] private ItemModelType _modelType;

    public ItemModelType GetModelType() => _modelType;
}
