using UnityEngine;

public class EffectProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject surfacePrefab;
    [SerializeField]
    private float surfaceRadiusMin = 1.2f;
    [SerializeField]
    private float surfaceRadiusMax = 2.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player") return;

        // TODO HARDCODED LAYER
        if (collision.gameObject.layer != 6)
        {
            gameObject.SetActive(false);
            return;
        }

        // Get surface position
        Vector3 pos = collision.GetContact(0).point /*+ new Vector3(0f, 0.01f)*/;

        // Get random zone radiusn
        float radius = Random.Range(surfaceRadiusMin, surfaceRadiusMax);

        // Create surface
        var surface = Instantiate(surfacePrefab, pos, Quaternion.Euler(90f, 0f, 0f));
        surface.transform.localScale = new Vector3(radius, radius, 1f);

        // Deactivate projectile
        gameObject.SetActive(false);
    }
}
