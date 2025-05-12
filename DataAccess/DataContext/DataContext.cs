using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess.DataContext
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    UserName = "admin",
                    Password = "admin",
                    Email = "admin@gmail.com",
                    CreatedOn = new DateTime(2025, 1, 1),
                    Slug = "admin"
                });
            modelBuilder.Entity<Book>()
                .HasMany(book => book.BookTags)
                .WithMany(bookTag => bookTag.Books)
                .UsingEntity(bookTag => bookTag.ToTable("BookTagsRelation"));

            modelBuilder.Entity<Book>()
                .HasMany(book => book.Authors)
                .WithMany(author => author.Books)
                .UsingEntity(author => author.ToTable("AuthorsRelation"));


            modelBuilder.Entity<Book>()
                .HasOne(book => book.Publisher)
                .WithMany(publisher => publisher.Books)
                .HasForeignKey(book => book.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.Image>()
                .HasOne(i => i.Book)
                .WithMany(b => b.Images)
                .HasForeignKey(i => i.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Models.Image>()
                .HasOne(i => i.Publisher)
                .WithMany(p => p.Images)
                .HasForeignKey(i => i.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Models.Image>()
                .HasOne(i => i.AuthorDetail)
                .WithMany(a => a.Images)
                .HasForeignKey(i => i.AuthorDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Models.Image>()
                .HasOne(i => i.User)
                .WithOne(a => a.Image)
                .HasForeignKey<Models.Image>(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>()
                .HasOne(a => a.AuthorDetail)
                .WithOne(ad => ad.Author)
                .HasForeignKey<AuthorDetail>(ad => ad.AuthorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();


        }
        public DbSet<Models.Image> Images { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorDetail> AuthorDetails { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<User> Users { get; set; }

    }
}

