using UnityEngine;

public class ObjectSpawner : MonoBehaviour, IInteractTarget
{
    [SerializeField]
    private SpawnedObject target;

    // Initial position and rotation
    private Vector3 spawnPosition;
    private Vector3 spawnRotation;

    private void Start()
    {
        // Save initial position and rotation
        spawnPosition = target.transform.position;
        spawnRotation = target.transform.eulerAngles;

        target.outOfBounds += ResetTargetPosition;
    }

    // Return target to initial position
    private void ResetTargetPosition()
    {
        target.transform.position = spawnPosition;
        target.transform.eulerAngles = spawnRotation;

        var targetRb = target.GetComponent<Rigidbody>();    
        targetRb.isKinematic = true;
        targetRb.useGravity = false;
    }

    public void OnInteract()
    {
        ResetTargetPosition();
    }
}
