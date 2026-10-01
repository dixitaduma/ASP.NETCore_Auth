namespace EcommerceDemo.Areas.User.ApiEndPoints
{
	public class UserApiEndPoints
	{
		public string UserRegistration = "AuthApi/UserRegistration";
		public string Login = "AuthApi/Login";
		public string UserOtpVerificaton = "AuthApi/OtpVerification";
		public string UserGenerateOtp = "AuthApi/GenerateOtp";
		public string UserForgetPassword = "AuthApi/ForgetPassword";
		public string GetUserProfile = "UserApi/GetOneUser?id=";
		public string UserUpdateProfile = "UserApi/UpdateUserDetails";
		public string DeleteUserAccount = "UserApi/DeleteUser";

	}
}
