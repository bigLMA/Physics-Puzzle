using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [Header("Crosshair Sides")]
    [SerializeField]
    private GameObject crosshairTop;
    [SerializeField]
    private GameObject crosshairBottom;
    [SerializeField]
    private GameObject crosshairRight;
    [SerializeField]
    private GameObject crosshairLeft;

    [Header("Crosshair sides offset")]
    [SerializeField]
    [Tooltip("Default distance from center of crosshair images")]
    private float defaultOffset = 15f;
    [SerializeField]
    [Tooltip("Distance from center of crosshair images when locked")]
    private float lockOffset = 8f;

    private void Start()
    {
        ResetCrosshair(defaultOffset);
    }

    /// <summary>
    /// Draw crosshair not as not locked at target
    /// </summary>
    public void DrawLoose()
    {
        ResetCrosshair(defaultOffset);
    }

    /// <summary>
    /// Draw crosshair not as locked at target
    /// </summary>
    public void DrawTight()
    {
        ResetCrosshair(lockOffset);
    }

    private void ResetCrosshair(float offset)
    {
        crosshairTop.GetComponent<RectTransform>().localPosition = new Vector3(0f, offset);
        crosshairBottom.GetComponent<RectTransform>().localPosition = new Vector3(0f, -offset);
        crosshairRight.GetComponent<RectTransform>().localPosition = new Vector3(offset, 0f);
        crosshairLeft.GetComponent<RectTransform>().localPosition = new Vector3(-offset, 0f);
    }
}
