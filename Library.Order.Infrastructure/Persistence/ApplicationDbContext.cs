using System.Reflection;
using Library.Order.Application.Interfaces;
using Library.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Order.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Domain.Entities.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Domain.Entities.Order>().HasMany(o => o.OrderItems).WithOne().HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Domain.Entities.Order>().HasOne<Payment>().WithOne().HasForeignKey<Payment>(p => p.OrderId).IsRequired(false);
            base.OnModelCreating(modelBuilder);
        }

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken) => await base.SaveChangesAsync(cancellationToken);
    }
}