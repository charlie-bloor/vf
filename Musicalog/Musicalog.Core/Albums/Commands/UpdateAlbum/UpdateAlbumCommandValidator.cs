using FluentValidation;

namespace Musicalog.Core.Albums.Commands.UpdateAlbum
{
    public class UpdateAlbumCommandValidator : AbstractValidator<UpdateAlbumCommand>
    {
        public UpdateAlbumCommandValidator()
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
