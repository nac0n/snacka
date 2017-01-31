using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSnake
{
    public class ListHandler
    {
        //singleton class
        private static ListHandler theOne;

        private List<IObject> ObjectList;
        private List<Creature> CreatureList;
        private List<Potion> potionList;
        private List<Treasure> treasureList;
        private List<Wall> wallList;
        private List<Stone> stoneList;
        private List<Monster> monsterList;

        private ListHandler()
        {
            ObjectList = new List<IObject>();
            CreatureList = new List<Creature>();
            potionList = new List<Potion>();
            treasureList = new List<Treasure>();
            wallList = new List<Wall>();
            stoneList = new List<Stone>();
            monsterList = new List<Monster>();
        }

        public static ListHandler GetInstance()
        {
            if (theOne == null)
            {
                theOne = new ListHandler();
            }
            return theOne;
        } 

        public List<IObject> GetObjects()
        {
            return ObjectList;
        }
        public List<Creature> GetCreatures()
        {
            return CreatureList;
        }
        public List<Potion> GetPotions()
        {
            return potionList;
        }
        public List<Treasure> GetTreasures()
        {
            return treasureList;
        }
        public List<Wall> GetWalls()
        {
            return wallList;
        }
        public List<Stone> GetStones()
        {
            return stoneList;
        }
        public List<Monster> GetMonsters()
        {
            return monsterList;
        }

    }
}
