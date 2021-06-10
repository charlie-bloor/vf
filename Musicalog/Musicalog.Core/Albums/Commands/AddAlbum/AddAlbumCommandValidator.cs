using FluentValidation;

namespace Musicalog.Core.Albums.Commands.AddAlbum
{
    public class AddAlbumCommandValidator : AbstractValidator<AddAlbumCommand>
    {
        public AddAlbumCommandValidator()
        {
            RuleFor(x => x.Stock).
                GreaterThanOrEqualTo(0);
            
            RuleFor(x => x.Title)
                .NotEmpty();
            
            RuleFor(x => x.ArtistName)
                .NotEmpty();
        }
    }
}
