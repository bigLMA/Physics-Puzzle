using System.Collections;
using UnityEngine;

public class ThrowProjectileInventory : MonoBehaviour
{
    [SerializeField]
    private Transform throwPoint;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private float projectileVelocity = 22f;
    [SerializeField]
    private float throwInterval = 1.2f;

    public bool HasProjectile { get; set; } = false;

    private bool canThrow = true;

    public void ThrowProjectile()
    {
        if (!HasProjectile || !canThrow) return;

        var projectile = Instantiate(projectilePrefab, throwPoint.position, throwPoint.rotation);   
        var projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.linearVelocity = throwPoint.forward.normalized * projectileVelocity;
        canThrow = false;

        StartCoroutine(nameof(IntervalCoroutine));
    }

    private IEnumerator IntervalCoroutine()
    {
        yield return new WaitForSeconds(throwInterval);

        canThrow = true;
    }
}
