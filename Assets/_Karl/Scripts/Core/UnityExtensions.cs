using UnityEngine;

public static class UnityExtensions
{
    public static void DestroyChildren(this Transform transform)
    {
        if (transform == null) return;
        int max = transform.childCount - 1;
        for (int i = max; i >= 0; i--)
        {
            MonoBehaviour.Destroy(transform.GetChild(i).gameObject);
        }
    }
    
    public static void SetLayerRecursively(this GameObject gameObject, int newLayer)
    {
        if (null == gameObject)
        {
            return;
        }
       
        gameObject.layer = newLayer;
       
        foreach (Transform child in gameObject.transform)
        {
            if (child == null)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public static Color SetAlpha(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}
