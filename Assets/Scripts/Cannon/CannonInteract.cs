using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CannonInteract : MonoBehaviour, IInteractible, IDisplayable
{
    [SerializeField]
    private GameObject cannonCamera;
    [SerializeField]
    private GameObject playerHud;
    [SerializeField]
    private GameObject cannonHud;

    private PlayerController playerRef;

    private void Start()
    {
        GetComponent<CannonController>().OnInteractHandler += SwitchPossesedObject;
    }

    public string Name() => "Cannon";

    public string Description() => "Use";

    public void Interact(PlayerController player)
    {
        playerRef = player;
        SwitchPossesedObject(true);
    }

    public void SwitchPossesedObject(bool cannon)
    {
        CannonController cannonController = GetComponent<CannonController>();

        // Deactivate player object
        playerRef.gameObject.SetActive(!cannon);

        // Activate cannon controller script and cannon camera
        cannonController.enabled = cannon;
        cannonCamera.SetActive(cannon);

        // Switch HUD
        cannonHud.SetActive(cannon);
        playerHud.SetActive(!cannon);
    }
}
