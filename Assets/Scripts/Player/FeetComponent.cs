using UnityEngine;

public class FeetComponent : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private string triggerLayer;

    private int triggerLayerNum;

    private void Start()
    {
        triggerLayerNum = LayerMask.NameToLayer(triggerLayer);
        print(triggerLayerNum);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == triggerLayerNum) return;

        if (other.gameObject == player) return;

        player.SetGrounded(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == triggerLayerNum) return;

        if (other.gameObject == player) return;

        player.SetGrounded(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == triggerLayerNum) return;

        if (other.gameObject == player) return;

        player.SetGrounded(true);
    }
}
