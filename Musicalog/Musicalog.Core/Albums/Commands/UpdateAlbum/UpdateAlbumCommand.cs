using System.Runtime.Serialization;

namespace Musicalog.Core.Albums.Commands.UpdateAlbum
{
    public class UpdateAlbumCommand
    {        
        [IgnoreDataMember]
        public int AlbumId { get; set; }

        /// <summary>
        /// The Title of the Album
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// The name of the Artist
        /// </summary>
        public string ArtistName { get; set; }
        
        /// <summary>
        /// The number of albums currently in stock
        /// </summary>
        public int Stock { get; set; }
        
    }
}