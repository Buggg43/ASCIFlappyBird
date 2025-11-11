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
        public static volatile bool ExitGame = false;
        public static volatile bool Paused = false;


        public static volatile bool GameDrawn = false;
        public static volatile bool PauseDrawn = false;
        public static volatile bool MenuDrawn = false;
        public static volatile bool ShowSoundPanel = false;

        public static volatile bool ShowGame = false;
        public static volatile bool ShowMenu = true;
        public static volatile bool ShowAbout = false;
        // Apparence
        public static readonly ConsoleColor[] Colors = new[]{
            ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green,
            ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta
        };
        public static int ColorOffSet = 0;
        // Menu 
        public static readonly List<string> MenuOptions = new List<string>{
            "Start Game",
            "Sound Panel",
            "Leader Board",
            "Controls",
            "Exit Game"
        };
        private static int selected;
        public static int SelectedMenu
        {
            get => selected;
            set => selected = value >= 0 && value <= 4 ? value : selected;
        }
        public static int PrevSelectedMenu;
        public static volatile bool UpdateCursor = true;

        //Audio
        public static readonly object audioLock = new object();
        public static bool Muted = false;
        public static float CurentVolume = 0.5f;
        public static float PreviousVolume = 0.5f;


    }
}
