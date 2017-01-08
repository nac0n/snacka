using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Player in world, inherits from superclass Creature.cs
    class Player : Creature
    {
        public int prevPosX;
        public int prevPosY;

        public Player()
        {
            color = ConsoleColor.Yellow;
            hasCollided = false;
        }
        public Player(int posX, int posY)
        {
            color = ConsoleColor.Yellow;
            this.posX = posX;
            this.posY = posY;
            hasCollided = false;
        }

        public void setPositionX(int x)
        {
            posX = x;
        }

        public void setPositionY(int y)
        {
            posY = y;
        }
        public void setPrevPositionX(int x)
        {
            prevPosX = x;
        }

        public void setPrevPositionY(int y)
        {
            prevPosY = y;
        }

    }
}
