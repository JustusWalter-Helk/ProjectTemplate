﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Frontend.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Frontend.MVVM.View
{
    /// <summary>
    /// Interaction logic for LikedSongs.xaml
    /// </summary>
    public partial class LikedSongs : UserControl
    {
        AudioManager audioManager = AudioManager.getAudioManager();
        static BackendAPI backendAPI = BackendAPI.getBackendAPI();
        ResourceManager resourceManager = backendAPI.createResourceManager();
        public LikedSongs()
        {
            InitializeComponent();
            List<Song> songs = resourceManager.getSongs();
            songsListBox.ItemsSource = songs;
            songsListBox.DataContext = this;
        }

        private async void songsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (songsListBox.SelectedItem != null && songsListBox.SelectedItem is Song selectedSong)
            {
                // Assuming Song class has a property named "songName"
                string songName = selectedSong.SongName;

                await Task.Run(() => { audioManager.playSong(songName); });
            }
        }
    }
}
