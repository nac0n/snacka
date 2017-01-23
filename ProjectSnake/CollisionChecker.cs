using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{

    //As class name suggests, contains funtions that can check for collisions.
    class CollisionChecker
    {
        public CollisionChecker()
        {

        }

        public bool HasCollided(Creature creature, IObject worldObject)
        {
            if(creature.posX == worldObject.posX && creature.posY == worldObject.posY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasCollided(Creature creature1, Creature creature2)
        {
            if (creature1.posX == creature2.posX && creature1.posY == creature2.posY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HasCollided(IObject worldObject1, IObject worldObject2)
        {
            if (worldObject1.posX == worldObject2.posX && worldObject1.posY == worldObject2.posY)
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
