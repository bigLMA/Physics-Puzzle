using UnityEngine;

public class OutOfBoundsTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        print("Out");

        var resetTarget = other.GetComponent<SpawnedObject>();

        if(resetTarget != null )
        {
            resetTarget.BoundsCrossed();
        }
    }
}
