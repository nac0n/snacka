using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Potion object in world. It isn't doing much at the moment...
    public class Potion : ICollideableObject
    {
        public int posX;
        public int posY;
        public int prevPosX;
        public int prevPosY;
        private int giveHP = 50;
        private bool HasCollided;

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

        bool ICollideable.HasCollided
        {
            get { return HasCollided; }

            set { HasCollided = value; }
        }

        bool ICollideable.IsDestructable
        {
            get { return true; }
        }

        bool ICollideable.IsObtainable
        {
            get { return true; }
        }

        bool ICollideable.IsPassable
        {
            get { return false; }
        }

        bool ICollideable.IsMoveable
        {
            get { return false; }
        }

        float IHasProperties.expModifier
        {
            get { return 0; }
        }

        int IHasProperties.giveHP
        {
            get { return 20; }
        }
    }
}
