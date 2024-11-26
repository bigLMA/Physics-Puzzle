using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IInteractible
{
    public void Interact(PlayerController player);

    public bool CanInteract(PlayerController player) => true;
}
