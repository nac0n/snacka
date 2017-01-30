using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Player in world, inherits from superclass Creature.cs
    public class Player : Creature
    {
        public int maxExp;
        public double MaxExpModifier;

        public Player()
        {
            color = ConsoleColor.Yellow;
            level = 1;
            maxHealth = 20;
            health = 20;
            damage = 2;
        }

        public Player(int posX, int posY)
        {
            color = ConsoleColor.Yellow;
            this.posX = posX;
            this.posY = posY;
            level = 1;
            maxHealth = 20;
            health = 20;
            damage = 2;
        }
        
        public bool MaxExpisReached()
        {
            return true;
        }

        public int getMaxExp()
        {
            double exp = maxExp* MaxExpModifier;
            
            return (int)exp;
        }
        public double changeExpModifier()
        {
            double modifier = level * 15.3;
            return 0.0;
        }
        
    }
}
