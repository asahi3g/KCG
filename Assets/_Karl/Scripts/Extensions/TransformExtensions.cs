using UnityEngine;

public static class TransformExtensions
{

    public static void DestroyChildren(this Transform value)
    {
        if (value == null) return;
        int max = value.childCount - 1;
        for (int i = max; i >= 0; i--)
        {
            Transform child = value.GetChild(i);
            if(child == null) continue;
            MonoBehaviour.Destroy(child.gameObject);
        }
    }
    
}
