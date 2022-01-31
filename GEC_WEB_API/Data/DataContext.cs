using Microsoft.EntityFrameworkCore;

namespace GEC_WEB_API.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public DbSet<FacultyDetails> FacultyDetail { get; set; }
    }
}
