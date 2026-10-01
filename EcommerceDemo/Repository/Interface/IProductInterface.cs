using EcommerceDemo.Models;
using EcommerceDemo.Models.DTO_Models;

namespace EcommerceDemo.Repository.Interface
{
	public interface IProductInterface
	{
		Task<ResponseModel<object>> AddProduct(DTOAddProduct Productdata);
		Task<ResponseModel<object>> UpdateProduct(DTOUpdateProduct Productdata);
		Task<ResponseModel<object>> DeleteProduct(DTODeleteProduct Productdata);
		//Task<ResponseModel<List<DTOProductResponce>>> GetAllProduct();
	}
}
