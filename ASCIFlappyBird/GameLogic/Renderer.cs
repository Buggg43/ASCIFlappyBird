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
        public void DrawPillars(Board board, List<Pillar> pillars, int scrollOffset)
        {
            // Pole gry (spójne z DrawFrame)
            int playLeft = 1;
            int playRight = board.WindowWidth - board.MarginX - 1;
            int playTop = 1;
            int playBottom = board.WindowHeight - board.MarginY - 1;

            const int pillarWidth = 2;

            foreach (var p in pillars)
            {
                int screenX = p.WorldX - scrollOffset;

                if (screenX > playRight || screenX + pillarWidth - 1 < playLeft)
                    continue;

                int gapTop = (int)Math.Floor(p.GapCenterY - p.GapHeight / 2.0);
                int gapBottom = (int)Math.Ceiling(p.GapCenterY + p.GapHeight / 2.0);

                gapTop = Math.Max(gapTop, playTop);
                gapBottom = Math.Min(gapBottom, playBottom + 1);

                for (int xCol = screenX; xCol < screenX + pillarWidth; xCol++)
                {
                    if (xCol < playLeft || xCol > playRight) continue;

                    for (int y = playTop; y <= gapTop - 1; y++)
                    {
                        if (TrySetCursorPosition(xCol, y)) Console.Write('|');
                    }
                    for (int y = gapBottom; y <= playBottom; y++)
                    {
                        if (TrySetCursorPosition(xCol, y)) Console.Write('|');
                    }

                    int colHeight = playBottom - playTop + 1;
                    for (int y = playTop; y <= playBottom; y++)
                    {
                        if (TrySetCursorPosition(xCol+2, y) && xCol < playRight-1) Console.Write(' ');
                        if (TrySetCursorPosition(xCol , y) && xCol-1 < playLeft) Console.Write(new string(' ',pillarWidth));
                    }

                }
            }
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
