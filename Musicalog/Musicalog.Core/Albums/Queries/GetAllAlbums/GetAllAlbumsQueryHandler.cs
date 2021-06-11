using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicalog.Core.Albums.Dtos;
using Musicalog.Data.Repositories;
using Musicalog.Domain;

namespace Musicalog.Core.Albums.Queries.GetAllAlbums
{
    // ReSharper disable once UnusedType.Global
    public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, List<AlbumDto>>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IConverter<Album, AlbumDto> _albumEntityToDtoConverter;

        public GetAllAlbumsQueryHandler(IAlbumRepository albumRepository,
                                        IConverter<Album, AlbumDto> albumEntityToDtoConverter)
        {
            _albumRepository = albumRepository;
            _albumEntityToDtoConverter = albumEntityToDtoConverter;
        }
        
        public async Task<List<AlbumDto>> HandleAsync(GetAllAlbumsQuery request)
        {
            var albums = await _albumRepository.GetByArtistAndTitleAsync(request.Artist, request.Title);
            return albums.Select(_albumEntityToDtoConverter.Convert).ToList();
        }
    }
}