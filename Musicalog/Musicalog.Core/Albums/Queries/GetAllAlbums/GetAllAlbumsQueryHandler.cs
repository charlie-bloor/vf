using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicalog.Core.Albums.Dtos;
using Musicalog.Data.Repositories;
using Musicalog.Domain;

namespace Musicalog.Core.Albums.Queries.GetAllAlbums
{
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
            // TODO: filter
            var albums = await _albumRepository.GetAllAsync();
            return albums.Select(_albumEntityToDtoConverter.Convert).ToList();
        }
    }
}