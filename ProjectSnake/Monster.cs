using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSnake
{
    //A monster in the world, inherits from superclass Creature.cs
    public class Monster : Creature
    {
        private int givenExp = 10;
        
        public Monster()
        {
            sprite = "M";
            level = 1;
            color = ConsoleColor.Red;
            maxHealth = 20;
            health = maxHealth;
            damage = 2;

        }
        public Monster(int posX, int posY)
        {
            sprite = "M";
            level = 1;
            color = ConsoleColor.Red;
            this.posX = posX;
            this.posY = posY;
            maxHealth = 20;
            health = maxHealth;
            damage = 2;
        }

        public void LevelUp()
        {
            //Do a check later if player has finished level or not
            //if(course > level);

            level += 1;
            givenExp *= level;
            maxHealth += 10;
            health = maxHealth;
            damage += 2;
        }

    }
}

    
