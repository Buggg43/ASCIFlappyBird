using ASCIFlappyBird.GameLogic;
using ASCIFlappyBird.Models;
using ASCIFlappyBird.Services;
using System.Diagnostics;

public class Program
{
    public static void Main()
    {
        Console.CursorVisible = true;
        BirdService _birdService = new BirdService();
        Renderer _renderer = new Renderer();
        Bird _bird = new Bird();
        Board _board = new Board();

        var sw = Stopwatch.StartNew();
        double birdAccumulatorMs = 0;
        const double BirdDtMs = 16;
        int score = 0;
        while (true)
        {
            var elapsedMs = sw.Elapsed.TotalMilliseconds;
            sw.Restart();
            birdAccumulatorMs += elapsedMs;
            if(birdAccumulatorMs >= BirdDtMs)
            {
                birdAccumulatorMs -= BirdDtMs;
                _renderer.TrySetCursorPosition(_board.Center.x, _board.Center.y);
                Console.Write((score++)/BirdDtMs);
                _renderer.TrySetCursorPosition(_board.Center.x, _board.Center.y + 1);
                Console.Write(score++);
            }
            _board.WindowChanged(_board);
            if (_board.WindowResized)
            {
                _board.WindowResized = false;
                _renderer.DrawFrame(_board);
            }
            if (_board.Collision)
            {
                break;
            }

            
            

            Thread.Sleep(50);
        }
    }
}