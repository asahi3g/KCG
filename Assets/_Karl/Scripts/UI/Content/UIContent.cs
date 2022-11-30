using System;
using UnityEngine;

public class UIContent : UIMonoBehaviour
{
    [SerializeField] private UIContentElement _prefab;
    [SerializeField] private UIContentSelection _selection;
    [SerializeField] private bool _fetchElements = true;
    [SerializeField] private UIContentElement[] _elements;
    

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
    
    public T[] GetElements<T>(bool includeInactive) where T : UIContentElement
    {
        if (_fetchElements)
        {
            T[] t = rectTransform.GetComponentsInChildren<T>(includeInactive);
            _elements = Array.ConvertAll(t, item => (UIContentElement)item);
            return t;
        }
        return Array.ConvertAll(_elements, item => (T)item);;
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
