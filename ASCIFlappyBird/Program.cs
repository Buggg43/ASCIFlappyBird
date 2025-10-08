using ASCIFlappyBird.GameLogic;
using ASCIFlappyBird.Models;
using ASCIFlappyBird.Services;
using System.Diagnostics;
using System.Security.AccessControl;

public class Program
{
    public static void Main()
    {
        Console.Title = "ASCII Flappy Bird";
        Console.CursorVisible = false;
        Random rng = new Random();
        BirdService _birdService = new BirdService();
        Renderer _renderer = new Renderer();
        Board _board = new Board();
        Bird _bird = new Bird()
        {
            VerticalSpeed = 0,
            GravityAcceleration = 5,
        };
        bool birdCentered = false;
        var sw = Stopwatch.StartNew();
        List<Pillar> _pillars = new List<Pillar>();
        double nextSpawnWorldX = 0;

        double worldAccumulatorMs = 0;
        const double WorldDtMs = 16;
        double worldScrollCells = 0;
        double worldSpeedCellsPerSecond = 8;
        bool spawnPrimed = false;



        double birdAccumulatorMs = 0;
        const double BirdDtMs = 16;
        int score = 0;

        int minGapY = 5,maxGapY = 9;
        while (true)
        {
            var playLeft = _board.MarginX;
            var playRight = _board.WindowWidth - _board.MarginX - 1;
            var playWidth = playRight - playLeft;

            _board.WindowChanged(_board);
            if (_board.WindowResized)
            {
                _board.WindowResized = false;
                _renderer.DrawFrame(_board);
                var birdX = (int)Math.Round(playLeft + 0.25 * playWidth);
                _bird.VerticalPosition = _board.Center.y;
                _bird.Position = (birdX, _board.Center.y);
                if (!spawnPrimed)
                {
                    int boardWidth = (_board.WindowWidth - _board.MarginX) - 1 ;
                    int playW = boardWidth - playLeft;

                    double spawnFraction = 0.75;
                    int firstSpawnScreenX = _board.MarginX + (int)Math.Round(playW * spawnFraction);

                    int scrollOffset = (int)Math.Floor(worldScrollCells);
                    nextSpawnWorldX = scrollOffset + firstSpawnScreenX;

                    spawnPrimed = true;
                }

            }

            var elapsedMs = sw.Elapsed.TotalMilliseconds;
            sw.Restart();
            birdAccumulatorMs += elapsedMs;
            worldAccumulatorMs += elapsedMs;
            while (birdAccumulatorMs >= BirdDtMs)
            {
                birdAccumulatorMs -= BirdDtMs;
                if(Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        _birdService.MoveBird(_bird);
                    }
                }
                _birdService.ApplyGravity(_bird, _board, BirdDtMs / 1000.0);
                //Console.SetCursorPosition(_board.Center.x, _board.Center.y);
                //Console.Write($"Bird VP: {_bird.VerticalPosition} / VS: {_bird.VerticalSpeed}  "); 
            }
            while(worldAccumulatorMs >= WorldDtMs)
            {
                worldAccumulatorMs -= WorldDtMs;
                worldScrollCells += worldSpeedCellsPerSecond * (WorldDtMs / 1000.0);
                var scrollOffset = (int)Math.Floor(worldScrollCells);
                const int pillarWidth = 2;
                _pillars.RemoveAll(p => p.WorldX + pillarWidth - 1 < scrollOffset + playLeft);

                foreach (var p in _pillars )
                {
                    var screenX = p.WorldX - scrollOffset;
                    if(playLeft <= screenX && screenX <= playRight)
                    {
                        var gapTop = p.GapCenterY - (p.GapHeight / 2);
                        var gapBottom = p.GapCenterY + (p.GapHeight / 2);
                        var playTop = _board.MarginY + 1;
                        _renderer.DrawPillars(_board, _pillars, scrollOffset);
                        //Console.SetCursorPosition(screenX, Math.Max(playTop, gapTop - 1)); 
                        //Console.Write("^");
                    }
                }
                //Console.SetCursorPosition(_board.Center.x, _board.Center.y+2);
                //Console.Write($"World SC: {worldScrollCells} ");
            }
            while(nextSpawnWorldX <= worldScrollCells + playRight)
            {
                int gapHeight = rng.Next(minGapY, maxGapY + 1);
                int playTop = _board.MarginY + 1;
                int playBottom = _board.WindowHeight - _board.MarginY - 1;
                int gapHalf = gapHeight / 2;
                int gapCenterY = rng.Next(gapHalf + playTop, playBottom - gapHalf);
                _pillars.Add(new Pillar()
                {
                    WorldX = (int)nextSpawnWorldX,
                    GapCenterY = gapCenterY,
                    GapHeight = gapHeight
                });
                nextSpawnWorldX += rng.Next(24, 33);
                if (_pillars.Count > 0)
                {
                    
                    //Console.SetCursorPosition(2, _board.Center.y + 4);
                    //Console.Write($"Pillars: {_pillars.Count}, last X={_pillars[^1].WorldX:F1}   "); 
                }
            }
            
            if (_board.Collision)
            {
                break;
            }
            
            Thread.Sleep(50);
        }
    }
}

