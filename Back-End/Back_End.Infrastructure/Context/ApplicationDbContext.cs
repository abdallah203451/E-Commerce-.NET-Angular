using Back_End.Domain.Entities;
using Back_End.Infrastructure.Context.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Infrastructure.Context
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration(new RoleConfiguration());
			builder.ApplyConfiguration(new CategoryConfiguration());
		}
	}
}
