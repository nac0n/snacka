using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Interface for an object in the world. This does not include creatures which is an own superclass.
    interface IObject
    {
        int posX { get; set; }
        int posY { get; set; }
        float expModifier { get; }
        int giveHP { get; }
        
        //In the future: Make use of these bools beneath :).
        bool IsDestructable { get; }
        bool IsObtainable { get; }
        bool IsPassable { get; }
        bool IsMoveable { get; }
    }
}
