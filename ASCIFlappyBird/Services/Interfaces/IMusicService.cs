using ASCIFlappyBird.Enums;

namespace ASCIFlappyBird.Services.Interfaces
{
    public interface IMusicService
    {
        void Play(MusicTrack track, bool loop = true);
        void Pause();
        void Resume();
        void SetVolume(float volume);
        void ToggleMute();
        void Dispose();

        bool IsMuted { get; }
        MusicTrack? CurrentTrack { get; }

    }
}
