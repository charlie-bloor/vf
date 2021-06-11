using Musicalog.Domain;

namespace Musicalog.Core.Albums.Dtos
{
    public class AlbumDto
    {
        /// <summary>
        /// The ID of the Album
        /// </summary>
        public int Id { get; set; }
        
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
        
        /// <summary>
        /// The type of media e.g. CD or Vinyl
        /// </summary>
        public MediaType MediaType { get; set; }        
    }
}