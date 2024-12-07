using System.Collections;
using UnityEngine;

public class PushPlayerTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator gatesAnimator;
    [SerializeField]
    private float closeGatesDelay = 1.5f;
    [SerializeField]
    private GameObject pushWall;
    [SerializeField]
    ThrowProjectileInventory playerProjectileInventory;

    bool pushes = false;

    private void OnTriggerEnter(Collider other)
    {
        if (pushes) return;

        if(playerProjectileInventory.HasProjectile)
        {
            pushes = true;

            pushWall.SetActive(true);
            var pushWallAnimator = pushWall.GetComponent<Animator>();
            pushWallAnimator.SetTrigger("Push");

            StartCoroutine(nameof(CloseGateCoroutine));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (pushes) return;

        if (playerProjectileInventory.HasProjectile)
        {
            pushes = true;

            pushWall.SetActive(true);
            var pushWallAnimator = pushWall.GetComponent<Animator>();
            pushWallAnimator.SetTrigger("Push");

            StartCoroutine(nameof(CloseGateCoroutine));
        }
    }

    private IEnumerator CloseGateCoroutine()
    {
        yield return new WaitForSeconds(closeGatesDelay);

        gatesAnimator.SetBool("Open", false);
       // Destroy(pushWall, closeGatesDelay);
    }
}
