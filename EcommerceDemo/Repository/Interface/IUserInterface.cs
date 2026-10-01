using EcommerceDemo.Models;
using EcommerceDemo.Models.DTO_Models;

namespace EcommerceDemo.Repository.Interface
{
	public interface IUserInterface
	{
		Task<ResponseModel<List<DTOUserResponse>>> GetAllUsers();
		Task<ResponseModel<List<DTOUserResponse>>> GetAllActiveUsers();
		Task<ResponseModel<List<DTOUserResponse>>> GetAllDeactiveUsers();
		Task<ResponseModel<DTOUserResponse>> GetOneUser(int id);
		Task<ResponseModel<Object>> DeleteUserAccount(DTODeleteUserAccount DeleteUser);
		Task<ResponseModel<Object>> UpdateUserDetails(DTOUpdateUserProfile user);
	}
}
