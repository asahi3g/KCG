using UnityEngine;

public static class GameObjectExtensions
{
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
}
