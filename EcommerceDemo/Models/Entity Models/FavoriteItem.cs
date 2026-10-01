namespace EcommerceDemo.Models.Entity_Models
{
	public class FavoriteItem
	{
		public int FavoriteItemId { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
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
