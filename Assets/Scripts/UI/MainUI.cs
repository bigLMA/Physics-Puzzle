using TMPro;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [Header("Player Reference")]
    [SerializeField]
    private PlayerController player;

    [Header("UI components")]
    [SerializeField]
    private TextMeshProUGUI displayText;
    [SerializeField]
    private Crosshair crosshair;

    [Header("Cannon")]
    [SerializeField]
    private RectTransform barrelHorizontalAim;
    [SerializeField]
    private float barrelAimElevationRange = 11f;

    private IDisplayable displayedObject = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.DisplaybleLook += ChangeDisplayble;
    }

    private void ChangeDisplayble(IDisplayable displayable)
    {
        // Don't reset existing values
        if (displayedObject == null && displayable == null) return;
        if (displayedObject == displayable) return;

        // Check if displayble is not null
        if (displayable != null)
        {
            // Reset displayeble object
            displayedObject = displayable;

            // Deactivate display text
            displayText.gameObject.SetActive(true);

            // Display UI text
            string interactButton = displayable.CanInteract() ? "LMB:" : string.Empty;
            displayText.text = $"{interactButton} {displayable.Description()}\n{displayable.Name()}";

            // Draw crosshair as locked at target
            crosshair.DrawTight();
        }
        else // otherwise
        {
            // Deactivate display text
            displayText.gameObject.SetActive(false);

            // Set displayed object to null
            displayedObject = null;

            // Draw crosshair as not locked at target
            crosshair.DrawLoose();
        }
    }

    public void ElevateBarrelAim(float elevation)
    {
        Vector3 pos = barrelHorizontalAim.transform.localPosition;
        Vector3 clampedPos = new Vector3(pos.x, Mathf.Clamp(pos.y + elevation, -barrelAimElevationRange, barrelAimElevationRange), pos.z);
        barrelHorizontalAim.localPosition = clampedPos;
    }
}
