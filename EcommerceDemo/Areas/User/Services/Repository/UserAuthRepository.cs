using EcommerceDemo.Areas.User.Services.IRepository;
using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Models;
using Azure;
using System.Net;
using EcommerceDemo.Areas.User.Models;
using EcommerceDemo.Areas.User.ApiEndPoints;
using EcommerceDemo._commanmethods;
namespace EcommerceDemo.Areas.User.Services.Repository
{
	public class UserAuthRepository : IUserAuthInterface
	{
		private readonly HttpClient _httpClient;
		private readonly UserApiEndPoints _userApiEndPoints;
		private readonly CommanMethods _commanMethods;
		public UserAuthRepository(HttpClient httpClient, UserApiEndPoints userApiEndPoints, CommanMethods commanMethods)
		{
			_httpClient = httpClient;
			_userApiEndPoints = userApiEndPoints;
			_commanMethods = commanMethods;
		}
		public async Task<ResponseModel<object>> UserRegistration(VMRegisterUser user)
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
					user.ProfileImage = _commanMethods.defaultUserProfileImageName;
				}
				var response = await _httpClient.PostAsJsonAsync(_userApiEndPoints.UserRegistration, user);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadFromJsonAsync<ResponseModel<object>>();
					//Console.WriteLine(result.messages);
					return result!;
				}
				return new ResponseModel<object>()
				{
					statusCode = (int)response.StatusCode,
					messages = _commanMethods.registrationFailed,
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
		public async Task<ResponseModel<object>> GenerateOtp(VMGenerateOtp userdata)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync(_userApiEndPoints.UserGenerateOtp, userdata);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadFromJsonAsync<ResponseModel<object>>();
					//Console.WriteLine(result.messages);
					return result!;
				}
				return new ResponseModel<object>()
				{
					statusCode = (int)response.StatusCode,
					messages = _commanMethods.otpGenerateFailed,
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
		public async Task<ResponseModel<object>> OtpVerification(VMOtpModel otp)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync(_userApiEndPoints.UserOtpVerificaton, otp);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadFromJsonAsync<ResponseModel<object>>();
					//Console.WriteLine(result.messages);
					return result!;
				}
				return new ResponseModel<object>()
				{
					statusCode = (int)response.StatusCode,
					messages = _commanMethods.otpValidateFailed,
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
		public async Task<ResponseModel<object>> ForgetPassword(VMForgetPassword userdata)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync(_userApiEndPoints.UserForgetPassword, userdata);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadFromJsonAsync<ResponseModel<object>>();
					//Console.WriteLine(result.messages);
					return result!;
				}
				return new ResponseModel<object>()
				{
					statusCode = (int)response.StatusCode,
					messages = _commanMethods.userPasswordUpdateFailed,
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
		public async Task<ResponseModel<VMUserResponseData>> Login(VMUserLogin user)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync(_userApiEndPoints.Login, user);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadFromJsonAsync<ResponseModel<VMUserResponseData>>();
					//Console.WriteLine(result.messages);
					return result!;
				}
				return new ResponseModel<VMUserResponseData>()
				{
					statusCode = (int)response.StatusCode,
					messages = _commanMethods.loginFailed,
					data = null
				};
			}
			catch (Exception ex)
			{
				return new ResponseModel<VMUserResponseData>()
				{
					statusCode = (int)HttpStatusCode.InternalServerError,
					messages = $" An error occurred {ex.Message}",
					data = null
				};
			}
			//return null;
		}
	}
}
