namespace EcommerceDemo.Models.Entity_Models
{
	public class ProductImage
	{
		public int ProductImageId { get; set; }
		public string ProductImageName { get; set; }
		public ImageType ProductImageType { get; set; }
		public int ProductId { get; set; }
		public Product Product { get; set; }
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
