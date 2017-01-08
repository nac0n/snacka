using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Treasure object in world. Touching it ends the game and in future
    //brings the player to the next level.
    class Treasure : IObject
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
