using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Models;
using EcommerceDemo.Repository.Interface;
using EcommerceDemo.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System.Net;
using EcommerceDemo._commanmethods;
using Azure;
using EcommerceDemo.Models.Entity_Models;
using System.Security.Cryptography;
namespace EcommerceDemo.Repository.Implementation
{
	public class UserRepository : IUserInterface
	{
		private readonly AppDbContext _appDbContext;
		private readonly CommanMethods _commanMethods;
		public UserRepository(AppDbContext appDbContext, CommanMethods commanMethods)
		{
			_appDbContext = appDbContext;
			_commanMethods = commanMethods;
		}
		//public ResponseModel<object> response = new ResponseModel<object>();
		public async Task<ResponseModel<List<DTOUserResponse>>> GetAllUsers()
		{
			ResponseModel<List<DTOUserResponse>> response = new ResponseModel<List<DTOUserResponse>>();
			try
			{
				var userResponceList = new List<DTOUserResponse>();
				var userList = await _appDbContext.Users.Include(u => u.Role).Where(u => u.IsDeleted == false).ToListAsync();
				if (userList != null)
				{
					foreach (var user in userList)
					{
						var oneuser = new DTOUserResponse()
						{
							UserId = user.UserId,
							UserName = user.UserName,
							UserEmail = user.UserEmail,
							Gender = user.Gender,
							DateOfBirth = DateOnly.FromDateTime(user.DateOfBirth),
							PhoneNumber = user.PhoneNumber,
							ProfileImage = user.ProfileImage,
							Address = user.Address,
							City = user.City,
							State = user.State,
							PostalCode = user.PostalCode,
							RoleName = user.Role.RoleName
						};
						userResponceList.Add(oneuser);
					}
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.getUsersListSuccess;
					response.data = userResponceList;
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.getUsersListfailed;
					response.data = userResponceList;
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
		public async Task<ResponseModel<List<DTOUserResponse>>> GetAllActiveUsers()
		{
			ResponseModel<List<DTOUserResponse>> response = new ResponseModel<List<DTOUserResponse>>();
			try
			{
				var userResponceList = new List<DTOUserResponse>();
				var userList = await _appDbContext.Users.Include(u => u.Role).Where(u => u.IsDeleted == false).ToListAsync();
				if (userList != null && userList.Count != 0)
				{
					foreach (var user in userList)
					{
						var oneuser = new DTOUserResponse()
						{
							UserId = user.UserId,
							UserName = user.UserName,
							UserEmail = user.UserEmail,
							Gender = user.Gender,
							DateOfBirth = DateOnly.FromDateTime(user.DateOfBirth),
							PhoneNumber = user.PhoneNumber,
							ProfileImage = user.ProfileImage,
							Address = user.Address,
							City = user.City,
							State = user.State,
							PostalCode = user.PostalCode,
							RoleName = user.Role.RoleName
						};
						userResponceList.Add(oneuser);
					}
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.getActiveUsersListSuccess;
					response.data = userResponceList;
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.getActiveUsersListfailed;
					response.data = userResponceList;
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
		public async Task<ResponseModel<List<DTOUserResponse>>> GetAllDeactiveUsers()
		{
			ResponseModel<List<DTOUserResponse>> response = new ResponseModel<List<DTOUserResponse>>();
			try
			{
				var userResponceList = new List<DTOUserResponse>();
				var userList = await _appDbContext.Users.Include(u => u.Role).Where(u => u.IsDeleted == true).ToListAsync();
				if (userList != null && userList.Count != 0)
				{
					foreach (var user in userList)
					{
						var oneuser = new DTOUserResponse()
						{
							UserId = user.UserId,
							UserName = user.UserName,
							UserEmail = user.UserEmail,
							Gender = user.Gender,
							DateOfBirth = DateOnly.FromDateTime(user.DateOfBirth),
							PhoneNumber = user.PhoneNumber,
							ProfileImage = user.ProfileImage,
							Address = user.Address,
							City = user.City,
							State = user.State,
							PostalCode = user.PostalCode,
							RoleName = user.Role.RoleName
						};
						userResponceList.Add(oneuser);
					}
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.getDeactiveUsersListSuccess;
					response.data = userResponceList;
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.getDeactiveUsersListfailed;
					response.data = userResponceList;
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
		public async Task<ResponseModel<DTOUserResponse>> GetOneUser(int id)
		{
			ResponseModel<DTOUserResponse> response = new ResponseModel<DTOUserResponse>();
			try
			{
				var userData = await _appDbContext.Users.Include(u => u.Role).Where(u => u.IsDeleted == false).FirstOrDefaultAsync(u => u.UserId == id);
				if (userData != null)
				{
					var oneuser = new DTOUserResponse()
					{
						UserId = userData.UserId,
						UserName = userData.UserName,
						UserEmail = userData.UserEmail,
						Gender = userData.Gender,
						DateOfBirth = DateOnly.FromDateTime(userData.DateOfBirth),
						PhoneNumber = userData.PhoneNumber,
						ProfileImage = userData.ProfileImage,
						Address = userData.Address,
						City = userData.City,
						State = userData.State,
						PostalCode = userData.PostalCode,
						RoleName = userData.Role.RoleName
					};
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.getUsersListSuccess;
					response.data = oneuser;
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.getUsersListfailed;
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
		public async Task<ResponseModel<Object>> DeleteUserAccount(DTODeleteUserAccount DeleteUser)
		{
			ResponseModel<Object> response = new ResponseModel<Object>();
			try
			{
				var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserId == DeleteUser.UserId);
				if (user == null)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.userNotExist;
					return response;
				}
				user.DeletedAt = DeleteUser.DeletedAt ?? DateTime.Now;
				user.DeletedByUserId = DeleteUser.DeletedByUserId;
				user.IsDeleted = true;
				var result = await _appDbContext.SaveChangesAsync();
				if (result > 0)
				{
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.deleteUserSuccess;
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.deleteUserfailed;
				}
			}
			catch (Exception ex)
			{
				response.statusCode = (int)HttpStatusCode.InternalServerError;
				response.messages = $"An error occurred: {ex.Message}";
			}
			return response;
		}
		public async Task<ResponseModel<Object>> UpdateUserDetails(DTOUpdateUserProfile user)
		{
			ResponseModel<Object> response = new ResponseModel<Object>();
			try
			{
				var existingUser = await _appDbContext.Users.FindAsync(user.UserId);
				if (existingUser == null)
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.userNotExist;
					return response;
				}
				if (existingUser.UserEmail == user.UserEmail
					&& existingUser.UserName == user.UserName
					&& existingUser.Gender == user.Gender
					&& existingUser.DateOfBirth == user.DateOfBirth.ToDateTime(TimeOnly.MinValue)
					&& existingUser.PhoneNumber == user.PhoneNumber
					&& existingUser.ProfileImage == user.ProfileImage
					&& existingUser.Address == user.Address
					&& existingUser.City == user.City
					&& existingUser.State == user.State
					&& existingUser.PostalCode == user.PostalCode)
				{
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.UpdateUserDetailsSuccess;
					return response;
				}
				//Bind Data to User Model
				existingUser.UserName = user.UserName;
				existingUser.UserEmail = user.UserEmail;
				existingUser.Gender = user.Gender;
				existingUser.DateOfBirth = user.DateOfBirth.ToDateTime(TimeOnly.MinValue);
				existingUser.PhoneNumber = user.PhoneNumber;
				existingUser.ProfileImage = user.ProfileImage;
				existingUser.Address = user.Address;
				existingUser.City = user.City;
				existingUser.State = user.State;
				existingUser.PostalCode = user.PostalCode;
				existingUser.UpdatedAt = DateTime.Now;
				existingUser.UpdatedByUserId = user.UpdatedByUserId;
				//Store Userdata to database
				var saveResult = await _appDbContext.SaveChangesAsync();
				if (saveResult > 0)
				{
					response.statusCode = (int)HttpStatusCode.OK;
					response.messages = _commanMethods.UpdateUserDetailsSuccess;
				}
				else
				{
					response.statusCode = (int)HttpStatusCode.BadRequest;
					response.messages = _commanMethods.UpdateUserDetailsFailed;
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
