using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player();
  //          player.Volume = 20;
            player.Songs = GetSongsData();

//            TraceInfo(player);

            player.Play();
            player.VolumeUp();
            Console.WriteLine(player.Volume);

            player.VolumeChange(-300);
            Console.WriteLine(player.Volume);

            player.VolumeChange(300);
            Console.WriteLine(player.Volume);

            /*player.Volume = -25;
            Console.WriteLine(player.Volume);
            */
            player.Stop();

            Console.ReadLine();
        }

        public static Song[] GetSongsData()
        {
            var artist = new Artist();
            artist.Name = "Powerwolf";
            artist.Genre = "Metal";

            var artist2 = new Artist("Lordi");
            Console.WriteLine(artist2.Name);
            Console.WriteLine(artist2.Genre);

            var artist3 = new Artist("Sabaton", "Rock");
            Console.WriteLine(artist3.Name);
            Console.WriteLine(artist3.Genre);

            var album = new Album();
            album.Name = "New Album";
            album.Year = 2018;

            var song = new Song()
            {
                Duration = 100,
                Name = "New song",
                Album = album,
                Artist = artist
            };

            return new Song[] {song};
        }

        public static void TraceInfo(Player player)
        {
            Console.WriteLine(player.Songs[0].Artist.Name);
            Console.WriteLine(player.Songs[0].Duration);
            Console.WriteLine(player.Songs.Length);
            Console.WriteLine(player.Volume);
        }
    }
}
