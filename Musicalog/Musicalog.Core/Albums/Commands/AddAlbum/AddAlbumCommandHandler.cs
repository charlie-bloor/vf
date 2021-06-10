using System.Threading.Tasks;
using FluentValidation;
using Musicalog.Core.Albums.Dtos;
using Musicalog.Core.Services;
using Musicalog.Domain;

namespace Musicalog.Core.Albums.Commands.AddAlbum
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AddAlbumCommandHandler : IRequestHandler<AddAlbumCommand, AlbumDto>
    {
        private readonly IAlbumService _albumService;
        private readonly IConverter<Album, AlbumDto> _albumEntityToDtoConverter;
        private readonly IConverter<AddAlbumCommand, Album> _albumCommandToEntityConverter;
        private readonly IValidator<AddAlbumCommand> _validator;

        public AddAlbumCommandHandler(IAlbumService albumService,
                                      IConverter<Album, AlbumDto> albumEntityToDtoConverter,
                                      IConverter<AddAlbumCommand, Album> albumCommandToEntityConverter,
                                      IValidator<AddAlbumCommand> validator)
        {
            _albumService = albumService;
            _albumEntityToDtoConverter = albumEntityToDtoConverter;
            _albumCommandToEntityConverter = albumCommandToEntityConverter;
            _validator = validator;
        }
        public async Task<AlbumDto> HandleAsync(AddAlbumCommand request)
        {
            _validator.ValidateAndThrow(request);
            var entity = _albumCommandToEntityConverter.Convert(request);
            await _albumService.AddAsync(entity);
            return _albumEntityToDtoConverter.Convert(entity);
        }
    }
}