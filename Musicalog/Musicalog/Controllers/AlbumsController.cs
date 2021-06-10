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
        public async Task<ActionResult<List<AlbumDto>>> Get()  // TODO: filter criteria
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
        public async Task<ActionResult<AlbumDto>> Post([FromBody] AddAlbumCommand command)
        {
            return Ok(await _addAlbumRequestHandler.HandleAsync(command));
        }

        // PUT: api/albums/5
        [HttpPut("{albumId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateAlbumCommand command)
        {
            command.Id = id;
            await _updateAlbumCommandHandler.HandleAsync(command);
            return NoContent();
        }

        [HttpDelete("{albumId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int albumId)
        {
            await _removeAlbumCommandHandler.HandleAsync(new RemoveAlbumCommand(albumId));
            return NoContent();
        }
    }
}
