using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents objects, that can be interacted through mediator
/// </summary>
public interface IInteractTarget
{
    public void OnInteract();
}
