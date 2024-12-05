using UnityEngine;

public class CannonInteract : MonoBehaviour, IInteractible, IDisplayable
{
    [SerializeField]
    private GameObject cannonCamera;
    [SerializeField]
    private GameObject hud;

    public string Name() => "Cannon";

    public string Description() => "Use";

    public void Interact(PlayerController player)
    {
        CannonController cannon = GetComponent<CannonController>();

        hud.SetActive(false);
        player.gameObject.SetActive(false);
        cannon.enabled = true;
        cannon.PlayerRef = player.gameObject;
        cannonCamera.SetActive(true);
    }
}
