using ASCIFlappyBird.Config;

namespace ASCIFlappyBird.Services
{
    public class InputHandler
    {
        public void HandleKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.P:
                    GameConfig.Paused = !GameConfig.Paused;
                    break;
                case ConsoleKey.M:
                    if (!GameConfig.Muted)
                    {
                        GameConfig.PreviousVolume = GameConfig.CurentVolume;
                        GameConfig.CurentVolume = 0f;
                        GameConfig.Muted = true;
                    }
                    else
                    {
                        GameConfig.CurentVolume = GameConfig.PreviousVolume;
                        GameConfig.Muted = false;
                    }
                    break;
                case ConsoleKey.Escape:
                    if (!GameConfig.ShowMenu)
                        GameConfig.ShowMenu = !GameConfig.ShowMenu;
                    break;
                case ConsoleKey.UpArrow:
                    if (GameConfig.ShowMenu)
                    {
                        GameConfig.SelectedMenu--;
                        GameConfig.UpdateCursor = true;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (GameConfig.ShowMenu)
                    {
                        GameConfig.SelectedMenu++;
                        GameConfig.UpdateCursor = true;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (GameConfig.ShowSoundPanel)
                    {
                        GameConfig.CurentVolume = Math.Clamp(GameConfig.CurentVolume + 0.01f, 0f, 1f);
                        if (GameConfig.Muted && GameConfig.CurentVolume > 0f) GameConfig.Muted = false;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (GameConfig.ShowSoundPanel)
                    {
                        GameConfig.CurentVolume = Math.Clamp(GameConfig.CurentVolume - 0.01f, 0f, 1f);
                        if (GameConfig.CurentVolume == 0f) GameConfig.Muted = true;
                    }
                    break;
                case ConsoleKey.Enter:
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
                    break;
                default:
                    break;
            }
        }
    }
}
