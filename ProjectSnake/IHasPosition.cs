using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    interface IHasPosition
    {
        int posX { get; set; }
        int posY { get; set; }
        int prevPosX { get; set; }
        int prevPosY { get; set; }
    }
}
