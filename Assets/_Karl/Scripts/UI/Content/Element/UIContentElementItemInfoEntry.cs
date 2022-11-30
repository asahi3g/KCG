using TMPro;
using UnityEngine;

public class UIContentElementItemInfoEntry : UIContentElement
{
    [SerializeField] private TMP_Text _tmpLeft;
    [SerializeField] private TMP_Text _tmpRight;
    
    protected override bool CanSelect()
    {
        return false;
    }

    public void SetInfo(string leftText, string rightText)
    {
        _tmpLeft.SetText(leftText);
        _tmpRight.SetText(rightText);
    }
    
    public void SetInfo(object leftText, object rightText)
    {
        SetInfo(leftText?.ToString(), rightText?.ToString());
    }

    public void Clear()
    {
        _tmpLeft.Clear();
        _tmpRight.Clear();
    }
}
