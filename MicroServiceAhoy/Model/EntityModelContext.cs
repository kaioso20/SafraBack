using System.Data.Entity;

namespace MicroService.Model
{
    public class EntityModelContext : DbContext
    {
        public EntityModelContext() : base("SafraDB")
        {
        }

        public DbSet<Safra> Safra { get; set; }
    }
}
