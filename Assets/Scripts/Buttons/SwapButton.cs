using UnityEngine;

public class SwapButton : MonoBehaviour, IDisplayable, IInteractible
{
    [Header("Spawapped objects transform components")]
    [SerializeField]
    private Transform firstTransform;
    [SerializeField] 
    private Transform secondTransform;

    public string Name() => "Swap button";

    public string Description() => "Swap objects";

    public void Interact(PlayerController player)
    {
        // save first transform
        Vector3 firstPos = firstTransform.position;
        Quaternion firstRot = firstTransform.rotation;

        // swap objects
        firstTransform.position = secondTransform.position;
        firstTransform.rotation = secondTransform.rotation;
        secondTransform.position = firstPos; 
        secondTransform.rotation = firstRot;

        // Enable dynamic physics on onbjects
        var firstRb = firstTransform.GetComponent<Rigidbody>();
        var secondRb = firstTransform.GetComponent<Rigidbody>();
        firstRb.useGravity = true;
        firstRb.isKinematic = false;
        secondRb.useGravity = true;
        secondRb.isKinematic = false;

    }
}
