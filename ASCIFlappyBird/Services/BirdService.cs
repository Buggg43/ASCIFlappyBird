using ASCIFlappyBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIFlappyBird.Services
{
    public class BirdService
    {
        public void MoveBird(Bird bird)
        {   
            bird.VerticalSpeed = -bird.InitialJumpSpeedPerSecond;
        }
        public void ResetBird()
        {
        }
        public void ApplyGravity(Bird bird,Board board, double deltaTimeSeconds)
        {
            double maxUpSpeed = 10.0;
            double maxDownSpeed = 15.0;
            

            bird.VerticalSpeed += bird.GravityAcceleration * deltaTimeSeconds;
            bird.VerticalSpeed = Math.Clamp(bird.VerticalSpeed, -maxUpSpeed, maxDownSpeed);
            bird.VerticalPosition += bird.VerticalSpeed * deltaTimeSeconds;

            double halfH = bird.Height / 2.0;
            double bottom = board.WindowHeight - board.MarginY - 1 - halfH;
            double top = board.MarginY + 1 + halfH;

            bird.VerticalPosition = Math.Clamp(bird.VerticalPosition, top, bottom);
            if(bird.VerticalPosition == top || bird.VerticalPosition == bottom)
                bird.VerticalSpeed = 0;

            bird.Position = (bird.Position.x, (int)Math.Round(bird.VerticalPosition - halfH));
        }
    }
}
