﻿using ASCIFlappyBird.Config;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace ASCIFlappyBird.Services
{
    public class SoundPlayerService : IDisposable
    {
        private IWavePlayer? _outputDevice;
        private AudioFileReader? _audioFile;
        private VolumeSampleProvider? _volumeProvider;
        private string menuMusic = "C:\\Users\\Desktop-KW\\source\\repos\\ASCIFlappyBird\\ASCIFlappyBird\\Assets\\bg2.mp3";
        private string gameMusic = "C:\\Users\\Desktop-KW\\source\\repos\\ASCIFlappyBird\\ASCIFlappyBird\\Assets\\bg1.mp3";

        public void Play()
        {
            Stop();
            _outputDevice = new WaveOutEvent();

            if (GameConfig.GameDrawn)
            {

                _audioFile = new AudioFileReader(gameMusic);
                _volumeProvider = new VolumeSampleProvider(_audioFile.ToSampleProvider());
                _volumeProvider.Volume = 0.05f;
                _outputDevice.Init(_volumeProvider.ToWaveProvider());
                _outputDevice.Play();
            }
            else if (GameConfig.ShowMenu)
            {
                _audioFile = new AudioFileReader(menuMusic);
                _volumeProvider = new VolumeSampleProvider(_audioFile.ToSampleProvider());
                _volumeProvider.Volume = 0.05f;
                _outputDevice.Init(_volumeProvider.ToWaveProvider());
                _outputDevice.Play();
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
    }
}
