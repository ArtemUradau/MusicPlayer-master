using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MusicPlayer
{
    public class Player
    {
        const int MIN_VOLUME = 0;
        const int MAX_VOLUME = 100;

        private int _volume;
        private bool _locked;
        private bool _playing;

        public List<Song> Songs { get; set; } = new List<Song>();
        public Song PlayingSong { get; private set; }

        public event Action<List<Song>, Song, bool, int> PlayerStarted;
        public event Action<List<Song>, Song, bool, int> SongStarted;
        public event Action<List<Song>, Song, bool, int> SongsListChanged;
        public event Action<List<Song>, Song, bool, int> VolumeChanged;
        public event Action<List<Song>, Song, bool, int> PlayerLocked;
        public int Volume
        {
            get
            {
                return _volume;
            }

            private set
            {
                if (value < MIN_VOLUME)
                {
                    _volume = MIN_VOLUME;
                }
                else if (value > MAX_VOLUME)
                {
                    _volume = MAX_VOLUME;
                }
                else
                {
                    _volume = value;
                }
            }
        }

        public bool Playing
        {
            get { return _playing; }
        }

        public void VolumeUp()
        {
            if (_locked == false)
            {
                Volume++;
            }
            VolumeChanged?.Invoke(null, null, _locked, _volume);
            Console.WriteLine("Volume has been increased");
        }

        public void VolumeDown()
        {
            if (_locked == false)
            {
                Volume--;
            }
            VolumeChanged?.Invoke(null, null, _locked, _volume);
            Console.WriteLine("Volume has been decreased");
        }

        public void VolumeChange(int step)
        {
            if (_locked == false)
            {
                Volume += step;
            }
            VolumeChanged?.Invoke(Songs, PlayingSong, _locked, _volume);
            Console.WriteLine($"Volume has been changed by {step}");
        }

        public void Lock()
        {
            _locked = true;
            PlayerLocked?.Invoke(null, null, _locked,_volume);
            Console.WriteLine("Player is locked");
        }

        public void Unlock()
        {
            _locked = false;
            PlayerLocked?.Invoke(null, null, _locked, _volume);
            Console.WriteLine("Player is unlocked");
        }

        public bool Play()
        {
            if (_locked == false&&Songs.Count>0)
            {
                _playing = true;
            }
            PlayerStarted?.Invoke(null, null, _playing, _volume);
            Console.WriteLine("Player has been started");

            if (_playing)
            {
                foreach (var song in Songs)
                {
                    PlayingSong = song;
                    SongStarted?.Invoke(Songs, song, _locked, _volume);
                    using (System.Media.SoundPlayer player = new System.Media.SoundPlayer())
                    {
                        player.SoundLocation = PlayingSong.Path;
                        player.PlaySync();
                    }
                }
            }
            _playing = false;
            return _playing;
        }

        public bool Stop()
        {
            if (_locked == false)
            {
                _playing = false;
            }
            PlayerStarted?.Invoke(null,null, _playing, _volume);
            Console.WriteLine("Player has been stopped");
            return _playing;
        }

        public void Load(string source)
        {
            var dirInfo = new DirectoryInfo(source);

            if (dirInfo.Exists)
            {
                var files = dirInfo.GetFiles();
                foreach (var file in files)
                {
                    var song = new Song
                    {
                        Path= file.FullName,
                        Name = file.Name
                    };

                    Songs.Add(song);
                }
            }
            SongsListChanged?.Invoke(Songs, PlayingSong, _locked, _volume);
        }
    }
}
