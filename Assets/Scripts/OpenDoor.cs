using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject openObject;
    [SerializeField]
    private Animator gatesAnimator;

    private void OnCollisionEnter(Collision collision)
    {
        // If collides with door opener, start open door animation
        if (collision.gameObject == openObject) gatesAnimator.SetTrigger("Open");
    }
}
