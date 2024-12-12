using UnityEngine;

public class PickupObject : MonoBehaviour, IInteractible, IDisplayable
{
    [Header("Animators")]
    [Tooltip("When object is picked up, gates will open")]
    [SerializeField]
    private Animator gatesAnimator;
    [Tooltip("When object is picked up, wall will push player out")]
    [SerializeField]
    private Animator wallAnimator;
    [Tooltip("When object is picked up, stand will become trigger to push out the player")]
    [SerializeField]
    private Animator standAnimator;

    public string Name() => "Pickup Shpere. Can throw to create an icy field";

    public string Description() => "Pick up";

    public void Interact(PlayerController player)
    {
        var throwComponent = player.GetComponent<ThrowProjectileInventory>();
        
        if(throwComponent != null)
        {
            throwComponent.HasProjectile = true;
        }

        gatesAnimator.SetBool("Open", true);
        wallAnimator.SetTrigger("Push");
        standAnimator.SetTrigger("Move");

        Destroy(gameObject);
    }
}
