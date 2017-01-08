using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSnake
{
    //The class with all logic. It updates positions and draws the sprites.
    class Game
    {
        Player player;
        Treasure treasure;
        Random rnd = new Random();
        List<Potion> potionList = new List<Potion>();
        List<Treasure> treasureList = new List<Treasure>();
        List<Wall> wallList = new List<Wall>();
        List<Stone> stoneList = new List<Stone>();
        List<Monster> monsterList = new List<Monster>();

        public bool gameIsRunning = true;
        public int AMOUNT_OF_COLS;
        public int AMOUNT_OF_ROWS;

        //Initiates assets.
        public void Init()
        {
            InitWorld();
        }

        //The "real" gameloop. 
        //If the player collides with treasure or Enemy then the game will end.
        public void Update()
        {
            while (gameIsRunning)
            {
                MoveCharacterPos();
                CheckCollisions();
                Draw();
            }
        }

        //Runs the ending function. Ends the game or level.
        public void End()
        {
            Console.WriteLine("Game over! Starta om applikationen för att köra igen");
            Console.ReadLine();

            //Selfnote: I senare stadie kan man köra om Init(); och sätta gameIsRunning boolen till true om man
            // vill starta om spelet utan att stänga fönstret :D!
        }
        
        //The whole logic for moving a creature. Player and Monster alike. It wait's for input from player.
        public void MoveCharacterPos()
        {
            Console.SetCursorPosition(player.posX, player.posY);
            ConsoleKey input = Console.ReadKey().Key;

            foreach(Monster m in monsterList)
            {
                m.setPrevPositionX(m.posX);
                m.setPrevPositionY(m.posY);
            }

            player.setPrevPositionX(player.posX);
            player.setPrevPositionY(player.posY);

            switch (input)
            {
                case ConsoleKey.LeftArrow:
                    Console.SetCursorPosition(player.posX, player.posY);
                    Console.Write(" ");
                    player.posX -= 1;
                    break;
                case ConsoleKey.RightArrow:
                    Console.SetCursorPosition(player.posX, player.posY);
                    Console.Write(" ");
                    player.posX += 1;
                    break;
                case ConsoleKey.UpArrow:
                    Console.SetCursorPosition(player.posX, player.posY);
                    Console.Write(" ");
                    player.posY -= 1;
                    break;
                case ConsoleKey.DownArrow:
                    Console.SetCursorPosition(player.posX, player.posY);
                    Console.Write(" ");
                    player.posY += 1;
                    break;
                case ConsoleKey.Q:
                    gameIsRunning = false;
                    break;
            }
            if(input ==ConsoleKey.LeftArrow || 
                input == ConsoleKey.RightArrow || 
                input == ConsoleKey.UpArrow||
                input == ConsoleKey.DownArrow)
            {
                foreach (Monster m in monsterList)
                {
                    int tempValue = rnd.Next(1, 5);
                    switch (tempValue)
                    {
                        case 1:
                            Console.SetCursorPosition(m.posX, m.posY);
                            Console.Write(" ");
                            m.posX -= 1;
                            break;
                        case 2:
                            Console.SetCursorPosition(m.posX, m.posY);
                            Console.Write(" ");
                            m.posX += 1;
                            break;
                        case 3:
                            Console.SetCursorPosition(m.posX, m.posY);
                            Console.Write(" ");
                            m.posY -= 1;
                            break;
                        case 4:
                            Console.SetCursorPosition(m.posX, m.posY);
                            Console.Write(" ");
                            m.posY += 1;
                            break;
                    }
                }
            }
        }

        //The collisioncheck, this function runs after the player and monsters has changed their position.
        //It checks everything in the world.

        //ToFix: Give only moving creatures a function for the collisioncheck. Helps readability.
        public void CheckCollisions()
        {
            //To fix: Player and monster and collide when there is something unpassable on the same
            // spot they change position to.

            foreach (Treasure t in treasureList)
            {
                if (hasCollided(player.posX, player.posY, t.posX, t.posY))
                {
                    gameIsRunning = false;
                }
            }

            foreach (Potion p in potionList)
            {
                if (hasCollided(player.posX, player.posY, p.posX, p.posY))
                {

                }
            }

            foreach (Monster m in monsterList)
            {
                //Tofix: Monsters that collide with the treasure or a potion writes over it when passing.
                
                if (hasCollided(player.posX, player.posY, m.posX, m.posY))
                {
                    gameIsRunning = false;
                }
            }

            foreach (Wall w in wallList)
            {
                foreach (Monster m in monsterList)
                {
                    if (hasCollided(m.posX, m.posY, w.posX, w.posY))
                    {
                        m.posX = m.prevPosX;
                        m.posY = m.prevPosY;
                    }
                }

                if (hasCollided(player.posX, player.posY, w.posX, w.posY))
                {
                    player.posX = player.prevPosX;
                    player.posY = player.prevPosY;
                }
            }

            foreach (Stone s in stoneList)
            {
                foreach(Monster m in monsterList)
                {
                    if (hasCollided(m.posX, m.posY, s.posX, s.posY))
                    {
                        m.posX = m.prevPosX;
                        m.posY = m.prevPosY;
                    }
                }
                if (hasCollided(player.posX, player.posY, s.posX, s.posY))
                {
                    player.posX = player.prevPosX;
                    player.posY = player.prevPosY;
                }
            }
        }

        //Redraws the world after player has made an input.
        public void Draw()
        {
            Console.SetCursorPosition(player.posX, player.posY);
            Console.Write("@");
            foreach(Monster m in monsterList)
            {
                Console.SetCursorPosition(m.posX, m.posY);
                Console.Write("M");
            }
            
        }
        
        //Initiating the world from file. 
        //Future: Add more levels and load them seperately after game is finished etc.
        public void InitWorld()
        {
            string world;
            try
            {
                world = System.IO.File.ReadAllText(@"..\..\Levels\Level1.txt");
            }
            catch
            {
                Console.Write("Level not found...");
                throw new System.IO.FileNotFoundException();
            }
            
            DefineSymbolsAndPos(world);
            
        }

        //Adds an object for the sprites. 
        //This defines the sprites, gives them an object with positions etc.

        public void DefineSymbolsAndPos(string world)
        {
            int indexX = 0;
            int indexY = 0;
            foreach (char c in world)
            {
                indexX += 1;
                switch (c)
                {
                    case '@':
                        player = new Player();
                        player.setPositionX(indexX);
                        player.setPositionY(indexY);
                        break;
                    case 'O':
                        Stone tempStone = new Stone();
                        tempStone.posX = indexX;
                        tempStone.posY = indexY;
                        stoneList.Add(tempStone);
                        break;
                    case 'P':
                        Potion tempPotion = new Potion();
                        tempPotion.posX = indexX;
                        tempPotion.posY = indexY;
                        potionList.Add(tempPotion);
                        break;
                    case '?':
                        Treasure tempTreasure = new Treasure();
                        tempTreasure.posX = indexX;
                        tempTreasure.posY = indexY;
                        treasureList.Add(tempTreasure);
                        break;
                    case 'M':
                        Monster tempMonster = new Monster();
                        tempMonster.setPositionX(indexX);
                        tempMonster.setPositionY(indexY);
                        monsterList.Add(tempMonster);
                        break;
                    case '#':
                        Wall tempWall = new Wall();
                        tempWall.posX = indexX;
                        tempWall.posY = indexY;
                        wallList.Add(tempWall);
                        break;
                    default:
                        break;
                }

                if (c == '\n')
                {
                    indexX = 0;
                    indexY += 1;
                }
                else if (c == '\r') { }
                else
                {
                    Console.SetCursorPosition(indexX, indexY);
                    Console.Write(c);
                }
            }

            AMOUNT_OF_COLS = indexX;
            AMOUNT_OF_ROWS = indexY;
        }

        //Function to check if sprites collide with each other (Has the same position)
        public bool hasCollided(int element1posX, int element1posY, int element2posX, int element2posY)
        {
            if(element1posX == element2posX && element1posY == element2posY)
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
