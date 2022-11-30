using UnityEngine;

public class AppUI : BaseMonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RectTransform _views;

    public Camera GetCamera() => _camera;

    public T GetView<T>() where T : UIView
    {
        return _views.GetComponentInChildren<T>();
    }
}
