using UnityEngine;

public class SafeAreaAdjuster : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        #if UNITY_ANDROID
        rectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
        #endif
    }

    public void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 minAnchor = safeArea.position;
        Vector2 maxAnchor = minAnchor + safeArea.size;
        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;
        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }
}