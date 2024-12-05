using UnityEngine;

public class ShatteredWall : MonoBehaviour
{
    [Header("Shatter")]
    [SerializeField]
    private GameObject shatteredWallPrefab;
    [SerializeField]
    private float shatterImpulse = 70f;

    [Header("Explosion")]
    [SerializeField]
    private float explosionForce = 15f;
    [SerializeField]
    private float explosionRadius = 3f;
    [SerializeField]
    private float upwardsModifier = 2.5f;

    private void OnCollisionEnter(Collision collision)
    {
        ShatteredProjectile proj = collision.gameObject.GetComponent<ShatteredProjectile>();

        if (proj == null) return;

        // if projectile breaks walls
        if (proj.BreaksWalls)
        {
            // If projectile speed is enough
            if (collision.impulse.sqrMagnitude < shatterImpulse * shatterImpulse) return;

            // Destroy projectile and wall
            // Spawn shattered wall
            Vector3 contactPos = collision.GetContact(0).point;
            Destroy(gameObject);
            Destroy(collision.gameObject);
            var wall = Instantiate(shatteredWallPrefab, transform.position, Quaternion.identity);

            // Explode pieces of shattered wall
            foreach (Transform t in wall.transform)
            {
                var shatterRb = t.GetComponent<Rigidbody>();
                shatterRb.AddExplosionForce(explosionForce, contactPos, explosionRadius, upwardsModifier, ForceMode.VelocityChange);
            }
        }
    }
}
