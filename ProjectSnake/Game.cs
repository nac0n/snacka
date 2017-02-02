using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectSnake
{
    //The class with all logic. It updates positions and draws the sprites.
    class Game
    {

        private Player player;
        private Stairs stairs;
        private Random rnd = new Random();
        private Monster encounteredMonster;

        public bool gameIsRunning = true;
        public bool nextLevel = false;
        public int AMOUNT_OF_COLS;
        public int AMOUNT_OF_ROWS;

        //Initiates assets.
        public void Init()
        {
            InitWorld();
        }

        //The logic. 
        //If the player collides with Enemy then the game will end.
        //If player collides with Stairs, Next level will Load.
        public void Update()
        {
            while (gameIsRunning == true && nextLevel == false)
            {
                MoveCharacterPos();
                CollisionHandler.CollisionCheck();
                Draw();
            }
        }

        //Runs the ending function. Ends the game or level.
        public void End()
        {
            if (gameIsRunning == false)
            {
                Console.WriteLine("Game over! Starta om applikationen för att köra igen");
                Console.ReadLine();
            }
            else if (nextLevel == true)
            {
                Console.WriteLine("Bra jobbat! Nästa bana! Tryck valfri knapp för att starta!");
                Console.ReadLine();
            }

            //Selfnote: I senare stadie kan man köra om Init(); och sätta gameIsRunning boolen till true om man
            // vill starta om spelet utan att stänga fönstret :D!
        }
        
        //The whole logic for moving a creature. Player and Monster alike. It wait's for input from player.
        public void MoveCharacterPos()
        {
            Console.SetCursorPosition(player.posX, player.posY);
            ConsoleKey input = Console.ReadKey().Key;

            //foreach(Monster m in monsterList)
            //{
            //    m.setPrevPositionX(m.posX);
            //    m.setPrevPositionY(m.posY);
            //}

            player.prevPosX = player.posX;
            player.prevPosY = player.posY;

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
                foreach (Monster m in ListHandler.GetInstance().GetMonsters())
                {
                    int tempValue = rnd.Next(1, 5);
                    m.prevPosX = m.posX;
                    m.prevPosY = m.posY;

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
        
        //Redraws the world after player has made an input.
        public void Draw()
        {
            Console.SetCursorPosition(player.posX, player.posY);
            Console.Write(player.sprite);
            foreach(Monster m in ListHandler.GetInstance().GetMonsters())
            {
                Console.SetCursorPosition(m.posX, m.posY);
                Console.Write(m.sprite);
            }
            DrawMenu();
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
            DrawMenu();
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
                        player.posX = indexX;
                        player.posY = indexY;
                        ListHandler.GetInstance().GetAllCollideables().Add(player);
                        break;
                    case 'O':
                        Stone tempStone = new Stone();
                        tempStone.posX = indexX;
                        tempStone.posY = indexY;
                        ListHandler.GetInstance().GetAllCollideables().Add(tempStone);
                        ListHandler.GetInstance().GetStones().Add(tempStone);
                        break;
                    case 'P':
                        Potion tempPotion = new Potion();
                        tempPotion.posX = indexX;
                        tempPotion.posY = indexY;
                        ListHandler.GetInstance().GetAllCollideables().Add(tempPotion);
                        ListHandler.GetInstance().GetPotions().Add(tempPotion);
                        break;
                    case '?':
                        Treasure tempTreasure = new Treasure();
                        tempTreasure.posX = indexX;
                        tempTreasure.posY = indexY;
                        ListHandler.GetInstance().GetAllCollideables().Add(tempTreasure);
                        ListHandler.GetInstance().GetTreasures().Add(tempTreasure);
                        break;
                    case '/':
                        stairs = new Stairs();
                        stairs.posX = indexX;
                        stairs.posY = indexY;
                        ListHandler.GetInstance().GetAllCollideables().Add(stairs);
                        break;
                    case 'M':
                        Monster tempMonster = new Monster();
                        tempMonster.posX = indexX;
                        tempMonster.posY = indexY;
                        ListHandler.GetInstance().GetAllCollideables().Add(tempMonster);
                        ListHandler.GetInstance().GetMonsters().Add(tempMonster);
                        break;
                    case '#':
                        Wall tempWall = new Wall();
                        tempWall.posX = indexX;
                        tempWall.posY = indexY;
                        ListHandler.GetInstance().GetAllCollideables().Add(tempWall);
                        ListHandler.GetInstance().GetWalls().Add(tempWall);
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

        public void DrawMenu()
        {
            Console.SetCursorPosition(0, AMOUNT_OF_ROWS + 2);
            Console.WriteLine("Health:"  + player.health);
            Console.SetCursorPosition(0, AMOUNT_OF_ROWS + 3);
            Console.WriteLine("Level:" + player.level);

            if(encounteredMonster != null)
            {
                Console.SetCursorPosition(0, AMOUNT_OF_ROWS + 5);
                Console.WriteLine("Enemy Health:" + encounteredMonster.health);
            }
           
        }
        
    }
}
