using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSnake
{
    //Interface for an object in the world. This does not include creatures which is an own superclass.
    public interface IHasProperties
    {
        float expModifier { get; }
        int givenHP { get; }
        
    }
}
