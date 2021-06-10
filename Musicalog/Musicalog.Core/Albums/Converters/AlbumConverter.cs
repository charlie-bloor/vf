using Musicalog.Core.Albums.Commands.AddAlbum;
using Musicalog.Core.Albums.Dtos;
using Musicalog.Domain;

namespace Musicalog.Core.Albums.Converters
{
    public class AlbumConverter : IConverter<AddAlbumCommand, Album>,
                                  IConverter<Album, AlbumDto>
    {
        public Album Convert(AddAlbumCommand value)
        {
            return new Album
            {
                Id = value.Id,
                Stock = value.Stock,
                Title = value.Title,
                ArtistName = value.ArtistName
            };
        }

        public AlbumDto Convert(Album value)
        {
            return new AlbumDto
            {
                Id = value.Id,
                Stock = value.Stock,
                Title = value.Title,
                ArtistName = value.ArtistName
            };
        }
    }
}