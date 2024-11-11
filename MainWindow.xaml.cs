using egz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private List<Album> _albumList;
        private int _currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeAlbumList();
            UpdateDisplayedAlbum(_currentIndex);
        }

        private void InitializeAlbumList()
        {
            _albumList = new List<Album>();

            try
            {
                string[] fileLines = File.ReadAllLines("Data.txt");

                for (int i = 0; i < fileLines.Length; i += 5)
                {
        
                    if (i + 4 >= fileLines.Length)
                    {
                        MessageBox.Show("Nieprawidłowa struktura pliku Data.txt. Brakuje danych dla niektórych albumów.");
                        break;
                    }

                    string artist = fileLines[i];
                    string title = fileLines[i + 1];

                    bool isTrackCountValid = int.TryParse(fileLines[i + 2], out int trackCount);
                    bool isReleaseYearValid = int.TryParse(fileLines[i + 3], out int releaseYear);
                    bool isDownloadCountValid = int.TryParse(fileLines[i + 4], out int downloadCount);

                    if (!isTrackCountValid || !isReleaseYearValid || !isDownloadCountValid)
                    {
                        MessageBox.Show($"Błąd formatu danych w albumie: {artist} - {title}");
                        continue; 
                    }

                    var album = new Album(artist, title, trackCount, releaseYear, downloadCount);
                    _albumList.Add(album);
                }

                if (_albumList.Count == 0)
                {
                    MessageBox.Show("Brak prawidłowych danych albumów w pliku Data.txt.");
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Plik Data.txt nie został znaleziony. Upewnij się, że plik znajduje się w odpowiedniej lokalizacji.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas wczytywania danych: {ex.Message}");
            }
        }

        private void UpdateDisplayedAlbum(int index)
        {
            if (_albumList == null || _albumList.Count == 0) return;

            var album = _albumList[index];
            ArtistLabel.Content = album.Artist;
            TitleLabel.Content = album.Title;
            TrackCountLabel.Content = $"{album.TrackCount} utworów";
            ReleaseYearLabel.Content = album.ReleaseYear.ToString();
            DownloadCountLabel.Content = album.DownloadCount.ToString();
        }

        private void Pobierz_Click(object sender, RoutedEventArgs e)
        {
            if (_albumList == null || _albumList.Count == 0) return;

            _albumList[_currentIndex].DownloadCount++;
            UpdateDisplayedAlbum(_currentIndex); 
        }

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            if (_albumList == null || _albumList.Count == 0) return;

            _currentIndex = (_currentIndex - 1 + _albumList.Count) % _albumList.Count;
            UpdateDisplayedAlbum(_currentIndex);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (_albumList == null || _albumList.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % _albumList.Count;
            UpdateDisplayedAlbum(_currentIndex);
        }
    }
}
