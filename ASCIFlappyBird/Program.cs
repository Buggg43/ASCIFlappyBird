using ASCIFlappyBird.Config;
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
            GravityAcceleration = 10,
        };
        (int x, int y) birdLastPosition = (0, 0);
        var sw = Stopwatch.StartNew();
        List<Pillar> _pillars = new List<Pillar>();
        double nextSpawnWorldX = 0;

        double worldAccumulatorMs = 0;
        double worldScrollCells = 0;
        double worldSpeedCellsPerSecond = 8;
        bool spawnPrimed = false;

        int count = 0;

        double birdAccumulatorMs = 0;
        const double BirdDtMs = 16;
        int minGapY = 5, maxGapY = 9;
        int lastScrollOffset = -1;
        while (true)
        {
            _board.WindowChanged(_board);
            if (_board.WindowResized)
            {
                _board.WindowResized = false;
                if(!GameConfig.Paused)
                {
                    _renderer.DrawFrame(_board);
                    var birdX = (int)Math.Round(_board.GameWindowLeft + 0.25 * _board.GameWindowWidth);
                    var scrollOffset = (int)Math.Floor(worldScrollCells);
                    _bird.VerticalPosition = _board.Center.y;
                    _bird.Position = (birdX, _board.Center.y);

                    if (!spawnPrimed)
                    {
                        nextSpawnWorldX = scrollOffset + _board.FirstSpawnScreenX;
                        spawnPrimed = true;
                    }
                        Console.ResetColor();
                }
            }
            if (!GameConfig.Paused)
            { 
                var elapsedMs = sw.Elapsed.TotalMilliseconds;
                sw.Restart();
                birdAccumulatorMs += elapsedMs;
                worldAccumulatorMs += elapsedMs;

                while (worldAccumulatorMs >= GameConfig.WorldDtMs)
                {
                    worldAccumulatorMs -= GameConfig.WorldDtMs;
                    worldScrollCells += worldSpeedCellsPerSecond * (GameConfig.WorldDtMs / 1000.0);
                    var scrollOffset = (int)Math.Floor(worldScrollCells);
                    if (lastScrollOffset == scrollOffset) continue;

                    lastScrollOffset = scrollOffset;
                    int removedCount = _pillars.RemoveAll(p => p.WorldX + p.Width - 1 < scrollOffset + _board.GameWindowLeft);

                    if (_pillars.Count < 5) removedCount++;

                    for (int i = 0; i < removedCount; i++)
                    {
                        int gapHeight = rng.Next(minGapY, maxGapY + 1);
                        int gapHalf = gapHeight / 2;
                        int gapCenterY = rng.Next(_board.GameWindowTop + gapHalf, _board.GameWindowBottom - gapHalf);

                        _pillars.Add(new Pillar
                        {
                            WorldX = (int)nextSpawnWorldX,
                            GapCenterY = gapCenterY,
                            GapHeight = gapHeight
                        });

                        nextSpawnWorldX += rng.Next(24, 33);
                    }
                    _renderer.DrawPillars(_board, _pillars, scrollOffset);
                    _renderer.RemovePillar(_board, _pillars, scrollOffset, ref count);
                }


                while (birdAccumulatorMs >= BirdDtMs)
                {
                    birdAccumulatorMs -= BirdDtMs;
                    if (Console.KeyAvailable)
                    {
                        var keyInfo = Console.ReadKey(true);
                        if (keyInfo.Key == ConsoleKey.Spacebar)
                        {
                            _birdService.MoveBird(_bird);
                        }
                    }
                    _birdService.ApplyGravity(_bird, _board, BirdDtMs / 1000.0);
                    if (_bird.Position != birdLastPosition)
                    {
                        _renderer.RemoveBird(_bird, birdLastPosition);
                        birdLastPosition = (_bird.Position.x - _bird.Width, (int)Math.Round(_bird.VerticalPosition));
                    }
                    if (_bird.Position != birdLastPosition) _renderer.DrawBird(_bird);

                }
                if (_board.Collision)
                {
                    break;
                }
                Thread.Sleep(50);
            }
            else
            {
                _renderer.DrawMenu(_board);
            }
        }
    }
}

