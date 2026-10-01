namespace EcommerceDemo.Models.Entity_Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public decimal Price { get; set; }
		public int StockQuantity { get; set; }
		public ICollection<ProductImage> ProductImages { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public bool IsDeleted { get; set; }
		public int? DeletedByUserId { get; set; }
		public User DeletedByUser { get; set; }
		public int? UpdatedByUserId { get; set; }
		public User UpdatedByUser { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }
	}
}
