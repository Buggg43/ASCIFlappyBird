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
            TrySetCursorPosition(0, 0);
            Console.Write(new string('#', board.WindowWidth));
            for (int y = 0; y <= board.BoardHeight; y++)
            {
                TrySetCursorPosition(0, y);
                Console.Write('#');
                TrySetCursorPosition(board.BoardWidth, y);
                Console.Write('#');
            }
            TrySetCursorPosition(0, board.BoardHeight);
            Console.Write(new string('#', board.BoardWidth));
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
            foreach (var p in pillars)
            {
                int screenX = p.WorldX - scrollOffset;

                if (screenX > board.GameWindowRight || screenX + p.Width - 1 < board.GameWindowLeft)
                    continue;

                int gapTop = (int)Math.Floor(p.GapCenterY - p.GapHeight / 2.0);
                int gapBottom = (int)Math.Ceiling(p.GapCenterY + p.GapHeight / 2.0);

                gapTop = Math.Max(gapTop, board.GameWindowTop);
                gapBottom = Math.Min(gapBottom, board.GameWindowBottom + 1);

                for (int xCol = screenX; xCol < screenX + p.Width; xCol++)
                {
                    if (xCol < board.GameWindowLeft || xCol > board.GameWindowRight) continue;

                    for (int y = board.GameWindowTop; y <= gapTop - 1; y++)
                    {
                        if (TrySetCursorPosition(xCol, y)) Console.Write('|');
                        if (TrySetCursorPosition(xCol + 2, y) && xCol < board.GameWindowRight - 1) Console.Write(' ');
                    }
                    for (int y = gapBottom; y <= board.GameWindowBottom; y++)
                    {
                        if (TrySetCursorPosition(xCol, y)) Console.Write('|');
                        if (TrySetCursorPosition(xCol + 2, y) && xCol < board.GameWindowRight - 1) Console.Write(' ');
                    }
                }
                if (TrySetCursorPosition(board.GameWindowLeft, board.GameWindowTop) && screenX - 1 < board.GameWindowLeft)
                {

                    for (int y = board.GameWindowTop; y <= board.GameWindowBottom; y++)
                        Console.Write(new string(' ', board.WindowWidth));
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
/*                    for (int y = board.GameWindowTop; y <= board.GameWindowBottom; y++)
                    {
                        if (TrySetCursorPosition(xCol + 2, y) && xCol < board.GameWindowRight - 1) Console.Write(' ');
                        if (TrySetCursorPosition(xCol, y) && xCol - 1 < board.GameWindowLeft) Console.Write(new string(' ', board.WindowWidth));
                    }*/
