using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Wall object in world. Unpassable! Unbreakable!
    class Wall : IObject
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
                return false;
            }
        }

        bool IObject.gPassable
        {
            get
            {
                return false;
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
