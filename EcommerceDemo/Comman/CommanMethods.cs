using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Security.Cryptography;
namespace EcommerceDemo._commanmethods
{
	public class CommanMethods
	{
		public string registrationSuccess = "User Register Successfuly";
		public string registrationFailed = "User Registration  Failed";
		public string emailOrUserNameAlreadyExist = "User already Exist, Please try again with defferent Username or Email";
		public string invalidUserNameOrEmail = "Please enter valid user name or email";
		public string ageValidationGreterThan18Years = "User  must be at least 18 years old.";
		public string defaultUserProfileImageName = "Default.png";
		public string loginSuccess = "User Login Successfuly";
		public string loginFailed = "Faild to login";
		public string incorrectPassword = "Incorrect Password";
		public string userNotActive = "User is not active please contact support team";
		public string otpGenerateSuccess = "Otp Generated Successfuly";
		public string otpGenerateFailed = "Faild to Generate Otp";
		public string otpValidateSuccess = "Otp Validate Successfuly";
		public string otpValidateFailed = "Invalid Otp";
		public string otpGenrateSuccess = "Otp generated successfuly";
		public string otpGenrateFailed = "Failed to generate otp";
		public string otpNotVerified = "Please complete opt verification";
		public string otpSubject = "Otp Verification";
		public string otpInvalidData = "Please provide valid otpdata";
		public string modelStateInvalid = "ModelState is invalid";
		public string getUsersListSuccess = "A list of users is successfully retrieved";
		public string getUsersListfailed = "No users found";
		public string getActiveUsersListSuccess = "A list of active users is successfully retrieved";
		public string getActiveUsersListfailed = "No active users found";
		public string getDeactiveUsersListSuccess = "A list of active users is successfully retrieved";
		public string getDeactiveUsersListfailed = "No active users found";
		public string deleteUserSuccess = "User deleted successfuly ";
		public string deleteUserfailed = "Failed to delete user";
		public string UpdateUserDetailsSuccess = "User updated successfuly";
		public string UpdateUserDetailsFailed = "Failed to update user";
		public string userFoundSuccess = "User found successfuly";
		public string userNotExist = "No user found";
		public string failedToGetUserProfile = "Failed to get user profile";
		public string productAlreadyExists = "Product with same name and category is already exists";
		public string addProductSuccess = "Product added successfuly";
		public string addProductFailed = "Failed to add product";
		public string productNotFound = "Product not found";
		public string updateProductSuccess = "Product updated successfuly";
		public string updateProductFailed = "Failed to update product";
		public string deleteProductSuccess = "Product deleted successfuly";
		public string deleteProductFailed = "Failed to deleted product";
		public string userPasswordUpdateSuccess = "Password updated successfuly";
		public string userPasswordUpdateFailed = "Failed to update password";
		//Repo Messages
		public string userDataRetriveSuccess = "User find successfuly";
		public string userDataRetriveFailed = "No user fount with this email or username";
		public string enternalServerError = "We are sorry, but something went wrong";

		public string genrateOtp()
		{
			string otp = new Random().Next(100000, 999999).ToString();
			return otp;
		}

		public string HashPassword(string password, byte[] key)
		{
			using var pbkdf2 = new Rfc2898DeriveBytes(password, key, 100_000, HashAlgorithmName.SHA256);
			byte[] hash = pbkdf2.GetBytes(32);
			return Convert.ToBase64String(hash);
		}

		public bool VerifyPassword(string enteredPassword, byte[] key, string storedHash)
		{
			string enteredHash = HashPassword(enteredPassword, key);
			return CryptographicOperations.FixedTimeEquals(
				Convert.FromBase64String(storedHash),
				Convert.FromBase64String(enteredHash)
			);
		}

		public string nullItem(string item)
		{
			return $"{item} is null";
		}

		public string Encrypt(string plainText)
		{
			//Secret Key.
			string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#";
			//Secret Bytes.
			byte[] secretBytes = Encoding.UTF8.GetBytes(secretKey);
			//Plain Text Bytes.
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			//Encrypt with AES Alogorithm using Secret Key.
			using (Aes aes = Aes.Create())
			{
				aes.Key = secretBytes;
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.PKCS7;
				byte[] encryptedBytes = null;
				using (ICryptoTransform encryptor = aes.CreateEncryptor())
				{
					encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
				}
				return Convert.ToBase64String(encryptedBytes);
			}
		}

		public string Decrypt(string encryptedText)
		{
			//Secret Key.
			string secretKey = "$ASPcAwSNIgcPPEoTSa0ODw#";
			//Secret Bytes.
			byte[] secretBytes = Encoding.UTF8.GetBytes(secretKey);
			//Encrypted Bytes.
			byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
			//Decrypt with AES Alogorithm using Secret Key.
			using (Aes aes = Aes.Create())
			{
				aes.Key = secretBytes;
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.PKCS7;
				byte[] decryptedBytes = null;
				using (ICryptoTransform decryptor = aes.CreateDecryptor())
				{
					decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
				}
				return Encoding.UTF8.GetString(decryptedBytes);
			}
		}
	}
}
