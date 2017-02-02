using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Stairs object in the world. Ends the game and is gonna
    //  bring the player to the next level in other versions.
    public class Stairs : ICollideableObject
    {
        public int posX;
        public int posY;
        public int prevPosX;
        public int prevPosY;
        private int giveHP = 0;
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

        bool ICollideable.IsKillable
        {
            get { return false; }
        }

        bool ICollideable.IsDestructable
        {
            get { return false; }
        }

        bool ICollideable.IsObtainable
        {
            get { return false; }
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
            get { return 0; }
        }
        void ICollideableObject.SetBackPosition()
        {

        }
    }
}
