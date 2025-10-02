using ASCIFlappyBird.GameLogic;
using ASCIFlappyBird.Models;
using ASCIFlappyBird.Services;

public class Program
{
    public static void Main()
    {
        Console.CursorVisible = true;
        BirdService _birdService = new BirdService();
        Renderer _renderer = new Renderer();
        Bird _bird = new Bird();

        Board _board = new Board();
        while (true)
        {
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