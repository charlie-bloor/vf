using System.Threading.Tasks;
using Musicalog.Core.Albums.Commands.RemoveAlbum;
using Musicalog.Core.Albums.Dtos;

namespace Musicalog.Core.Albums.Commands.UpdateAlbum
{
    public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand>
    {
        public Task HandleAsync(UpdateAlbumCommand request)
        {
            throw new System.NotImplementedException();
        }
    }
}