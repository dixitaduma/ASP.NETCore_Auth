using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Models;
using EcommerceDemo.Repository.Interface;
using EcommerceDemo.ApplicationDbContext;
using EcommerceDemo.Models.Entity_Models;
using EcommerceDemo._commanmethods;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Mvc;
namespace EcommerceDemo.Repository.Implementation
{
	public class AuthRepository : IAuthInterface
	{
		private readonly AppDbContext _appDbContext;
		private readonly CommanMethods _commanMethods;
		private readonly IEmailServiceInterface _emailService;
		public AuthRepository(AppDbContext appDbContext, CommanMethods commanMethods, IEmailServiceInterface emailService)
		{
			_appDbContext = appDbContext;
			_commanMethods = commanMethods;
			_emailService = emailService;
		}
		public ResponseModel<object> response = new ResponseModel<object>();
		public async Task<ResponseModel<object>> UserRegistration(DTOUserRegistration user)
		{
			try
			{
				//UserName And Paasword in lower case
				user.UserEmail = user.UserEmail.ToLower();
				user.UserName = user.UserName.ToLower();
				//Otp Generate 
				string otp = _commanMethods.genrateOtp();
				//check user exist 
				var IsUserAreadyExists = await _appDbContext.Users.Include(u => u.DeletedByUser)
																  .ThenInclude(du => du.Role)
																  .FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail || u.UserName == user.UserName);
				if (IsUserAreadyExists != null)
				{
					//Check Users account is deleted by Admin
					if (IsUserAreadyExists.IsDeleted == true && IsUserAreadyExists.DeletedByUser.Role.RoleName == "Admin")
					{
						response.statusCode = (int)HttpStatusCode.BadRequest;
						response.messages = _commanMethods.emailOrUserNameAlreadyExist;
						response.data = null;
						return response;
					}
					//Check Users account is deleted by user itself than Update the user data 
					else if (IsUserAreadyExists.IsDeleted == true && IsUserAreadyExists.DeletedByUserId == IsUserAreadyExists.UserId)
					{
						IsUserAreadyExists.UserName = user.UserName;
						IsUserAreadyExists.UserEmail = user.UserEmail;
						IsUserAreadyExists.Gender = user.Gender;
						IsUserAreadyExists.Password = _commanMethods.Encrypt(user.Password);
						IsUserAreadyExists.DateOfBirth = user.DateOfBirth.ToDateTime(TimeOnly.MinValue);
						IsUserAreadyExists.PhoneNumber = user.PhoneNumber;
						IsUserAreadyExists.ProfileImage = user.ProfileImage;
						IsUserAreadyExists.Address = user.Address;
						IsUserAreadyExists.City = user.City;
						IsUserAreadyExists.State = user.State;
						IsUserAreadyExists.PostalCode = user.PostalCode;
						IsUserAreadyExists.RoleId = user.RoleId;
						IsUserAreadyExists.Otp = otp;
						IsUserAreadyExists.IsDeleted = false;
					}
				}
				else
				{
					//Bind Data to User Model for new user
					var userData = new User
					{
						UserName = user.UserName,
						UserEmail = user.UserEmail,
						Gender = user.Gender,
						Password = _commanMethods.Encrypt(user.Password),
						DateOfBirth = user.DateOfBirth.ToDateTime(TimeOnly.MinValue),
						PhoneNumber = user.PhoneNumber,
						ProfileImage = user.ProfileImage,
						Address = user.Address,
						City = user.City,
						State = user.State,
						PostalCode = user.PostalCode,
						RoleId = user.RoleId,
						Otp = otp,
					};
					await _appDbContext.Users.AddAsync(userData);
				}
				//Store Userdata to database
				var saveResult = await _appDbContext.SaveChangesAsync();
				if (saveResult > 0)
				{
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.registrationSuccess;
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.registrationFailed;
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
		public async Task<ResponseModel<DTOUserResponse>> Login(DTOUserLogin user)
		{
			ResponseModel<DTOUserResponse> response = new ResponseModel<DTOUserResponse>();
			try
			{
				var userData = await _appDbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail || u.UserName == user.UserEmail);
				if (userData != null)
				{
					var userResponceData = new DTOUserResponse
					{
						UserId = userData.UserId,
						UserName = userData.UserName,
						UserEmail = userData.UserEmail,
						Password = userData.Password,
						Gender = userData.Gender,
						DateOfBirth = DateOnly.FromDateTime(userData.DateOfBirth),
						PhoneNumber = userData.PhoneNumber,
						ProfileImage = userData.ProfileImage,
						Address = userData.Address,
						City = userData.City,
						State = userData.State,
						PostalCode = userData.PostalCode,
						RoleName = userData.Role.RoleName,
						IsDeleted = userData.IsDeleted,
						IsOtpVerified = userData.IsOtpVerified
					};
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.userDataRetriveSuccess;
					response.data = userResponceData;
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.userDataRetriveFailed;
					response.data = null;
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = _commanMethods.enternalServerError;
				response.data = null;
			}
			return response;
		}
		public async Task<ResponseModel<object>> OtpVrification(DTOOtpVerificationModel Otpdata)
		{
			try
			{
				Otpdata.UserEmail = Otpdata.UserEmail.ToLower();
				var result = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserEmail == Otpdata.UserEmail);
				if (result != null)
				{
					if (result.IsOtpVerified)
					{
						response.statusCode = (int)HttpStatusCode.OK;
						response.messages = _commanMethods.otpValidateSuccess;
						return response;
					}
					else if (result.Otp == Otpdata.Otp)
					{
						result.IsOtpVerified = true;
						var save = await _appDbContext.SaveChangesAsync();
						if (save > 0)
						{
							response.statusCode = (int)HttpStatusCode.OK;
							response.messages = _commanMethods.otpValidateSuccess;
						}
						else
						{
							response.statusCode = (int)HttpStatusCode.BadRequest;
							response.messages = _commanMethods.otpValidateFailed;
						}
					}
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.invalidUserNameOrEmail;
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
		public async Task<ResponseModel<object>> GenerateOtp(DTOGenerateOtp userdata)
		{
			try
			{
				var result = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserEmail == userdata.UserEmail);
				if (result != null)
				{
					var otp = _commanMethods.genrateOtp();
					result.Otp = otp;
					////Otp Send on Email 
					//var sendEmailOtp = new DTOSendEmailOtp()
					//    {
					//    ToEmail = user.UserEmail,
					//    Subject = _commanMethods.otpSubject,
					//    Body = otp
					//    };
					//var otpSendResponce = _emailService.SendOtpEmailAsync(sendEmailOtp);
					var save = await _appDbContext.SaveChangesAsync();
					if (save > 0)
					{
						response.statusCode = (int)HttpStatusCode.OK;
						response.messages = _commanMethods.otpGenrateSuccess;
					}
					else
					{
						response.statusCode = (int)HttpStatusCode.BadRequest;
						response.messages = _commanMethods.otpGenrateFailed;
					}
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.userNotExist;
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
		public async Task<ResponseModel<object>> ForgetPassword(DTOForgatePassword user)
		{
			try
			{
				if (!user.IsOtpVerified)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.otpNotVerified;
					return response;
				}
				user.UserEmail = user.UserEmail.ToLower();
				user.Password = _commanMethods.Encrypt(user.Password);
				var UserData = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail);
				if (UserData != null)
				{
					bool ispasswordmath = user.Password == UserData.Password;
					if (!ispasswordmath)
					{
						UserData.Password = user.Password;
						var save = await _appDbContext.SaveChangesAsync();
						if (save > 0)
						{
							response.statusCode = (int)HttpStatusCode.OK;
							response.messages = _commanMethods.userPasswordUpdateSuccess;
						}
						else
						{
							response.statusCode = (int)HttpStatusCode.BadRequest;
							response.messages = _commanMethods.userPasswordUpdateFailed;
						}
					}
					else
					{
						response.statusCode = (int)HttpStatusCode.OK;
						response.messages = _commanMethods.userPasswordUpdateSuccess;
					}
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.userNotExist;
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
	}
}
