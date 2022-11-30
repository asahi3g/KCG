using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class UIContentElement : UIMonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header(H_A + "Element" + H_B)]
    [SerializeField] private UIContent _content;
    
    public class Event : UnityEvent<UIContentElement>{ }
    public class Event2 : UnityEvent<UIContentElement, UIContentElement>{ }
    
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

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
