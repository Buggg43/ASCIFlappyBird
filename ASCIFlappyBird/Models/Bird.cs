using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIFlappyBird.Models
{
    public class Bird
    {
        public (int x, int y) Position { get; set; }
        public double GravityAcceleration { get; set; } = 5.0;
        public double VerticalPosition { get; set; }
        public double VerticalSpeed { get; set; }
        public double JumpStrength { get; set; } = 6.0;
        public int Width { get; set; } =  5; 
        public int Height { get; set; }
        public double InitialJumpSpeedPerSecond => Math.Sqrt(2 * GravityAcceleration * JumpStrength);
    }
}
