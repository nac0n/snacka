using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //Abstract class to define all "living" things in the world. 
    //Includes player and monsters.
    public class Creature: ICollideableObject
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

        int IHasProperties.givenHP
        {
            get { return 0; }
        }

        void ICollideableObject.GiveHP(ICollideableObject obj)
        {
            
            
        }

        void ICollideableObject.SetBackPosition()
        {
            posX = prevPosX;
            posY = prevPosY;
        }

        public void SetLevel(int newLevel)
        {
            level = newLevel;
        }
        public int GetLevel()
        {
            return level;
        }

        public void SetHealth(int modifier)
        {
            health += modifier;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public int GetHealth()
        {
            return health;
        }

        public void SetMaxHealth(int newHealth)
        {
            maxHealth = newHealth;
        }
        public int GetMaxHealth()
        {
            return maxHealth;
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
