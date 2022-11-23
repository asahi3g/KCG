using UnityEngine;

public class UIContent : UIMonoBehaviour
{
    [SerializeField] private UIContentElement _prefab;
    [SerializeField] private UIContentSelection _selection;

    public UIContentElement GetPrefab() => _prefab; 

    public void Clear()
    {
        transform.DestroyChildren();
    }

    public T Create<T>() where T : UIContentElement
    {
        T t = (T) Instantiate(_prefab, rectTransform);
        t.SetContent(this);
        return t;
    }
    
    public T[] GetElements<T>(bool includeInactive) where T : UIContentElement {
        return rectTransform.GetComponentsInChildren<T>(includeInactive);
    }
    
    public void Select(UIContentElement value)
    {
        bool pass = true;
        if (_selection != null)
        {
            pass = _selection.SetSelected(value);
        }

        if (!pass)
        {
            return;
        }
    }
}
