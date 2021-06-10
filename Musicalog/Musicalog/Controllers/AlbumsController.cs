using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicalog.Core;
using Musicalog.Core.Albums.Commands.AddAlbum;
using Musicalog.Core.Albums.Commands.RemoveAlbum;
using Musicalog.Core.Albums.Commands.UpdateAlbum;
using Musicalog.Core.Albums.Dtos;
using Musicalog.Core.Albums.Queries.GetAllAlbums;

namespace Musicalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IRequestHandler<AddAlbumCommand, AlbumDto> _addAlbumRequestHandler;
        private readonly IRequestHandler<GetAllAlbumsQuery, List<AlbumDto>> _getAllAlbumsRequestHandler;
        private readonly IRequestHandler<RemoveAlbumCommand> _removeAlbumCommandHandler;
        private readonly IRequestHandler<UpdateAlbumCommand> _updateAlbumCommandHandler;

        public AlbumsController(IRequestHandler<AddAlbumCommand, AlbumDto> addAlbumRequestHandler,
                                IRequestHandler<GetAllAlbumsQuery, List<AlbumDto>> getAllAlbumsRequestHandler,
                                IRequestHandler<RemoveAlbumCommand> removeAlbumCommandHandler,
                                IRequestHandler<UpdateAlbumCommand> updateAlbumCommandHandler)
        {
            _addAlbumRequestHandler = addAlbumRequestHandler;
            _getAllAlbumsRequestHandler = getAllAlbumsRequestHandler;
            _removeAlbumCommandHandler = removeAlbumCommandHandler;
            _updateAlbumCommandHandler = updateAlbumCommandHandler;
        }
        
        /// <summary>
        /// Get all albums
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AlbumDto>>> GetAll()  // TODO: filter criteria
        {
            var query = new GetAllAlbumsQuery();
            return Ok(await _getAllAlbumsRequestHandler.HandleAsync(query));
        }

        /// <summary>
        /// Add an album
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<AlbumDto>> Add([FromBody] AddAlbumCommand command)
        {
            return Ok(await _addAlbumRequestHandler.HandleAsync(command));
        }

        /// <summary>
        /// Update an album
        /// </summary>
        [HttpPut("{albumId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Update(int albumId, [FromBody] UpdateAlbumCommand command)
        {
            command.AlbumId = albumId;
            await _updateAlbumCommandHandler.HandleAsync(command);
            return NoContent();
        }

        /// <summary>
        /// Remove an album
        /// </summary>
        [HttpDelete("{albumId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Remove(int albumId)
        {
            await _removeAlbumCommandHandler.HandleAsync(new RemoveAlbumCommand(albumId));
            return NoContent();
        }
    }
}
