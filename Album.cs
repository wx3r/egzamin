using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace egz
{

    public class Album
    {
        public string Artist { get; }
        public string Title { get; }
        public int TrackCount { get; }
        public int ReleaseYear { get; }
        public int DownloadCount { get; set; }  

        public Album(string artist, string title, int trackCount, int releaseYear, int downloadCount)
        {
            Artist = artist;
            Title = title;
            TrackCount = trackCount;
            ReleaseYear = releaseYear;
            DownloadCount = downloadCount;
        }
    }

}
