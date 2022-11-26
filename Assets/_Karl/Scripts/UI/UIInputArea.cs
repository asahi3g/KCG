using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIInputArea : UIMonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    [SerializeField] private bool _interactable = true;
    [SerializeField] private Camera _camera;
    [SerializeField] private Identifier _identifier;
    [SerializeField] private GameObject _retargetEvents = default;
    [SerializeField] private PointerEventData.InputButton[] _allowed;

    public readonly Event onBeginDrag = new Event();
    public readonly Event onDrag = new Event();
    public readonly Event onEndDrag = new Event();
    public readonly Event onPointerDown = new Event();
    public readonly Event onPointerUp = new Event();
    public readonly Event onPointerClick = new Event();
    public readonly Event onPointerMove = new Event();
    
    private PointerEventData _drag = null;
    private PointerEventData _pointerDown = null;
    private bool _pointerEntered;
    private PointerEventData _lastMove;
    public class Event : UnityEvent<PointerEventData>{ }

    public bool isDragging => _drag != null;

    public bool interactable
    {
        get => _interactable;
        set
        {
            _interactable = value;
        }
    }

    public Camera GetCamera() => _camera;

    public Identifier GetIdentifier() => _identifier;
    
    public PointerEventData GetLastMove()
    {
        if (_lastMove == null)
        {
            _lastMove = new PointerEventData(EventSystem.current)
            {
                position = new Vector2(Screen.width * 0.75f, Screen.height * 0.5f)
            };
        }
        return _lastMove;
    }

    void Update()
    {
        bool isOver = _camera.IsMouseOutsideGameView();
        
        if (isDragging)
        {
            if (isOver)
            {
                Debug.Log("cancelling drag because outside game view");
                OnEndDrag(_drag);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        TryRetargetEvents(eventData, ExecuteEvents.beginDragHandler);
        
        if (!interactable) return;
        if(_drag != null) return;
        if (!IsAllowed(eventData.button)) return;
        _drag = eventData;
        onBeginDrag.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        TryRetargetEvents(eventData, ExecuteEvents.dragHandler);
        
        if (_drag == null) return;
        if (_drag.pointerId != eventData.pointerId) return;
        onDrag.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData) {
        TryRetargetEvents(eventData, ExecuteEvents.endDragHandler);
        
        if (_drag == null) return;
        if (_drag.pointerId != eventData.pointerId) return;
        _drag = null;
        onEndDrag.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TryRetargetEvents(eventData, ExecuteEvents.pointerClickHandler);
        
        if (!interactable) return;
        if(isDragging) return;
        if (!IsAllowed(eventData.button)) return;
        onPointerClick.Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TryRetargetEvents(eventData, ExecuteEvents.pointerDownHandler);
        
        if (!interactable) return;
        if (_pointerDown != null) return;
        if (!IsAllowed(eventData.button)) return;
        _pointerDown = eventData;
        onPointerDown.Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        TryRetargetEvents(eventData, ExecuteEvents.pointerUpHandler);
        
        if (_pointerDown == null) return;
        if (_pointerDown.pointerId != eventData.pointerId) return;
        _pointerDown = null;
        onPointerUp.Invoke(eventData);
    }

    private bool IsAllowed(PointerEventData.InputButton value)
    {
        if (_allowed == null) return false;
        int length = _allowed.Length;
        if (length == 0) return false;
        return ((IList) _allowed).Contains(value);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TryRetargetEvents(eventData, ExecuteEvents.pointerEnterHandler);
        
        if (!interactable) return;
        if (_pointerEntered) return;
        _pointerEntered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TryRetargetEvents(eventData, ExecuteEvents.pointerExitHandler);
        
        if (!_pointerEntered) return;
        _pointerEntered = false;
    }
    
    private void TryRetargetEvents<T>(PointerEventData eventData, ExecuteEvents.EventFunction<T> eventFunction) where T : IEventSystemHandler
    {
        if (_retargetEvents == null)
        {
            return;
        }

        if (_retargetEvents == this.gameObject)
        {
            Debug.LogWarningFormat("Retarget gameObject is the same, this will result in infinite loop, skipping..");
            return;
        }
        ExecuteEvents.Execute(_retargetEvents, eventData, eventFunction);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        _lastMove = eventData;
        onPointerMove.Invoke(eventData);
    }
}
