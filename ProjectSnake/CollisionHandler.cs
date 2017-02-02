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
            for (int i = 0; i < ListHandler.GetInstance().GetAllCollideables().Count; i++)
            {
                var clist = ListHandler.GetInstance().GetAllCollideables();

                if (i != clist.Count -1)
                {
                    
                    for (int x = i+1; x < clist.Count; x++)
                    {
                        if(clist.ElementAt(i).posX == clist.ElementAt(x).posX && clist.ElementAt(i).posY == clist.ElementAt(x).posY)
                        {
                            //clist.ElementAt(i).HasCollided = true;
                            //clist.ElementAt(x).HasCollided = true;
                            AfterCollision(clist.ElementAt(i), clist.ElementAt(x));

                            //if (clist.ElementAt(x).HasCollided == true)
                            //{
                            //  clist.ElementAt(x).SetBackPosition();
                            //  clist.ElementAt(x).HasCollided = false;
                            //}
                        }
                    }
                }
            }
        }

        public static void AfterCollision(ICollideableObject x, ICollideableObject y)
        {
            if(x.IsDestructable || y.IsDestructable)
            {
                if(x.IsDestructable)
                {
                    RemoveObject(x);
                }
                if(y.IsDestructable)
                {
                    RemoveObject(y);
                }
                else
                {
                    x.SetBackPosition();
                    y.SetBackPosition();
                }
            }
            else if(x.IsObtainable || y.IsObtainable)
            {
                if(x.IsObtainable)
                {

                }
                if(y.IsObtainable)
                {

                }
                else
                {
                    x.SetBackPosition();
                    y.SetBackPosition();
                }
            }
            else if (x.IsMoveable || y.IsMoveable)
            {
                if (x.IsObtainable)
                {

                }
                if (y.IsObtainable)
                {

                }
                else
                {
                    x.SetBackPosition();
                    y.SetBackPosition();
                }
            }
            else if (x.IsPassable || y.IsPassable)
            {
                if (x.IsPassable)
                {

                }
                if (y.IsPassable)
                {

                }
                else
                {
                    x.SetBackPosition();
                    y.SetBackPosition();
                }
            }
            else if (x.IsKillable && y.IsKillable)
            {
                if (x.IsKillable)
                {

                }
                if (y.IsKillable)
                {

                }
                else
                {
                    x.SetBackPosition();
                    y.SetBackPosition();
                }
            }
        }

        public static void RemoveObject(ICollideableObject co)
        {
            if(co.IsDestructable)
            {
                PurgeFromWorld(co);
            }
        }

        public static void ObtainObject(ICollideableObject co)
        {

        }

        public static void MoveObject(ICollideableObject co)
        {

        }

        public static void Kill(ICollideableObject co)
        {

        }



        public static void PurgeFromWorld(ICollideableObject co)
        {
            for (int i = 0; i < ListHandler.GetInstance().GetAllCollideables().Count; i++)
            {
                if(co == ListHandler.GetInstance().GetAllCollideables().ElementAt(i))
                {
                    ListHandler.GetInstance().GetAllCollideables().RemoveAt(i);
                }
            }
        }
    }
}
