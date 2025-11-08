using ASCIFlappyBird.Models;

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

        public void ApplyGravity(Bird bird, Board board, double deltaTimeSeconds)
        {
            double maxUpSpeed = 10.0;
            double maxDownSpeed = 15.0;

            bird.VerticalSpeed += bird.GravityAcceleration * deltaTimeSeconds;
            bird.VerticalSpeed = Math.Clamp(bird.VerticalSpeed, -maxUpSpeed, maxDownSpeed);
            bird.VerticalPosition += bird.VerticalSpeed * deltaTimeSeconds;

            double halfH = bird.Height / 2.0;
            double bottom = board.GameWindowBottom - halfH;
            double top = board.GameWindowTop + halfH;

            bird.VerticalPosition = Math.Clamp(bird.VerticalPosition, top, bottom);
            if (bird.VerticalPosition == top || bird.VerticalPosition == bottom)
                bird.VerticalSpeed = 0;

            bird.Position = (bird.Position.x, (int)Math.Round(bird.VerticalPosition - halfH));

        }
        public bool BirdCollision(Bird bird, Board board, List<Pillar> pilars, int scrollOfset)
        {
            //var pillarCollision  = pilars.Any(p => p.WorldX-)
            return false;
        }
    }
}