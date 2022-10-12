using ComicsStore.Data.Model;
using ComicsStore.Data.Model.Output;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ComicsStore.Data.Common
{
    public class ComicsStoreDbContext : DbContext
    {
        public ComicsStoreDbContext(DbContextOptions<ComicsStoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            _ = modelBuilder.Entity<Story>()
                .HasOne(s => s.OriginStory)
                .WithMany(s => s.StoryFromOrigin)
                .HasForeignKey(s => s.OriginStoryId)
                .IsRequired(false);

            _ = modelBuilder.Entity<StoryCharacter>()
                .HasKey(sc => new { sc.StoryId, sc.CharacterId });

            _ = modelBuilder.Entity<StoryCharacter>()
                .HasOne(sc => sc.Story)
                .WithMany(s => s.StoryCharacter);

            _ = modelBuilder.Entity<StoryCharacter>()
                .HasOne(sc => sc.Character)
                .WithMany(c => c.StoryCharacter);

            _ = modelBuilder.Entity<Artist>()
                .Property(a => a.FullName)
                .HasComputedColumnSql("[Name] || CASE WHEN [FirstName] IS NULL THEN '' ELSE ', ' || [FirstName] END");

            _ = modelBuilder.Entity<Pseudonym>()
                .HasKey(p => new { p.MainArtistId, p.PseudonymArtistId });

            _ = modelBuilder.Entity<Pseudonym>()
                .HasOne(p => p.MainArtist)
                .WithMany(a => a.MainArtist);

            _ = modelBuilder.Entity<Pseudonym>()
                .HasOne(p => p.PseudonymArtist)
                .WithMany(a => a.PseudonymArtist);

            _ = modelBuilder.Entity<StoryArtist>()
                .HasKey(sa => new { sa.StoryId, sa.ArtistId });

            _ = modelBuilder.Entity<StoryArtist>()
                .HasOne(sa => sa.Story)
                .WithMany(s => s.StoryArtist);

            _ = modelBuilder.Entity<StoryArtist>()
                .HasOne(sa => sa.Artist)
                .WithMany(a => a.StoryArtist);

            _ = modelBuilder.Entity<StoryBook>()
                .HasKey(sb => new { sb.StoryId, sb.BookId });

            _ = modelBuilder.Entity<StoryBook>()
                .HasOne(sb => sb.Story)
                .WithMany(s => s.StoryBook);

            _ = modelBuilder.Entity<StoryBook>()
                .HasOne(sb => sb.Book)
                .WithMany(b => b.StoryBook);

            _ = modelBuilder.Entity<BookSeries>()
                .HasKey(bs => new { bs.BookId, bs.SeriesId });

            _ = modelBuilder.Entity<BookSeries>()
                .HasOne(bs => bs.Book)
                .WithMany(b => b.BookSeries);

            _ = modelBuilder.Entity<BookSeries>()
                .HasOne(bs => bs.Series)
                .WithMany(s => s.BookSeries);

            _ = modelBuilder.Entity<BookSeries>()
                .Property(bs => bs.SeriesOrder)
                .HasColumnType("numeric(18,2)");

            _ = modelBuilder.Entity<BookPublisher>()
                .HasKey(bp => new { bp.BookId, bp.PublisherId });

            _ = modelBuilder.Entity<BookPublisher>()
                .HasOne(bp => bp.Book)
                .WithMany(b => b.BookPublisher);

            _ = modelBuilder.Entity<BookPublisher>()
                .HasOne(bp => bp.Publisher)
                .WithMany(p => p.BookPublisher);

            _ = modelBuilder.Entity<Code>()
                .HasIndex(c => c.Name)
                .IsUnique(true);

            _ = modelBuilder.Entity<ExportStory>()
                .HasNoKey()
                .ToView("ExportStories");

            _ = modelBuilder.Entity<ExportBook>()
                .HasNoKey()
                .ToView("ExportBooks")
                .Property(v => v.StoryNumber).HasColumnName("Story Number");

            _ = modelBuilder.Entity<StorySeries>()
                .HasNoKey()
                .ToView("StorySeries");
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();
            foreach (var entry in changedEntriesCopy)
            {
                if (entry.State == EntityState.Detached) { }
            }
        }

        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<Code> Codes { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Series> Series { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        public virtual DbSet<StoryCharacter> StoryCharacters { get; set; }
        public virtual DbSet<StoryArtist> StoryArtists { get; set; }
        public virtual DbSet<StoryBook> StoryBooks { get; set; }
        public virtual DbSet<BookSeries> BookSeries { get; set; }
        public virtual DbSet<BookPublisher> BookPublishers { get; set; }
        public virtual DbSet<Pseudonym> Pseudonyms { get; set; }

        public virtual DbSet<ExportBook> ExportBooks { get; set; }
        public virtual DbSet<ExportStory> ExportStory { get; set; }
        public virtual DbSet<StorySeries> StorySeries { get; set; }
    }
}
