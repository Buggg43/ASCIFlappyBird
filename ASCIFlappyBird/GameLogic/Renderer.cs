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
        public void DrawFrame(char[,] frameBuffer)
        {
            Console.Clear();
            int height = frameBuffer.GetLength(0);
            int width = frameBuffer.GetLength(1);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(frameBuffer[y, x]);
                }
                Console.WriteLine();
            }
        }
        public void DrawGameOver(int score)
        {
            Console.Clear();
            Console.WriteLine("Game Over!");
            Console.WriteLine($"Your Score: {score}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        public void DrawBird(Bird bird, char[,] frameBuffer)
        {
            for (int y = 0; y < bird.Height; y++)
            {
                for (int x = 0; x < bird.Width; x++)
                {
                    int drawX = bird.Position.x + x;
                    int drawY = bird.Position.y + y;
                    if (drawX >= 0 && drawX < frameBuffer.GetLength(1) && drawY >= 0 && drawY < frameBuffer.GetLength(0))
                    {
                        frameBuffer[drawY, drawX] = 'O';
                    }
                }
            }
        }
    }
}
