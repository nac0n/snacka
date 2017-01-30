using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Stone object in world. Is gonna be able to be moved and destroyed.
    class Stone : IObject
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
                return true;
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
                return false;
            }
        }
        bool IObject.IsMoveable
        {
            get
            {
                return true;
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
