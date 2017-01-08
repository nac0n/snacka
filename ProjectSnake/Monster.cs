using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //A monster in the world, inherits from superclass Creature.cs
    class Monster : Creature
    {
        public int prevPosX;
        public int prevPosY;

        public Monster()
        {
            color = ConsoleColor.Red;
            hasCollided = false;
        }
        public Monster(int posX, int posY)
        {

            color = ConsoleColor.Red;
            this.posX = posX;
            this.posY = posY;
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

    
