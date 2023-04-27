using Microsoft.EntityFrameworkCore;
using UnityCommunication.Models;

namespace UnityCommunication.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<MessageModel> messageModels { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
