using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSnake
{

    //As class name suggests, contains funtions that can check for collisions.

    public static class CollisionHandler
    {
        //Initialcheck to see if collision has happened.

        public static void CollisionCheck()
        {
            for(int i = 0; i < ListHandler.GetInstance().GetAllCollideables().Count; i++)
            {
                for (int x = i; x < ListHandler.GetInstance().GetAllCollideables().Count; x++)
                {
                    if(ListHandler.GetInstance().GetAllCollideables().ElementAt(i).posX == ListHandler.GetInstance().GetAllCollideables().ElementAt(x).posX &&
                       ListHandler.GetInstance().GetAllCollideables().ElementAt(i).posY == ListHandler.GetInstance().GetAllCollideables().ElementAt(x).posY)
                    {

                    }
                }
            }
        }

        public static void DestroyObject()
        {

        }

        public static void ObtainObject()
        {

        }

        public static void MoveObject()
        {

        }

        //        foreach (Creature c in ListHandler.GetInstance().GetCreatures())
        //            {
        //                foreach(IObject io in ListHandler.GetInstance().GetObjects())
        //                {
        //                    if (c.posX == io.posX && c.posY == io.posY)
        //                    {
        //                        c.HasCollided = true;
        //                        io.HasCollided = true;
        //                    }
        //}
        //            }

        //            foreach (IObject io in ListHandler.GetInstance().GetObjects())
        //            {
        //                foreach (IObject io2 in ListHandler.GetInstance().GetObjects())
        //                {
        //                    if (io.posX == io2.posX && io.posY == io2.posY)
        //                    {
        //                        io.HasCollided = true;
        //                        io2.HasCollided = true;
        //                    }
        //                }
        //            }
        //            foreach (Creature c in ListHandler.GetInstance().GetCreatures())
        //            {
        //                foreach (Creature c2 in ListHandler.GetInstance().GetObjects())
        //                {
        //                    if (c.posX == c2.posX && c.posY == c2.posY)
        //                    {
        //                        c.HasCollided = true;
        //                        c2.HasCollided = true;
        //                    }
        //                }
        //            }
        //public static bool HasCollided(Creature c1, Creature c2)
        //{

        //    return false;
        //}

        //public bool HasCollided(Creature creature, IObject worldObject)
        //{
        //    if(creature.posX == worldObject.posX && creature.posY == worldObject.posY)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public bool HasCollided(Creature creature1, Creature creature2)
        //{
        //    if (creature1.posX == creature2.posX && creature1.posY == creature2.posY)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        //public bool HasCollided(IObject worldObject1, IObject worldObject2)
        //{
        //    if (worldObject1.posX == worldObject2.posX && worldObject1.posY == worldObject2.posY)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}
