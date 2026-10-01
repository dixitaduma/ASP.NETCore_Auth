using System.Net;
using EcommerceDemo._commanmethods;
using EcommerceDemo.Models;
using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace EcommerceDemo.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductApiController : ControllerBase
	{
		private readonly CommanMethods _commanMethods;
		private readonly IProductInterface _productRepo;
		public ResponseModel<object> response = new ResponseModel<object>();
		public ProductApiController(IProductInterface productRepo, CommanMethods commanMethods)
		{
			_productRepo = productRepo;
			_commanMethods = commanMethods;
		}
		[HttpPost("AddProduct")]
		public async Task<IActionResult> AddProduct(DTOAddProduct ProductData)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await _productRepo.AddProduct(ProductData);
					return Ok(result);
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.modelStateInvalid;
					response.data = ModelState;
					return BadRequest(response);
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("UpdateProduct")]
		public async Task<IActionResult> UpdateProduct(DTOUpdateProduct ProductData)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await _productRepo.UpdateProduct(ProductData);
					return Ok(result);
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.modelStateInvalid;
					response.data = ModelState;
					return BadRequest(response);
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
		[HttpPost("DeleteProduct")]
		public async Task<IActionResult> DeleteProduct(DTODeleteProduct ProductData)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var result = await _productRepo.DeleteProduct(ProductData);
					return Ok(result);
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.modelStateInvalid;
					response.data = ModelState;
					return BadRequest(response);
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = ex.Message;
				return BadRequest(response);
			}
		}
	}
}
