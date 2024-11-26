using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDisplayable
{
    public string Name();

    public string Description();

    public bool CanInteract() => true;
}