using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ASCIFlappyBird.Models
{
    public class Board
    {
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }
        public int MarginX { get; set; } = 1;
        public int MarginY { get; set; } = 2;
        public bool Collision { get; set; } = false;
        public bool WindowResized { get; set; } = false;

        public (int x, int y) Center => (WindowWidth / 2, WindowHeight / 2);


        public void WindowChanged(Board board)
        {
            int newW = Console.WindowWidth;
            int newH = Console.WindowHeight;
            if (board.WindowWidth != newW || board.WindowHeight != newH)
            {
                Console.Clear();
                Console.Write("\x1b[3J"); // escape sequence to clear scrollback buffer
                Console.SetWindowPosition(0, 0);
                Console.SetCursorPosition(0, 0);
                Console.SetBufferSize(newW, newH);
                WindowResized = true;
                board.WindowWidth = newW;
                board.WindowHeight = newH;
            }
            else
            {
                WindowResized = false;
            }
        }

    }
}
