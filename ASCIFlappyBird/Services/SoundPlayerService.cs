using ASCIFlappyBird.Config;
using ASCIFlappyBird.Enums;
using ASCIFlappyBird.Services.Interfaces;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace ASCIFlappyBird.Services
{
    public class SoundPlayerService : IMusicService, IDisposable
    {
        private IWavePlayer? _outputDevice;
        private AudioFileReader? _audioFile;
        private VolumeSampleProvider? _volumeProvider;
        private string menuMusic = "C:\\Users\\Desktop-KW\\source\\repos\\ASCIFlappyBird\\ASCIFlappyBird\\Assets\\bg2.mp3";
        private string gameMusic = "C:\\Users\\Desktop-KW\\source\\repos\\ASCIFlappyBird\\ASCIFlappyBird\\Assets\\bg1.mp3";

        private readonly Dictionary<MusicTrack, string> _trackPaths;
        private readonly List<MusicTrack> _playList;
        private int _playListIndex;
        private MusicTrack? _currentTrack;
        private bool _isMuted;
        private float _volume;

        public SoundPlayerService()
        {
            _trackPaths = new Dictionary<MusicTrack, string>
            {
                { MusicTrack.Menu, "C:\\Users\\Desktop-KW\\source\\repos\\ASCIFlappyBird\\ASCIFlappyBird\\Assets\\bg2.mp3" },
                { MusicTrack.Game, "C:\\Users\\Desktop-KW\\source\\repos\\ASCIFlappyBird\\ASCIFlappyBird\\Assets\\bg1.mp3" }
            };
            _playList = new List<MusicTrack> { MusicTrack.Menu, MusicTrack.Game };
            _playListIndex = 0;
            _currentTrack = null;
            _isMuted = false;
            _volume = 0.5f;
        }

        public bool IsMuted => _isMuted;

        public MusicTrack? CurrentTrack => _currentTrack;

        public void Play(MusicTrack track, bool loop = true)
        {
            Stop();
            _outputDevice = new WaveOutEvent();

            if (GameConfig.GameDrawn)
            {
                _audioFile = new AudioFileReader(gameMusic);
                _volumeProvider = new VolumeSampleProvider(_audioFile.ToSampleProvider());
                _volumeProvider.Volume = GameConfig.CurentVolume;
                _outputDevice.Init(_volumeProvider.ToWaveProvider());
                _outputDevice.Volume = GameConfig.CurentVolume;
                _outputDevice.Play();
                //while (true)

                if (_outputDevice.PlaybackState == PlaybackState.Stopped)
                {
                    _audioFile.Position = 0;
                    _outputDevice.Play();
                }
                Thread.Sleep(100);

            }
            else if (GameConfig.ShowMenu)
            {
                _audioFile = new AudioFileReader(menuMusic);
                _volumeProvider = new VolumeSampleProvider(_audioFile.ToSampleProvider());
                _volumeProvider.Volume = GameConfig.CurentVolume;
                _outputDevice.Init(_volumeProvider.ToWaveProvider());
                _outputDevice.Volume = GameConfig.CurentVolume;
                _outputDevice.Play();
                //while (true)
                //{
                if (_outputDevice.PlaybackState == PlaybackState.Stopped)
                {
                    _audioFile.Position = 0;
                    _outputDevice.Play();
                }
                Thread.Sleep(100);
                //}
            }
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


        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void SetVolume(float volume)
        {
            throw new NotImplementedException();
        }

        public void ToggleMute()
        {
            throw new NotImplementedException();
        }
    }
}
