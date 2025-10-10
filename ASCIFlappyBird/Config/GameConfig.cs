using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIFlappyBird.Config
{
    public sealed class GameConfig
    {
        // Clocks
        public const int BirdDtMs = 16;
        public const int WorldDtMs = 16;

        // Physics
        public const double GravityAcceleration = 5.0;
        public const double WorldSpeedCellsPerSecond = 8.0;

        // Pillars
        public const int MinGapY = 5;
        public const int MaxGapY = 9;
        public const int MinSpacing = 24; 
        public const int MaxSpacing = 33;
        public const double FirstSpawnFraction = 0.75; 
    }
}
