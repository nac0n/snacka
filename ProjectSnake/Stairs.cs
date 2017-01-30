using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    class Stairs : IObject
    {
        public int posX;
        public int posY;
        public int giveHP = 0;

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
                return false;
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
