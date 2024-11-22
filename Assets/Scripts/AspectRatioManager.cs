using UnityEngine;

public class AspectRatioManager : MonoBehaviour
{
    private void Start()
    {
        // Set the target aspect ratio (width / height)
        float targetAspect = 9.0f / 16.0f;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleWidth = windowAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleWidth < 1.0f)
        {
            // Add letterboxing (black bars on top and bottom)
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleWidth;
            rect.x = 0;
            rect.y = (1.0f - scaleWidth) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            // Add pillarboxing (black bars on the sides)
            float scaleHeight = 1.0f / scaleWidth;

            Rect rect = camera.rect;

            rect.width = scaleHeight;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleHeight) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}

