using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// When player looks at displayble objects, UI will display info about them
/// <para><see cref="MainUI"/> displays info </para>
/// </summary>
public interface IDisplayable
{
    /// <summary>
    /// Name of object, some properties as well
    /// </summary>
    public string Name();

    /// <summary>
    /// My rule of thumb if can interact, then description is name of what will happen on interact.
    /// <para>i.e. "Grab" for Grabbed objects etc (<see cref="GrabbedObject"/>)</para>
    /// </summary>
    public string Description();

    /// <summary>
    /// If true, will display description starting with "LMB:"
    /// </summary>
    public bool CanInteract() => true;
}