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
                    if (k.Key == ConsoleKey.Escape && !GameConfig.ShowMenu)
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
                        GameConfig.ShowMenu = false;
                        Console.Clear();
                        switch (GameConfig.SelectedMenu)
                        {
                            //Start Game
                            case 0:
                                GameConfig.ShowMenu = false;
                                GameConfig.ShowGame = true;
                                break;
                            //Sound Panel
                            case 1:
                                GameConfig.ShowMenu = false;
                                GameConfig.ShowSoundPanel = true;
                                break;
                            //Leader Board
                            case 2:
                                GameConfig.ShowMenu = false;
                                GameConfig.ShowSoundPanel = true;
                                break;
                            //Controls
                            case 3:
                                GameConfig.ShowMenu = false;
                                GameConfig.ShowAbout = true;
                                break;
                            //Exit Game
                            case 4:
                                GameConfig.ShowMenu = false;
                                GameConfig.ExitGame = true;
                                break;
                        }
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
                Thread.Sleep(10);
            }
        }
    }
}