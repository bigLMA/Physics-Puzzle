using UnityEngine;

public class GrabbedObject : MonoBehaviour, IInteractible
{
    public void Interact(PlayerController player)
    {
        if(((IInteractible)this).CanInteract(player))
        {
            player.Grab(this);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
