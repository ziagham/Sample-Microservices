using System;
using OrderApi.Domain.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace OrderApi.Data.v1.Database
{
    public class OrderContext : DbContext
    {
        public OrderContext() {}

        public OrderContext(DbContextOptions<OrderContext> options) : base(options) {}

        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.CustomerFullName).IsRequired();
            });
        }
    }
}