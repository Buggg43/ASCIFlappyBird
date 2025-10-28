namespace ASCIFlappyBird.Config
{
    public sealed class GameConfig
    {
        // Clocks
        public const int BirdDtMs = 6;
        public const int WorldDtMs = 6;
        public static readonly object consoleLock = new();


        // Physics
        public const double GravityAcceleration = 5.0;
        public const double WorldSpeedCellsPerSecond = 8.0;

        // Pillars
        public const int MinGapY = 5;
        public const int MaxGapY = 9;
        public const int MinSpacing = 24;
        public const int MaxSpacing = 33;
        public const double FirstSpawnFraction = 0.75;

        // Score
        public const int Score = 0;

        // GameState
        public static volatile bool Paused = false;
        public static volatile bool ShowMenu = true;
        public static volatile bool ShowGame = false;
        public static volatile bool GameDrawn = false;
        public static volatile bool PauseDrawn = false;
        public static volatile bool MenuDrawn = false;

        // Apparence
        public static readonly ConsoleColor[] Colors = new[]{
            ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green,
            ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta
        };
        public static int ColorOffSet = 0;
        // ????????????????????????   
        //public static readonly List<Menu> MenuOptions = new List<Menu> { };
    }
}
