using UnityEngine;
using UnityEngine.EventSystems;

public class UIInputAreaPointerPosition : UIMonoBehaviour
{
    [SerializeField] private UIInputArea _inputArea;


    protected override void OnEnable()
    {
        base.OnEnable();
        UpdateLook(_inputArea.GetLastMove());
        _inputArea.onPointerMove.AddListener(OnPointerMove);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _inputArea.onPointerMove.RemoveListener(OnPointerMove);
    }

    private void OnPointerMove(PointerEventData eventData)
    {
        UpdateLook(eventData);
    }

    private void UpdateLook(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle (_inputArea.rectTransform, eventData.position, _inputArea.GetCamera(), out Vector2 localPoint))
        {
            rectTransform.anchoredPosition = localPoint;
        }
    }
}
