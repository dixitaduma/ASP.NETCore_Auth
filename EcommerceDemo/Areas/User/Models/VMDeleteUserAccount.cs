namespace EcommerceDemo.Areas.User.Models
{
	public class VMDeleteUserAccount
	{
		public int UserId { get; set; }
		public DateTime? DeletedAt { get; set; }
		public int? DeletedByUserId { get; set; }
	}
}
