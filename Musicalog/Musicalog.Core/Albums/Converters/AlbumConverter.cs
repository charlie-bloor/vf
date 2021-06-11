using Musicalog.Core.Albums.Commands.AddAlbum;
using Musicalog.Core.Albums.Commands.UpdateAlbum;
using Musicalog.Core.Albums.Dtos;
using Musicalog.Domain;

namespace Musicalog.Core.Albums.Converters
{
    // ReSharper disable once UnusedType.Global
    public class AlbumConverter : IConverter<AddAlbumCommand, Album>,
                                  IConverter<Album, AlbumDto>,
                                  IUpdater<UpdateAlbumCommand, Album>
    {
        public Album Convert(AddAlbumCommand value)
        {
            return new Album
            {
                Stock = value.Stock,
                Title = value.Title,
                ArtistName = value.ArtistName,
                MediaType = value.MediaType
            };
        }

        public AlbumDto Convert(Album value)
        {
            return new AlbumDto
            {
                Id = value.Id,
                Stock = value.Stock,
                Title = value.Title,
                ArtistName = value.ArtistName,
                MediaType = value.MediaType
            };
        }

        public void Update(UpdateAlbumCommand source, Album target)
        {
            target.Stock = source.Stock;
            target.Title = source.Title;
            target.ArtistName = source.ArtistName;
            target.MediaType = source.MediaType;
        }
    }
}