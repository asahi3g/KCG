using UnityEngine;

public class UIContentSelection : BaseMonoBehaviour
{
    [SerializeField] private bool _deselectOnEnable;
    [SerializeField] private bool _canDeselect;
    [SerializeField] private UIContentElement _selected;
    [SerializeField] private UIContent[] _scope;

    public readonly UIContentElement.Event onSelect = new UIContentElement.Event();

    public bool IsSelected() => _selected != null;

    public UIContentElement GetSelected() => _selected;
    
    public T GetSelected<T>() where T : UIContentElement
    {
        return (T) _selected;
    }


    protected override void OnEnable()
    {
        base.OnEnable();

        if (_deselectOnEnable)
        {
            SetSelected(null);
        }
    }


    public void ClearSelection()
    {
        SetSelected(null);
    }

    public bool SetSelected(UIContentElement value)
    {
        // Clicking the same
        bool clickedSame = _selected != null && _selected == value;
        if (clickedSame)
        {
            if (_canDeselect)
            {
                value = null;
            }
        }
        
        if (_selected != null)
        {
            _selected.SetIsSelected(false);
        }
        _selected = value;
        UpdateIsSelected();
        return true;
    }

    public void Deselect()
    {
        SetSelected(null);
    }
    
    public void UpdateIsSelected() {
        if(_scope == null) return;
        int length = _scope.Length;
        
        onSelect.Invoke(_selected);
        
        for(int i = 0; i < length; i++) {
            UIContent content = _scope[i];
            UIContentElement[] elements = content.GetElements<UIContentElement>(true);
            foreach(UIContentElement element in elements) {
                if(element == null) continue;
                bool isSelected = element == _selected;
                element.SetIsSelected(isSelected);
            }
        }
    }
}