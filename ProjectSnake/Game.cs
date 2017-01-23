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
        private Player player;
        private Stairs stairs;
        private Random rnd = new Random();
        private Monster encounteredMonster;
        private CollisionChecker Collision = new CollisionChecker();
        private List<Potion> potionList = new List<Potion>();
        private List<Treasure> treasureList = new List<Treasure>();
        private List<Wall> wallList = new List<Wall>();
        private List<Stone> stoneList = new List<Stone>();
        private List<Monster> monsterList = new List<Monster>();

        public bool gameIsRunning = true;
        public bool nextLevel = false;
        public int AMOUNT_OF_COLS;
        public int AMOUNT_OF_ROWS;

        //Initiates assets.
        public void Init()
        {
            InitWorld();
        }

        //The "real" gameloop. 
        //If the player collides with Enemy then the game will end.
        public void Update()
        {
            while (gameIsRunning == true && nextLevel == false)
            {
                MoveCharacterPos();
                CheckCollisions();
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
            else
            {

            }
            

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


            //Stairs and Player collision

            
            foreach (Monster m in monsterList)
            {
                //Monster and Player collision
                if (Collision.HasCollided(player, m))
                {
                    encounteredMonster = m;
                    player.setPositionX(player.prevPosX);
                    player.setPositionY(player.prevPosY);
                    m.setPositionX(m.prevPosX);
                    m.setPositionY(m.prevPosY);

                    player.SetHealth(-m.damage);
                    m.SetHealth(-player.damage);
                    
                    //gameIsRunning = false;
                    if (m.isDead())
                    {
                        monsterList.Remove(m);
                        encounteredMonster = null;
                        break;
                    }
                    else if (player.isDead())
                    {
                        gameIsRunning = false;
                        break;
                    }
                }

                //Potion and Monster collision
                foreach (Potion p in potionList)
                {
                    if(Collision.HasCollided(m, p))
                    {
                        potionList.Remove(p);
                        break;
                    }
                }
            }

            foreach(Potion p in potionList)
            {
                //Potion and Player collision
                if(Collision.HasCollided(player, p))
                {
                    player.SetHealth(p.giveHP);
                }
            }

            foreach (Stone s in stoneList)
            {
                foreach(Wall w in wallList)
                {
                    if (Collision.HasCollided(w, s))
                    {
                        gameIsRunning = false;
                        //Sätter positionen till föregående position.
                        //s.posX = s.prevPosX;
                        //s.posY = s.prevPosY;
                    }
                }
            }

            foreach(Wall w in wallList)
            {
                if(Collision.HasCollided(player, w))
                {
                    player.posX = player.prevPosX;
                    player.posY = player.prevPosY;
                }

                foreach(Monster m in monsterList)
                {
                    if (Collision.HasCollided(m, w))
                    {
                        m.posX = m.prevPosX;
                        m.posY = m.prevPosY;
                    }
                }
            }

            foreach (Treasure t in treasureList)
            {
                if (Collision.HasCollided(player, t))
                {

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
                    case '/':
                        stairs = new Stairs();
                        stairs.posX = indexX;
                        stairs.posY = indexY;
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
