using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents interact targets
/// </summary>
public interface IInteractible
{
    /// <summary>
    ///  Start interact
    /// </summary>
    /// <param name="player">Player script reference</param>
    public void Interact(PlayerController player);

    /// <summary>
    ///  Can override to create different behaviour based on can object interact or not
    /// </summary>
    /// <param name="player">Player script reference</param>
    public bool CanInteract(PlayerController player) => true;
}
