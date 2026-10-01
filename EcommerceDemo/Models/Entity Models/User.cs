using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace EcommerceDemo.Models.Entity_Models
{
	public class User
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string UserEmail { get; set; }
		public string Gender { get; set; }
		public string Password { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string PhoneNumber { get; set; }
		public string ProfileImage { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		[MaxLength(6)]
		public string Otp { get; set; }
		public int RoleId { get; set; }
		public Role Role { get; set; }
		public int? DeletedByUserId { get; set; }
		public User DeletedByUser { get; set; }
		public int? UpdatedByUserId { get; set; }
		public User UpdatedByUser { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public bool IsOtpVerified { get; set; } = false;
		public bool IsDeleted { get; set; }
		public ICollection<CartItem> CartItems { get; set; }
		public ICollection<FavoriteItem> FavoriteItems { get; set; }
		public ICollection<Order> Orders { get; set; }
	}
}
