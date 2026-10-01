namespace EcommerceDemo.Models.Entity_Models
{
	public class Order
	{
		public int OrderId { get; set; }
		public string OrderStatus { get; set; } = "Pending";
		public string PaymentStatus { get; set; } = "Unpaid";
		public string ShippingAddress { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalAmount { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; }
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
