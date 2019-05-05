using Microsoft.EntityFrameworkCore;

namespace ComicsStore.Data.Model
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

            modelBuilder.Entity<StoryCharacter>()
                .HasKey(sc => new { sc.StoryId, sc.CharacterId });

            modelBuilder.Entity<StoryCharacter>()
                .HasOne(sc => sc.Story)
                .WithMany(s => s.StoryCharacter);

            modelBuilder.Entity<StoryCharacter>()
                .HasOne(sc => sc.Character)
                .WithMany(c => c.StoryCharacter);

            modelBuilder.Entity<StoryArtist>()
                .HasKey(sa => new { sa.StoryId, sa.ArtistId });

            modelBuilder.Entity<StoryArtist>()
                .HasOne(sa => sa.Story)
                .WithMany(s => s.StoryArtist);

            modelBuilder.Entity<StoryArtist>()
                .HasOne(sa => sa.Artist)
                .WithMany(a => a.StoryArtist);

            modelBuilder.Entity<StoryBook>()
                .HasKey(sb => new { sb.StoryId, sb.BookId });

            modelBuilder.Entity<StoryBook>()
                .HasOne(sb => sb.Story)
                .WithMany(s => s.StoryBook);

            modelBuilder.Entity<StoryBook>()
                .HasOne(sb => sb.Book)
                .WithMany(b => b.StoryBook);

            modelBuilder.Entity<BookSeries>()
                .HasKey(bs => new { bs.BookId, bs.SeriesId });

            modelBuilder.Entity<BookSeries>()
                .HasOne(bs => bs.Book)
                .WithMany(b => b.BookSeries);

            modelBuilder.Entity<BookSeries>()
                .HasOne(bs => bs.Series)
                .WithMany(s => s.BookSeries);

            modelBuilder.Entity<BookSeries>()
                .Property(bs => bs.SeriesOrder)
                .HasColumnType("numeric(18,2)");

            modelBuilder.Entity<BookPublisher>()
                .HasKey(bp => new { bp.BookId, bp.PublisherId });

            modelBuilder.Entity<BookPublisher>()
                .HasOne(bp => bp.Book)
                .WithMany(b => b.BookPublisher);

            modelBuilder.Entity<BookPublisher>()
                .HasOne(bp => bp.Publisher)
                .WithMany(p => p.BookPublisher);

            modelBuilder.Entity<Code>()
                .HasIndex(c => c.Name)
                .IsUnique(true);

            modelBuilder.Query<ExportStory>()
                .ToView("ExportStories");

            modelBuilder.Query<ExportBook>()
                .ToView("ExportBooks")
                .Property(v => v.StoryNumber).HasColumnName("Story Number");

            modelBuilder.Query<StorySeries>()
                .ToView("StorySeries");
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

        public virtual DbQuery<ExportBook> ExportBooks { get; set; }
        public virtual DbQuery<ExportStory> ExportStory { get; set; }
        public virtual DbQuery<StorySeries> StorySeries { get; set; }
    }
}
