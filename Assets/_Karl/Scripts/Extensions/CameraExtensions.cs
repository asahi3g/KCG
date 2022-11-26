using UnityEngine;

public static class CameraExtensions
{
    public static bool IsMouseOutsideGameView(this Camera camera)
    {
        var view = camera.ScreenToViewportPoint(Input.mousePosition);
        var isOutside = view.x < 0f || view.x > 1f || view.y < 0f || view.y > 1f;
        return isOutside;
    }
}
