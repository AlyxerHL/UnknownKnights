using UnityEngine;

public static class CameraExtension
{
    public static Vector2 WorldToCanvasPoint(this Camera camera, Vector3 worldPosition)
    {
        var viewportPosition = camera.WorldToViewportPoint(worldPosition);
        var canvasRect = PagesRouter.CurrentPage.RectTransform.rect;
        return new Vector2(
            (viewportPosition.x * canvasRect.width) - (canvasRect.width * 0.5f),
            (viewportPosition.y * canvasRect.height) - (canvasRect.height * 0.5f)
        );
    }
}
