using ASCIFlappyBird.Config;
using ASCIFlappyBird.Models;

namespace ASCIFlappyBird.GameLogic
{
    public class Renderer
    {
        int item = 0;
        private int collorToPick => (item + GameConfig.ColorOffSet) % GameConfig.Colors.Length;

        public void DrawFrame(Board board)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            TrySetCursorPosition(0, 0);
            Console.Write(new string(' ', board.WindowWidth));
            for (int y = 0; y <= board.BoardHeight; y++)
            {
                TrySetCursorPosition(0, y);

                Console.Write(' ');
                TrySetCursorPosition(board.BoardWidth, y);
                Console.Write(' ');
            }

            TrySetCursorPosition(0, board.BoardHeight);
            Console.Write(new string(' ', board.BoardWidth));
            Console.ResetColor();
            Thread.Sleep(1000);
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

                if (screenX > board.GameWindowRight || screenX + p.Width < board.GameWindowLeft)
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
                        if (TrySetCursorPosition(xCol, y))
                        {
                            Console.Write("|" + "\x1b[32m");

                        }
                        if (TrySetCursorPosition(xCol + 2, y) && xCol < board.GameWindowRight - 1) Console.Write(" ");
                    }
                    for (int y = gapBottom; y <= board.GameWindowBottom; y++)
                    {
                        if (TrySetCursorPosition(xCol, y)) Console.Write('|' + "\x1b[32m");
                        if (TrySetCursorPosition(xCol + 2, y) && xCol < board.GameWindowRight - 1) Console.Write(' ');
                    }
                }
            }
        }
        public void RemovePillar(Board board, List<Pillar> pillars, int scrollOffset, ref int count)
        {
            var pilarToRemove = pillars.FirstOrDefault(p => p.WorldX - scrollOffset < board.GameWindowLeft);
            if (pilarToRemove != null)
            {
                var screenX = pilarToRemove.WorldX - scrollOffset;
                for (int y = board.GameWindowTop; y <= board.BoardHeight - 1; y++)
                {
                    if (TrySetCursorPosition(screenX + 1, y) && screenX < board.GameWindowRight - 1) Console.Write(new string(' ', pilarToRemove.Width));
                }
                Console.Write(count);
            }
        }
        public void DrawBird(Bird Bird)
        {
            string sprite = Bird.VerticalSpeed switch
            {
                < -1 => "<('^)", // UP
                > 1 => "<('_)",  // down
                _ => "<(')>",    // equil.
            };
            TrySetCursorPosition(Bird.Position.x - Bird.Width, Bird.Position.y);
            Console.Write(sprite);
        }
        public void RemoveBird(Bird bird, (int x, int y) lastPosition)
        {
            if (lastPosition.x > 1)
            {
                TrySetCursorPosition(lastPosition.x, lastPosition.y);
                Console.Write(new string(' ', bird.Width));
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
        public void DrawMenu(Board board)
        {
            var menuStartX = (int)Math.Round(board.Center.x - 21.0f);
            var menuEndX = (int)Math.Round(board.Center.x + 21.0f);

            var menuStartY = (int)Math.Round(board.Center.y - 9.0f);
            var menuEndY = (int)Math.Round(board.Center.y + 9.0f);

            var menuLength = menuEndX - menuStartX;

            TrySetCursorPosition(menuStartX, menuStartY);
            Console.BackgroundColor = GameConfig.Colors[collorToPick];
            Console.Write(new string(' ', menuLength));
            for (int i = menuStartY + 1; i < menuEndY; i++)
            {

                item++;
                Console.BackgroundColor = GameConfig.Colors[collorToPick];
                TrySetCursorPosition(menuStartX, i);
                Console.Write("  ");
                if (!GameConfig.MenuDrawn)
                {
                    Console.ResetColor();
                    Console.Write(new string(' ', menuLength - 2));
                    Console.BackgroundColor = GameConfig.Colors[collorToPick];
                }
                TrySetCursorPosition(menuEndX - 2, i);
                Console.Write("  ");
            }
            item++;
            Console.BackgroundColor = GameConfig.Colors[collorToPick];
            TrySetCursorPosition(menuStartX, menuEndY);
            Console.Write(new string(' ', menuLength));


            if (!GameConfig.MenuDrawn || board.WindowResized)
            {
                int menuOptionOffsetX = 6;
                int menuOptionOffsetY = 3;
                int menuOptionStartX = menuStartX + menuOptionOffsetX;
                int menuOptionStartY = menuStartY + menuOptionOffsetY;
                Console.ResetColor();
                GameConfig.MenuDrawn = true;

                Console.ForegroundColor = ConsoleColor.DarkRed;
                //centering text

                int menuCenter = menuStartX + (menuLength / 2);
                int startingTextPosition = 0;
                foreach (var item in GameConfig.MenuOptions)
                {
                    startingTextPosition = menuCenter - (item.Length / 2);
                    TrySetCursorPosition(startingTextPosition, menuOptionStartY);
                    Console.Write(item);
                    menuOptionStartY += menuOptionOffsetY;
                }
                //i know it's not correctly centered :(
            }
            if (GameConfig.UpdateCursor || board.WindowResized)
            {
                int menuOptionOffsetX = 6;
                int menuOptionOffsetY = 3;
                int menuOptionStartX = menuStartX + menuOptionOffsetX;
                int menuOptionStartY = menuStartY + menuOptionOffsetY;
                Console.ResetColor();
                GameConfig.UpdateCursor = false;
                menuOptionStartY = menuStartY + menuOptionOffsetY;

                TrySetCursorPosition(menuOptionStartX + menuOptionOffsetX, menuOptionStartY + (menuOptionOffsetY * GameConfig.SelectedMenu) + 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(new string('-', menuLength - (menuOptionOffsetX * 4)));

                Console.ResetColor();

                if (GameConfig.PrevSelectedMenu != GameConfig.SelectedMenu)
                {
                    TrySetCursorPosition(menuOptionStartX + menuOptionOffsetX, menuOptionStartY + (menuOptionOffsetY * GameConfig.PrevSelectedMenu) + 1);
                    Console.Write(new string(' ', menuLength - (menuOptionOffsetX * 4)));
                    GameConfig.PrevSelectedMenu = GameConfig.SelectedMenu;
                }
            }
            Console.ResetColor();
            GameConfig.ColorOffSet++;
            Thread.Sleep(120);
        }
        public void DrawPause(Board board)
        {
            var pauseStartX = (int)Math.Round(board.Center.x - 22.0f);
            var pauseEndX = (int)Math.Round(board.Center.x + 22.0f);

            var pauseStartY = (int)Math.Round(board.Center.y - 2.5f);
            var pauseEndY = (int)Math.Round(board.Center.y + 2.5f);

            var pauseLength = pauseEndX - pauseStartX + 1;

            TrySetCursorPosition(pauseStartX, pauseStartY);
            Console.Write(new string('#', pauseLength));
            for (int i = pauseStartY + 1; i < pauseEndY; i++)
            {
                TrySetCursorPosition(pauseStartX, i);
                Console.Write(new string(' ', pauseLength));
                TrySetCursorPosition(pauseStartX, i);
                Console.Write("#");
                TrySetCursorPosition(pauseEndX, i);
                Console.Write("#");
            }
            TrySetCursorPosition(pauseStartX, pauseEndY);
            Console.Write(new string('#', pauseLength));


            //PAUSE DRAW
            TrySetCursorPosition(board.Center.x - 3, board.Center.y);
            Console.Write("PAUSED");
        }
        public void DrawSoundPanel(Board board)
        {
            string menuTitle = "Sound Panel";
            TrySetCursorPosition(board.Center.x - (menuTitle.Length / 2), board.Center.y - 2);
            Console.Write(menuTitle);

            int progresBarStartX = board.Center.x - 50;
            int progres = Convert.ToInt32(GameConfig.CurentVolume * 100);
            DrawProgresBar((progresBarStartX, board.Center.y), progres, 100);
        }
        public void DrawProgresBar((int x, int y) progresBarPosition, int progres, int progresBarLength)
        {

            TrySetCursorPosition(progresBarPosition.x - 5, progresBarPosition.y);
            Console.Write($"{progres}% [");
            TrySetCursorPosition(progresBarPosition.x + progresBarLength + 1, progresBarPosition.y);
            Console.Write("]");

            int progresLength = progresBarPosition.x + progres;
            int maxProgresLength = progresBarPosition.x + progresBarLength;

            Console.ForegroundColor = ConsoleColor.Red;
            TrySetCursorPosition(progresBarPosition.x, progresBarPosition.y);
            Console.Write(new string('\u25A0', progres));


            Console.ForegroundColor = ConsoleColor.Black;
            TrySetCursorPosition(progresLength, progresBarPosition.y);
            Console.Write(new string('\u25A0', maxProgresLength - progresLength));


            Console.ResetColor();
        }
        public void DrawAbout(Board board)
        {

        }
    }
}