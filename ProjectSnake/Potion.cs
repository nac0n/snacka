using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Potion object in world. It isn't doing much at the moment...
    class Potion : IObject
    {
        public int posX;
        public int posY;

        int IObject.posX
        {
            get { return posX; }
            set { posX = value; }

        }

        int IObject.posY
        {
            get { return posY; }
            set { posY = value; }
        }

        bool IObject.gDestructable
        {
            get
            {
                return false;
            }
        }

        bool IObject.gObtainable
        {
            get
            {
                return true;
            }
        }

        bool IObject.gPassable
        {
            get
            {
                return true;
            }
        }
        bool IObject.gMoveable
        {
            get
            {
                return false;
            }
        }
    }
}
