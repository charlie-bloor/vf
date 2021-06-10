namespace Musicalog.Core.Albums.Commands.RemoveAlbum
{
    public class RemoveAlbumCommand
    {
        public int AlbumId { get; }

        public RemoveAlbumCommand(int albumId)
        {
            AlbumId = albumId;
        }
    }
}