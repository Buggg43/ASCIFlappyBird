using ASCIFlappyBird.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                }
                Thread.Sleep(20);
            }
        }
    }
}
