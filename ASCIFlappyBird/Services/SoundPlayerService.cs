using NAudio.Wave;

namespace ASCIFlappyBird.Services
{
    public class SoundPlayerService : IDisposable
    {
        private IWavePlayer? _outputDevice;
        private AudioFileReader? _audioFile;

        public void Play(string filePath)
        {

        }
        public void Stop()
        {
            _outputDevice?.Stop();
            _outputDevice?.Dispose();
            _outputDevice = null;

            _audioFile?.Dispose();
            _audioFile = null;
        }
        public void Dispose()
        {
            Stop();
        }

    }
}
