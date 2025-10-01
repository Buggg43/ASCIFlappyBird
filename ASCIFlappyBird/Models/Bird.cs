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
        public int Velocity { get; set; }
        public int Gravity { get; set; }
        public int JumpStrength { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
