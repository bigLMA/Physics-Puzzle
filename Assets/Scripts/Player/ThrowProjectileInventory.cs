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
    [SerializeField]
    private int projectileCount = 4;

    private GameObject[] projectilePooling;

    public bool HasProjectile { get; set; } = false;

    private bool canThrow = true;

    private void Start()
    {
        projectilePooling = new GameObject[projectileCount];

        for (int i = 0; i < projectileCount; i++)
        {
            projectilePooling[i] = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity);
            projectilePooling[i].SetActive(false);
        }
    }

    public void ThrowProjectile()
    {
        print("BEFORE CHECK");

        if (!HasProjectile || !canThrow) return;

        print("CAN THROW");

        var projectile = GetPooledObject();
        projectile.SetActive(true);
        projectile.transform.position = throwPoint.position;
        projectile.transform.rotation = throwPoint.rotation;

        var projectileRb = projectile.GetComponent<Rigidbody>();
        projectileRb.linearVelocity = throwPoint.forward.normalized * projectileVelocity;
        canThrow = false;

        StartCoroutine(nameof(IntervalCoroutine));
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i <projectileCount;++i)
        {
            if (!projectilePooling[i].activeInHierarchy)
            {
                StartCoroutine(ProjectileCoroutine(i, projectileCount*throwInterval-1));
                return projectilePooling[i];
            }
        }

        return null;
    }

    private IEnumerator IntervalCoroutine()
    {
        yield return new WaitForSeconds(throwInterval);

        canThrow = true;
    }

    private IEnumerator ProjectileCoroutine(int idx, float projectileLifetime)
    {
        yield return new WaitForSeconds(projectileLifetime);

        projectilePooling[idx].SetActive(false);
    }
}
