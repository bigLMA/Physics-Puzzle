using UnityEngine;

public class PickupObject : MonoBehaviour, IInteractible, IDisplayable
{
    [Header("Gates animator")]
    [Tooltip("When object is picked up, gates will open")]
    [SerializeField]
    private Animator gatesAnimator;

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

        Destroy(gameObject);
    }
}
