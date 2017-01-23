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
        private int expGiven = 0;

        public Monster()
        {
            color = ConsoleColor.Red;
            maxHealth = 20;
            health = 8;
            damage = 2;

        }
        public Monster(int posX, int posY)
        {

            color = ConsoleColor.Red;
            this.posX = posX;
            this.posY = posY;
            maxHealth = 20;
            health = 8;
            damage = 2;
        }

        public void modifyGivenExp(int playerLevel)
        {
            //Aint finished method
            expGiven = 0;
        }
        
    }
}

    
