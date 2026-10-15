using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarData
{
    public class CarDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"
                                            Data Source = (localdb)\MSSQLLocalDB;
                                            Initial Catalog = CarSalon;
                                            Integrated Security = True;");
        }

        public virtual DbSet<Car> Cars { get; set; }
    }

    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        public int ModelYear { get; set; }
    }
}
