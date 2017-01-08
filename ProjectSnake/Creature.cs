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
        public int health { get; set; }
        public int damage { get; set; }
        public bool hasCollided { get; set; }
        
    }
}
