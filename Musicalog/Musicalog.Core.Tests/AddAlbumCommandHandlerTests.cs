using System.Threading.Tasks;
using Moq;
using Musicalog.Core.Albums.Commands.AddAlbum;
using Musicalog.Core.Albums.Dtos;
using Musicalog.Data.Repositories;
using Musicalog.Domain;
using Musicalog.TestUtilities;
using NUnit.Framework;

namespace Musicalog.Core.Tests
{
    [TestFixture]
    public class AddAlbumCommandHandlerTests : MockBase<AddAlbumCommandHandler>
    {
        [Test]
        public async Task HandleAsync_AllIsWell_CallsServices()
        {
            // Arrange
            var testInputCommand = new AddAlbumCommand();
            var testInputEntity = new Album();

            GetMock<IConverter<AddAlbumCommand, Album>>()
                .Setup(x => x.Convert(testInputCommand))
                .Returns(testInputEntity);
            
            var testInputDto = new AlbumDto();

            GetMock<IConverter<Album, AlbumDto>>()
                .Setup(x => x.Convert(testInputEntity))
                .Returns(testInputDto);
            
            // Act
            await Subject.HandleAsync(testInputCommand);

            // Assert
            GetMock<IConverter<AddAlbumCommand, Album>>()
                .Verify(x => x.Convert(testInputCommand), Times.Once);
            
            GetMock<IAlbumRepository>()
                .Verify(x => x.AddAsync(testInputEntity), Times.Once);

            GetMock<IConverter<Album, AlbumDto>>()
                .Verify(x => x.Convert(testInputEntity));
        }
    }
}