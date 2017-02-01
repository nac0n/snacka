using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSnake
{
    //Player in world, inherits from superclass Creature.cs
    public class Player : Creature
    {
        private int currentExp;
        private int maxExp;
        private double MaxExpModifier;

        public Player()
        {
            sprite = "@";
            currentExp = 0;
            color = ConsoleColor.Yellow;
            level = 1;
            maxHealth = 20;
            health = 20;
            damage = 2;
        }

        public Player(int posX, int posY)
        {
            sprite = "@";
            currentExp = 0;
            color = ConsoleColor.Yellow;
            this.posX = posX;
            this.posY = posY;
            level = 1;
            maxHealth = 20;
            health = 20;
            damage = 2;
        }
        
        public void LevelUp()
        {
            level += 1;
            double temp = maxExp * MaxExpModifier;
            maxExp = (int)temp;
            currentExp = 0;
        }

        public void GiveExp(Monster m)
        {
            currentExp = m.GetMaxHealth() + m.GetLevel();
        }

        public int GetMaxExp()
        {
            return maxExp;
        }

        public double GetMaxExpModifier()
        {
            return MaxExpModifier;
        }
        
    }
}
