namespace Musicalog.Core.Albums.Commands.AddAlbum
{
    public class AddAlbumCommand
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
    }
}