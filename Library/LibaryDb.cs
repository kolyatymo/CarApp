using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Library
{
    public class LibraryDb : DbContext
    {
        public LibraryDb()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"
                                Data Source = (localdb)\MSSQLLocalDB;
                                Initial Catalog = MyLibrary;
                                Integrated Security = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Authors>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            
            modelBuilder.Entity<Authors>()
                .Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Books>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Shops>()
                .Property (x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Themes>()
                .Property(x => x.Name)
                .HasMaxLength(50);

            // 1 ... * (-_') 

            modelBuilder.Entity<Sales>()
                .HasOne(s => s.Shops)
                .WithMany(m => m.Sales)
                .HasForeignKey(f => f.ShopId);

            modelBuilder.Entity<Authors>()
                .HasOne(c => c.Countries)
                .WithMany(m => m.Authors)
                .HasForeignKey(f => f.CountryId);
            
            modelBuilder.Entity<Countries>()
                .HasMany(m => m.Shops)
                .WithOne(o => o.Countries)
                .HasForeignKey(f => f.CountryId);

            modelBuilder.Entity<Authors>()
                .HasMany(m => m.Books)
                .WithOne(o => o.Authors)
                .HasForeignKey(f => f.AuthorId);

            modelBuilder.Entity<Books>()
                .HasMany(m => m.Sales)
                .WithOne(o => o.Books)
                .HasForeignKey(f => f.BookId);

            modelBuilder.Entity<Books>()
                .HasOne(o => o.Themes)
                .WithMany(m => m.Books)
                .HasForeignKey(f => f.ThemeId);
        }

        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Themes> Themes { get; set; }
    }

public class Authors
    {
        public Authors()
        {
            Books = new HashSet<Books>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public ICollection<Books> Books { get; set; }
        public int? CountryId { get; set; }
        public Countries Countries { get; set; }
    }

    public class Books
    {
        public Books()
        {
            Sales = new HashSet<Sales>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Pages { get; set; }
        public int Price { get; set; }
        public DateTime PublishDate { get; set; }
        public int? ThemeId { get; set; }
        public int? AuthorId { get; set; }
        public Themes Themes { get; set; }
        public Authors Authors { get; set; }
        public ICollection<Sales> Sales { get; set; }
    }

    public class Countries
    {
        public Countries()
        {
            Authors = new HashSet<Authors>();
            Shops = new HashSet<Shops>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Authors> Authors { get; set; }
        public ICollection<Shops> Shops { get; set; }

    }

    public class Sales
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? SaleDate { get; set; }
        public int? BookId { get; set; }
        public int? ShopId { get; set; }
        public Books Books { get; set; }
        public Shops Shops { get; set; }
    }
    public class Shops
    {
        public Shops()
        {
            Sales = new HashSet<Sales>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public Countries Countries { get; set; }
        public ICollection<Sales> Sales { get; set; }
    }

    public class Themes
    {
        public Themes()
        {
            Books = new HashSet<Books>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Books> Books { get; set; }
    }
}
