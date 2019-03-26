using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MusicPlayer
{
    class Program
    {
        static void Main(string[] args)
        {

            using (Player player = new Player())
            {
                player.Load("d://wavs");

                player.SongStarted += ShowInfo;
                player.SongsListChanged += ShowInfo;
                player.VolumeUp();
                Console.WriteLine(player.Volume);
                player.Play();
                player.Unlock();
                while(true)
                {
                    PlayerCommand.buffer = Console.ReadLine();
                    PlayerCommand.ToCommand(player);
                }
            }
        }

        private static void ShowInfo(List<Song> songs, Song playingSong, bool locked, int volume)
        {
            Console.Clear();

            foreach (var song in songs)
            {
                if (playingSong == song)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(song.Name);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(song.Name);
                }
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Volume is: {volume}. Locked: {locked}");
            Console.ResetColor();
        }        
    }
    static class PlayerCommand
    {
        static public string buffer;
        static public void ToCommand(Player player)
        {
            Console.WriteLine("*@"+buffer+"*@");
            if (buffer == "Stop") player.Playing = false;
            else if (buffer == "Start") player.Playing = true;
            else if (buffer == "LoadPlaylist")
            {
                Console.WriteLine("Select Folder:");
                player.Load(Console.ReadLine());
            }
            else Console.WriteLine("Unknown command \nTheCommand are \"Start\", \"Stop\" and \"LoadPlaylist\" ");
        }
    }
}
