using System.Threading.Tasks;
using Musicalog.Core.Albums.Dtos;

namespace Musicalog.Core.Albums.Commands.RemoveAlbum
{
    public class RemoveAlbumCommandHandler : IRequestHandler<RemoveAlbumCommand>
    {
        public Task HandleAsync(RemoveAlbumCommand request)
        {
            throw new System.NotImplementedException();
        }
    }
}