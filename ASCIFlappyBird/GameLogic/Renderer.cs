using ASCIFlappyBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIFlappyBird.GameLogic
{
    public class Renderer
    {

        public void DrawFrame(Board board)
        {
            var boardWidth = board.WindowWidth - board.MarginX;
            var boardHeight = board.WindowHeight - board.MarginY;
            TrySetCursorPosition(0, 0);
                Console.Write(new string('#', boardWidth));
            for (int y = 0; y <= boardHeight; y++)
            {
                TrySetCursorPosition(0, y);
                Console.Write('#');
                TrySetCursorPosition(boardWidth, y);
                Console.Write('#');
            }
            TrySetCursorPosition(0, boardHeight);
            Console.Write(new string('#', boardWidth));
        }
        public void DrawGameOver(int score)
        {
            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Your Score: {score}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        public bool TrySetCursorPosition(int x, int y)
        {
            if (x >= 0 && x < Console.BufferWidth && y >= 0 && y < Console.BufferHeight)
            {                
                Console.SetCursorPosition(x, y);
                return true;
            }

            return false;
        }
    }
}
