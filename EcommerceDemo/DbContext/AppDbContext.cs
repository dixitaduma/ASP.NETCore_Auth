using EcommerceDemo.Models.Entity_Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDemo.ApplicationDbContext
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categorys { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<CartItem> CartItems { get; set; }
		public DbSet<FavoriteItem> FavoriteItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			//User Table
			modelBuilder.Entity<User>().Property(u => u.IsOtpVerified).HasDefaultValue(false);

			modelBuilder.Entity<User>().Property(u => u.IsDeleted).HasDefaultValue(false);
			modelBuilder.Entity<ProductImage>().Property(u => u.IsDeleted).HasDefaultValue(false);
			modelBuilder.Entity<Category>().Property(u => u.IsDeleted).HasDefaultValue(false);
			modelBuilder.Entity<CartItem>().Property(u => u.IsDeleted).HasDefaultValue(false);
			modelBuilder.Entity<FavoriteItem>().Property(u => u.IsDeleted).HasDefaultValue(false);
			modelBuilder.Entity<Order>().Property(u => u.IsDeleted).HasDefaultValue(false);
			modelBuilder.Entity<OrderItem>().Property(u => u.IsDeleted).HasDefaultValue(false);


			modelBuilder.Entity<User>()
				.HasOne(u => u.DeletedByUser)
				.WithMany()
				.HasForeignKey(u => u.DeletedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
				.HasOne(u => u.UpdatedByUser)
				.WithMany()
				.HasForeignKey(u => u.UpdatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
				.Property(u => u.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			//Role Table
			modelBuilder.Entity<Role>()
				.HasOne(r => r.DeletedByUser)
				.WithMany()
				.HasForeignKey(r => r.DeletedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<Role>()
				.HasOne(r => r.UpdatedByUser)
				.WithMany()
				.HasForeignKey(r => r.UpdatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Role>()
			  .Property(r => r.CreatedAt)
			  .HasDefaultValueSql("GETUTCDATE()");


			//ProductImage Table
			modelBuilder.Entity<ProductImage>()
				.HasOne(p => p.DeletedByUser)
				.WithMany()
				.HasForeignKey(p => p.DeletedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<ProductImage>()
				.HasOne(p => p.UpdatedByUser)
				.WithMany()
				.HasForeignKey(p => p.UpdatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ProductImage>()
				.Property(p => p.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			//Product Table
			modelBuilder.Entity<Product>()
				.HasOne(p => p.DeletedByUser)
				.WithMany()
				.HasForeignKey(p => p.DeletedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<Product>()
				.HasOne(p => p.UpdatedByUser)
				.WithMany()
				.HasForeignKey(p => p.UpdatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Product>()
				.Property(p => p.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");


			//Category Table
			modelBuilder.Entity<Category>()
				.HasOne(c => c.DeletedByUser)
				.WithMany()
				.HasForeignKey(c => c.DeletedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<Category>()
				.HasOne(c => c.UpdatedByUser)
				.WithMany()
				.HasForeignKey(c => c.UpdatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Category>()
				.Property(c => c.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");


			//CartItem Table
			modelBuilder.Entity<CartItem>()
				.HasOne(c => c.DeletedByUser)
				.WithMany()
				.HasForeignKey(c => c.DeletedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<CartItem>()
				.HasOne(c => c.UpdatedByUser)
				.WithMany()
				.HasForeignKey(c => c.UpdatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<CartItem>()
				.Property(c => c.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");




			//FavoriteItem Table
			modelBuilder.Entity<FavoriteItem>()
				.HasOne(f => f.DeletedByUser)
				.WithMany()
				.HasForeignKey(f => f.DeletedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<FavoriteItem>()
				.HasOne(f => f.UpdatedByUser)
				.WithMany()
				.HasForeignKey(f => f.UpdatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<FavoriteItem>()
				.Property(f => f.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");



			//Order Table
			modelBuilder.Entity<Order>()
				.HasOne(o => o.DeletedByUser)
				.WithMany()
				.HasForeignKey(o => o.DeletedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<Order>()
				.HasOne(o => o.UpdatedByUser)
				.WithMany()
				.HasForeignKey(o => o.UpdatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<Order>()
				.Property(o => o.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()");

			//OrderItem Table
			modelBuilder.Entity<OrderItem>()
				.HasOne(o => o.DeletedByUser)
				.WithMany()
				.HasForeignKey(o => o.DeletedByUserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<OrderItem>()
				.HasOne(o => o.UpdatedByUser)
				.WithMany()
				.HasForeignKey(o => o.UpdatedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<OrderItem>()
			   .Property(o => o.CreatedAt)
			   .HasDefaultValueSql("GETUTCDATE()");
		}
	}
}
