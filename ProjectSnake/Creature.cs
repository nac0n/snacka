using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Superclass to define all "living" things in the world. Includes player and monsters.
    class Creature
    {
        public string sprite { get; set; }
        public ConsoleColor color { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public int prevPosX { get; set; }
        public int prevPosY { get; set; }
        public int maxHealth { get; set; }
        public int health { get; set; }
        public int damage { get; set; }
        public int level { get; set;  }

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
