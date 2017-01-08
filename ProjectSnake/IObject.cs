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

        //In the future: Make use of these bools beneath :).
        bool gDestructable { get; }
        bool gObtainable { get; }
        bool gPassable { get; }
        bool gMoveable { get; }
    }
}
