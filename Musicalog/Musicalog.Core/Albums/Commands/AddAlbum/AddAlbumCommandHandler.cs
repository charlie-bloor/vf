using System.Threading.Tasks;
using Musicalog.Core.Albums.Dtos;
using Musicalog.Core.Services;
using Musicalog.Domain;

namespace Musicalog.Core.Albums.Commands.AddAlbum
{
    public class AddAlbumCommandHandler : IRequestHandler<AddAlbumCommand, AlbumDto>
    {
        private readonly IAlbumService _albumService;
        private readonly IConverter<Album, AlbumDto> _albumEntityToDtoConverter;
        private readonly IConverter<AddAlbumCommand, Album> _albumCommandToEntityConverter;

        public AddAlbumCommandHandler(IAlbumService albumService,
                                      IConverter<Album, AlbumDto> albumEntityToDtoConverter,
                                      IConverter<AddAlbumCommand, Album> albumCommandToEntityConverter)
        {
            _albumService = albumService;
            _albumEntityToDtoConverter = albumEntityToDtoConverter;
            _albumCommandToEntityConverter = albumCommandToEntityConverter;
        }
        public async Task<AlbumDto> HandleAsync(AddAlbumCommand request)
        {
            var entity = _albumCommandToEntityConverter.Convert(request);
            await _albumService.AddAsync(entity);
            return _albumEntityToDtoConverter.Convert(entity);
        }
    }
}