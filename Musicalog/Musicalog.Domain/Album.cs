#nullable disable

namespace Musicalog.Domain
{
    public partial class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public int Stock { get; set; }
    }
}
