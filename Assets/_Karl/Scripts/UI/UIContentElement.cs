using UnityEngine;
using UnityEngine.Events;

public abstract class UIContentElement : UIMonoBehaviour
{
    [Header(H_A + "Element" + H_B)]
    [SerializeField] private UIContent _content;
    
    public class Event : UnityEvent<UIContentElement>{ }
    
    public UIContent GetContent() => _content;
    
    public void SetContent(UIContent value) {
        _content = value;
    }
    
    protected abstract bool CanSelect();

    public virtual void SetIsSelected(bool isSelected)
    {
        
    }
    
    public void Select()
    {
        if (!CanSelect()) return;
        _content.Select(this);
    }
}
