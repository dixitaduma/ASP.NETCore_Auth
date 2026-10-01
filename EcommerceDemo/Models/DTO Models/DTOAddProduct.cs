using EcommerceDemo.Models.Entity_Models;
using Microsoft.Identity.Client;

namespace EcommerceDemo.Models.DTO_Models
{
	public class DTOAddProduct
	{

		public string ProductName { get; set; }
		public string ProductDescription { get; set; }
		public decimal Price { get; set; }
		public int StockQuantity { get; set; }
		public List<DTOAddProductImage> ProductImages { get; set; }
		public int CategoryId { get; set; }

	}
}
