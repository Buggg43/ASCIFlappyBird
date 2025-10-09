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
        const int pillarWidth = 2;
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
                var scrollOffset = (int)Math.Floor(worldScrollCells);
                _bird.VerticalPosition = _board.Center.y;
                _bird.Position = (birdX, _board.Center.y);

                if (!spawnPrimed)
                {
                    nextSpawnWorldX = scrollOffset + _board.FirstSpawnScreenX;
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
            }
            while (worldAccumulatorMs >= WorldDtMs)
            {
                worldAccumulatorMs -= WorldDtMs;

                // 1) scroll
                worldScrollCells += worldSpeedCellsPerSecond * (WorldDtMs / 1000.0);
                var scrollOffset = (int)Math.Floor(worldScrollCells);

                int removedCount = _pillars.RemoveAll(p => p.WorldX + pillarWidth - 1 < scrollOffset + /* playLeft = */ 1);

                if (_pillars.Count < 5) removedCount ++;

                for (int i = 0; i < removedCount; i++)
                {
                    int gapHeight = rng.Next(minGapY, maxGapY + 1);
                    int gapHalf = gapHeight / 2;
                    int gapCenterY = rng.Next(_board.GameWindowTop + gapHalf, playBottom - gapHalf);

                    _pillars.Add(new Pillar
                    {
                        WorldX = (int)nextSpawnWorldX,
                        GapCenterY = gapCenterY,
                        GapHeight = gapHeight
                    });

                    nextSpawnWorldX += rng.Next(24, 33);
                }

                // 4) draw (raz na klatkę świata)
                _renderer.DrawPillars(_board, _pillars, scrollOffset);
            }


            if (_board.Collision)
            {
                break;
            }
            
            Thread.Sleep(50);
        }
    }
}

