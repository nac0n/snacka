using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Potion object in world. It isn't doing much at the moment...
    public class Potion : IHasProperties, IHasPosition
    {
        public int posX;
        public int posY;
        public int prevPosX;
        public int prevPosY;
        private int giveHP = 50;
        private bool hasCollided;

        int IHasPosition.posX
        {
            get { return posX; }
            set { posX = value; }

        }

        int IHasPosition.posY
        {
            get { return posY; }
            set { posY = value; }
        }

        int IHasPosition.prevPosX
        {
            get { return prevPosX; }

            set { prevPosX = value; }
        }

        int IHasPosition.prevPosY
        {
            get { return prevPosY; }

            set { prevPosY = value; }
        }


        float IHasProperties.expModifier
        {
            get { return 0; }

        }

        int IHasProperties.giveHP
        {
            get { return giveHP; }

        }

        bool IHasCollision.IsDestructable
        {
            get
            {
                return false;
            }
        }

        bool IHasProperties.IsObtainable
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

        bool IObject.HasCollided
        {
            get
            {
                return hasCollided;
            }

            set
            {
                hasCollided = value;
            }
        }
        //bool IObject.IsCollideable
        //{
        //    get
        //    {
        //        return true;
        //    }
    }
}
