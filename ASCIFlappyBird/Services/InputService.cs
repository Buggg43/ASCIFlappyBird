using ASCIFlappyBird.Config;

namespace ASCIFlappyBird.Services
{
    public class InputService
    {
        public static void InputListener(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var k = Console.ReadKey(true);
                    if (k.Key == ConsoleKey.P)
                        GameConfig.Paused = !GameConfig.Paused;
                    if (k.Key == ConsoleKey.Escape)
                        GameConfig.ShowMenu = !GameConfig.ShowMenu;
                    if (k.Key == ConsoleKey.UpArrow && GameConfig.ShowMenu == true)
                    {
                        GameConfig.SelectedMenu--;
                        GameConfig.UpdateCursor = true;
                    }
                    if (k.Key == ConsoleKey.DownArrow && GameConfig.ShowMenu == true)
                    {
                        GameConfig.SelectedMenu++;
                        GameConfig.UpdateCursor = true;
                    }
                }
                Thread.Sleep(20);
            }
        }
    }
}