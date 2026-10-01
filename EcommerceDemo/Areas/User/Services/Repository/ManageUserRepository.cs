using System.Net;
using EcommerceDemo._commanmethods;
using EcommerceDemo.Areas.User.ApiEndPoints;
using EcommerceDemo.Areas.User.Models;
using EcommerceDemo.Areas.User.Services.IRepository;
using EcommerceDemo.Models;
namespace EcommerceDemo.Areas.User.Services.Repository
{
	public class ManageUserRepository : IManageUserInterface
	{
		private readonly HttpClient _httpClient;
		private readonly UserApiEndPoints _userApiEndPoints;
		private readonly CommanMethods _commanMethods;
		public ManageUserRepository(HttpClient httpClient, UserApiEndPoints userApiEndPoints, CommanMethods commanMethods)
		{
			_httpClient = httpClient;
			_commanMethods = commanMethods;
			_userApiEndPoints = userApiEndPoints;
		}
		public async Task<ResponseModel<VMUserProfile>> GetUserProfile(int id)
		{
			try
			{
				var response = await _httpClient.GetFromJsonAsync<ResponseModel<VMUserProfile>>($"{_userApiEndPoints.GetUserProfile}{id}");
				if (response != null && response.statusCode >= 200 && response.statusCode < 300)
				{
					return response;
				}
				return new ResponseModel<VMUserProfile>()
				{
					statusCode = (int)response.statusCode,
					messages = _commanMethods.failedToGetUserProfile,
					data = null
				};
			}
			catch (Exception ex)
			{
				return new ResponseModel<VMUserProfile>()
				{
					statusCode = (int)HttpStatusCode.InternalServerError,
					messages = $" An error occurred {ex.Message}",
					data = null
				};
			}
		}
		public async Task<ResponseModel<object>> UpdateUserProfile(VMUserProfile user)
		{
			try
			{
				if (user.ProfileImageFile != null && user.ProfileImageFile.Length > 0)
				{
					var fileName = Guid.NewGuid() + Path.GetExtension(user.ProfileImageFile.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ProfileImages", fileName);
					using (var steam = new FileStream(filePath, FileMode.Create))
					{
						await user.ProfileImageFile.CopyToAsync(steam);
					}
					user.ProfileImage = fileName;
				}
				else
				{
					if (string.IsNullOrEmpty(user.ProfileImage))
					{
						user.ProfileImage = _commanMethods.defaultUserProfileImageName;
					}
				}
				var response = await _httpClient.PutAsJsonAsync(_userApiEndPoints.UserUpdateProfile, user);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadFromJsonAsync<ResponseModel<object>>();
					//Console.WriteLine(result.messages);
					return result!;
				}
				return new ResponseModel<object>()
				{
					statusCode = (int)response.StatusCode,
					messages = _commanMethods.UpdateUserDetailsFailed,
					data = response.Content
				};
			}
			catch (Exception ex)
			{
				return new ResponseModel<object>()
				{
					statusCode = (int)HttpStatusCode.InternalServerError,
					messages = $" An error occurred {ex.Message}",
					data = null
				};
			}
			//return null;
		}
		public async Task<ResponseModel<object>> DeleteUserProfile(VMDeleteUserAccount deleteUerData)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync(_userApiEndPoints.DeleteUserAccount, deleteUerData);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadFromJsonAsync<ResponseModel<object>>();
					//Console.WriteLine(result.messages);
					return result!;
				}
				return new ResponseModel<object>()
				{
					statusCode = (int)response.StatusCode,
					messages = _commanMethods.UpdateUserDetailsFailed,
					data = response.Content
				};
			}
			catch (Exception ex)
			{
				return new ResponseModel<object>()
				{
					statusCode = (int)HttpStatusCode.InternalServerError,
					messages = $" An error occurred {ex.Message}",
					data = null
				};
			}
		}
	}
}
