using UnityEngine;
using UnityEngine.UI;

public class UIEffectGraphicFlash : UIMonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Graphic _graphic;
    [SerializeField] private Color _color;

    private Color _firstColor;
    private float _nextTime;
    private bool _other = true;

    protected override void Awake()
    {
        base.Awake();
        _firstColor = _graphic.color;
    }

    private void Update()
    {
        if (Time.time >= _nextTime)
        {
            _graphic.color = _other ? _color : _firstColor;
            _other = !_other;
            UpdateNextTime();
        }
    }

    private void UpdateNextTime()
    {
        _nextTime = Time.time + _delay;
    }
    
}
