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
        public int giveHP = 50;

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

        float IObject.expModifier
        {
            get { return 0; }

        }

        int IObject.giveHP
        {
            get { return giveHP; }

        }

        bool IObject.IsDestructable
        {
            get
            {
                return false;
            }
        }

        bool IObject.IsObtainable
        {
            get
            {
                return true;
            }
        }

        bool IObject.IsPassable
        {
            get
            {
                return true;
            }
        }
        bool IObject.IsMoveable
        {
            get
            {
                return false;
            }
        }
        bool IObject.IsCollideable
        {
            get
            {
                return true;
            }
        }
    }
}
