using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectSnake
{
    class Program
    {
        //All this class is doing is running the gameloop. 

        //Init() loads the world and assets.
        //Update() runs the gamelogic and redraws sprites.
        //End() runs when the game is finished. This will include finishing a level.
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Game gGame = new Game();
            
            gGame.Init();
            gGame.Update();
            gGame.End();
            
        }
    }
    
}
