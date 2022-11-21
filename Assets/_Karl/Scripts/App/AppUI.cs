using UnityEngine;

public class AppUI : BaseMonoBehaviour
{
    [SerializeField] private RectTransform _views;


    public T GetView<T>() where T : UIView
    {
        return _views.GetComponentInChildren<T>();
    }
}
