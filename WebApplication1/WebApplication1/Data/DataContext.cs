using Microsoft.EntityFrameworkCore;
using MontyHallAPI.Models;

namespace WebApplication1.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<SimulationResult> MontyHalles => Set<SimulationResult>();

    }
}
