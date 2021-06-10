using System.Threading.Tasks;
using FluentValidation;
using Musicalog.Core.Services;
using Musicalog.Data.Repositories;
using Musicalog.Domain;

namespace Musicalog.Core.Albums.Commands.UpdateAlbum
{
    // ReSharper disable once UnusedType.Global
    public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand>
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IAlbumService _albumService;
        private readonly IUpdater<UpdateAlbumCommand, Album> _albumUpdater;
        private readonly IValidator<UpdateAlbumCommand> _validator;

        public UpdateAlbumCommandHandler(IAlbumRepository albumRepository,
                                         IAlbumService albumService,
                                         IUpdater<UpdateAlbumCommand, Album> albumUpdater,
                                         IValidator<UpdateAlbumCommand> validator)
        {
            _albumRepository = albumRepository;
            _albumService = albumService;
            _albumUpdater = albumUpdater;
            _validator = validator;
        }
        public async Task HandleAsync(UpdateAlbumCommand request)
        {
            _validator.ValidateAndThrow(request);
            var album = await _albumRepository.SingleAsync(request.AlbumId);
            _albumUpdater.Update(request, album);
            await _albumService.UpdateAsync(album);
        }
    }
}