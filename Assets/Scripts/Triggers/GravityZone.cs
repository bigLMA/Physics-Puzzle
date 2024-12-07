using UnityEngine;

public class GravityZone : MonoBehaviour, IDisplayable
{
    [Header("Gravity Zone")]
    [SerializeField]
    [Tooltip("Zone won't affect objects with bigger mass")]
    private float maxMass = 10f;
    [SerializeField]
    [Tooltip("Pull strength")]
    private float pullStrength = 13.5f;
    [SerializeField]
    [Tooltip("How much slow to apply when rigid body overlaps")]
    private float overlapSlow = 5f;

    private void OnTriggerEnter(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        
        // Check if rb and appropriate mass
        if (rb == null || rb.mass > maxMass) return;

        // Turn off gravity
        rb.useGravity = false;
        // Slow rigid body
        rb.linearVelocity /= overlapSlow;
        // Pull object up
        rb.AddForce(transform.up * Time.fixedDeltaTime * pullStrength, ForceMode.VelocityChange);
    }

    private void OnTriggerStay(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        // Check if rb and appropriate mass
        if (rb == null || rb.mass > maxMass) return;

        //rb.useGravity = false;
        // Pull object up
        rb.AddForce(transform.up * Time.fixedDeltaTime * pullStrength, ForceMode.VelocityChange);
    }

    private void OnTriggerExit(Collider other)
    {
        var rb = other.GetComponent<Rigidbody>();
        // Check if rb and appropriate mass
        if (rb == null || rb.mass > maxMass) return;
        // Reset gravity use
        rb.useGravity = true;
    }

    public string Name() => $"Gravity Zone, maximum pull mass: {maxMass}";

    public string Description() => "Lift box";

    bool IDisplayable.CanInteract()=> false;
}
