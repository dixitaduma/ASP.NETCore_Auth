using EcommerceDemo.Areas.User.Models;
using EcommerceDemo.Models;

namespace EcommerceDemo.Areas.User.Services.IRepository
{
	public interface IManageUserInterface
	{
		Task<ResponseModel<VMUserProfile>> GetUserProfile(int id);
		Task<ResponseModel<object>> UpdateUserProfile(VMUserProfile profile);
		Task<ResponseModel<object>> DeleteUserProfile(VMDeleteUserAccount deleteUerData);
	}
}
