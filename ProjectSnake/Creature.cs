using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Abstract class to define all "living" things in the world. 
    //Includes player and monsters.
    public class Creature: IHasPosition, IHasCollision
    {   
        public string sprite { get; set; }
        public int posX;
        public int posY;
        public int prevPosX;
        public int prevPosY;
        public ConsoleColor color { get; set; }
        public int maxHealth { get; set; }
        public int health { get; set; }
        public int damage { get; set; }
        public int level { get; set;  }
        public bool HasCollided { get; set; }

        public void SetLevel(int newLevel)
        {
            level = newLevel;
        }

        public void SetHealth(int modifier)
        {
            health += modifier;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }

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
        public bool isDead()
        {
            if (health == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
