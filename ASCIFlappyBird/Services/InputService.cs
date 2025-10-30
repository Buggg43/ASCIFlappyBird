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
                    else if (k.Key == ConsoleKey.DownArrow && GameConfig.ShowMenu == true)
                    {
                        GameConfig.SelectedMenu++;
                        GameConfig.UpdateCursor = true;
                    }
                    if (k.Key == ConsoleKey.Enter && GameConfig.ShowMenu == true)
                    {
                        GameConfig.SelectedMenu++;
                        GameConfig.ShowMenu = false;
                    }
                    if (k.Key == ConsoleKey.RightArrow && GameConfig.ShowSoundPanel == true)
                    {
                        GameConfig.CurentVolume += 0.01f;
                    }
                    else if (k.Key == ConsoleKey.LeftArrow && GameConfig.ShowSoundPanel == true)
                    {
                        GameConfig.CurentVolume -= 0.01f;
                    }
                }
                Thread.Sleep(20);
            }
        }
    }
}