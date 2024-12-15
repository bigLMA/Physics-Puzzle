using UnityEngine;

public class EffectProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject surfacePrefab;
    [SerializeField]
    private float surfaceRadiusMin = 1.2f;
    [SerializeField]
    private float surfaceRadiusMax = 2.5f;
    [SerializeField]
    private GameObject iceParticle;
    [SerializeField]
    private string groundLayer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player") return;

        // Get surface position
        Vector3 pos = collision.GetContact(0).point;

        var particle =Instantiate(iceParticle, pos, Quaternion.identity);
        Destroy(particle, 5f);

        int groundLayerNum = LayerMask.NameToLayer(groundLayer);

        if (collision.gameObject.layer != groundLayerNum)
        {
            gameObject.SetActive(false);
            return;
        }



        // Get random zone radiusn
        float radius = Random.Range(surfaceRadiusMin, surfaceRadiusMax);

        // Create surface
        var surface = Instantiate(surfacePrefab, pos, Quaternion.Euler(90f, 0f, 0f));
        surface.transform.localScale = new Vector3(radius, radius, 1f);

        // Deactivate projectile
        gameObject.SetActive(false);
    }
}
