using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIMonoBehaviour : BaseMonoBehaviour
{
    private RectTransform _rectTransform;

    public RectTransform rectTransform {
        get {
            if(_rectTransform is null) _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
    }
}
