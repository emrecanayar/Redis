using Microsoft.EntityFrameworkCore;
using RealTimeNotificationApp.Domain.Entities;

namespace RealTimeNotificationApp.Persistence.Contexts
{
    // Entity Framework Core için veritabanı bağlamı
    public class RealTimeNotificationContext : DbContext
    {
        public RealTimeNotificationContext(DbContextOptions<RealTimeNotificationContext> options)
         : base(options)
        {
        }

        // Entity Framework Core için veritabanı bağlamı
        public DbSet<Notification> Notifications { get; set; }
    }
}
