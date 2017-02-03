using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Potion object in world.
    public class Potion : ICollideableObject
    {
        private int posX;
        private int posY;
        private int prevPosX;
        private int prevPosY;
        private int giveHP = 50;
        private bool HasCollided;
        private bool isObtainAble = true;

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

        bool ICollideable.IsKillable
        {
            get { return true; }
        }

        bool ICollideable.IsDestructable
        {
            get { return true; }
        }

        bool ICollideable.IsObtainable
        {
            get { return isObtainAble; }
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

        int IHasProperties.givenHP
        {
            get { return 20; }
        }

       

        void ICollideableObject.SetBackPosition()
        {

        }
        void ICollideableObject.GiveHP(ICollideableObject obj)
        {
            if (isObtainAble)
            {

            }
        }
    }
}
