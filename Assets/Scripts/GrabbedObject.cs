using UnityEngine;

public class GrabbedObject : MonoBehaviour, IInteractible, IDisplayable
{
    public void Interact(PlayerController player)
    {
        if(((IInteractible)this).CanInteract(player))
        {
            player.Grab(this);
        }
    }

    public string Name() => "Interact cube";

    public string Description() => "Grab";
}
