using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Input - ", menuName = "SO/Input", order = 1)]
public class SOInput : SO
{
    [TextArea(3,6)]
    [SerializeField] private string _title;
    [SerializeField] private KeyCode _primary;
    [SerializeField] private KeyCode _secondary;

    [System.Serializable]
    public class Event : UnityEvent<SOInput>{}

    public string GetTitle() => _title;
    public KeyCode GetPrimary() => _primary;
    public KeyCode GetSecondary() => _secondary;
}
