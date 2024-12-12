using UnityEngine;

public class FeetComponent : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        // TODO HARDCODED LAYER
        if (other.gameObject.layer == 3) return;

        if (other.gameObject == player) return;

        player.SetGrounded(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3) return;

        if (other.gameObject == player) return;

        player.SetGrounded(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 3) return;

        if (other.gameObject == player) return;

        player.SetGrounded(true);
    }
}
