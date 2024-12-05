using UnityEngine;

public class ShatteredProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject shatteredProjectilePrefab;
    [SerializeField]
    private float breakImpulse = 40f;
    [SerializeField]
    private bool breaksWalls;

    private Vector3 velocity;
    bool collided = false;
    private Rigidbody rb;
    private float velocityBreakModifier = 0.1f;

    public bool BreaksWalls { get => breaksWalls; private set => breaksWalls = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if(!collided) velocity = rb.linearVelocity * velocityBreakModifier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (BreaksWalls) return;

        if (collision.gameObject.name == "Player") return;

        if(collision.impulse.sqrMagnitude < breakImpulse * breakImpulse) return;

        var shattered = Instantiate(shatteredProjectilePrefab, transform.position, transform.rotation);
        Destroy(gameObject);

        // Save existing velocity to the parts of the projectile
        foreach(Transform t in shattered.transform)
        {
            var rb = t.GetComponent<Rigidbody>();
            rb.linearVelocity = velocity;
        }
    }
}
