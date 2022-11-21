using UnityEngine;

public class UIViewSettings : UIView
{
    [SerializeField] private UIContent _content;
    
    
    protected override void OnGroupOpened()
    {
        UpdateLook();
    }

    protected override void OnGroupClosed()
    {
        
    }


    private void UpdateLook()
    {
        UIContentElementSettingsEntry[] entries = _content.GetElements<UIContentElementSettingsEntry>(true);
        int lenght = entries.Length;
        for (int i = 0; i < lenght; i++)
        {
            entries[i].UpdateLook();
        }
    }
}
