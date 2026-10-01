namespace EcommerceDemo.Models.Entity_Models
{
	public class Category
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public ICollection<Product> Products { get; set; }
		public bool IsDeleted { get; set; }
		public int? DeletedByUserId { get; set; }
		public User DeletedByUser { get; set; }
		public int? UpdatedByUserId { get; set; }
		public User UpdatedByUser { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
	}
}
