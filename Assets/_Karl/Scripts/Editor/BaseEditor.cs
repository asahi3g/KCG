using UnityEditor;
using UnityEngine;

public abstract class BaseEditor<T> : Editor where T : MonoBehaviour
{
    public new T target => (T) base.target;
}
