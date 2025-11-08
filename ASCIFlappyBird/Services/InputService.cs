namespace ASCIFlappyBird.Services
{
    public class InputService
    {
        private InputHandler _inputHandler = new InputHandler();
        private Task? _listeningTask;
        public void InputListener(CancellationToken cancellationToken)
        {
            _listeningTask = Task.Run(() => LoopInputAsync(cancellationToken), cancellationToken);
        }
        private async Task LoopInputAsync(CancellationToken ct)
        {
            const int PollDelayMs = 20;
            while (!ct.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);
                    _inputHandler.HandleKey(key.Key);
                }
                else
                {
                    try
                    {
                        await Task.Delay(PollDelayMs, ct);
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }
                }
            }
        }
    }
}