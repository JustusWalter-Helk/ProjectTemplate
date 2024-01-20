﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Core
{
    class BackendAPI
    {
        [DllImport("Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateAudioManager();
        [DllImport("Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateResourceManager();

        public static AudioManager createAudioManager()
        {
            IntPtr audioManager = CreateAudioManager();
            return new AudioManager(audioManager);
        }

        public static ResourceManager createResourceManager()
        {
            IntPtr resourceManager = CreateResourceManager();
            return new ResourceManager(resourceManager);
        }
    }

    class AudioManager
    {
        [DllImport("Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void DestroyAudioManager(IntPtr audio);
        [DllImport("Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void AudioManager_PlaySong(IntPtr audio, [MarshalAs(UnmanagedType.LPStr)] string name);

        private IntPtr pointer;

        public void playSong(string songName)
        {
            AudioManager_PlaySong(pointer, songName);
        }

        //Obtain the required pointer from the CreateAudioManager method exposed by the Backend.dll
        public AudioManager(IntPtr audioManagerPointer) { 
            pointer = audioManagerPointer; 
        }

        ~AudioManager()
        {
            DestroyAudioManager(pointer);
        }
    }

    class ResourceManager
    {
        [DllImport("Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr ResourceManager_GetAvailableSongs(IntPtr resource);

        private IntPtr pointer;

        public ResourceManager(IntPtr resourceManagerPointer)
        {
            pointer = resourceManagerPointer;
        }

        public List<Song> getSongs() {
            string result = Marshal.PtrToStringAnsi(ResourceManager_GetAvailableSongs(pointer));

            Dictionary<string, Song> Songs = new Dictionary<string, Song>();
            Songs = JsonConvert.DeserializeObject<Dictionary<string, Song>>(result);

            Console.WriteLine(Songs.Values);

            List<Song> songsList = Songs.Values.ToList();

            Console.WriteLine(songsList[0].SongName);

            return songsList;
        }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Song
    {
        [JsonProperty("storageLocation")]
        public string StorageLocation { get; set; }

        [JsonProperty("sizeInBytes")]
        public int SizeInBytes { get; set; }

        [JsonProperty("songName")]
        public string SongName { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("partiallyLoaded")]
        public bool PartiallyLoaded { get; set; }

        [JsonProperty("isPlaying")]
        public bool IsPlaying { get; set; }

        [JsonProperty("isPaused")]
        public bool IsPaused { get; set; }

        [JsonProperty("hasLyricsAvailable")]
        public bool HasLyricsAvailable { get; set; }

        [JsonProperty("timeRemaining")]
        public int TimeRemaining { get; set; }
    }




}
