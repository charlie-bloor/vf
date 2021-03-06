using Microsoft.EntityFrameworkCore;
using Musicalog.Domain;

#nullable disable

namespace Musicalog.Data.Contexts
{
    public interface IMusicalogContext : IDbContext
    {
        DbSet<Album> Albums { get; set; }
    }
    
    public partial class MusicalogContext : DbContext, IMusicalogContext
    {
        public MusicalogContext()
        {
        }

        public MusicalogContext(DbContextOptions<MusicalogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Album>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Album_pk")
                    .IsClustered(false);

                entity.ToTable("Album");

                entity.HasIndex(e => new { e.Title, e.ArtistName }, "UIX_Title_ArtistName")
                    .IsUnique();

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);
                
                // Store the MediaType enum as a string in the database to allow adding an enum type more easily.
                // We could instead persist this as an integer if we were more interested in performance.
                modelBuilder.Entity<Album>()
                    .Property(s => s.MediaType)
                    .HasConversion<string>();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
