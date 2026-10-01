using EcommerceDemo.Models;
using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Models.Entity_Models;

namespace EcommerceDemo.Repository.Interface
    {
    public interface IAuthInterface
        {
        Task<ResponseModel<object>> UserRegistration(DTOUserRegistration user);  
        //Task<ResponseModel<object>> Login(DTOUserLogin user);
        Task<ResponseModel<object>> OtpVrification(DTOOtpVerificationModel Otpdata);
        Task<ResponseModel<object>> GenerateOtp(DTOGenerateOtp userdata);
        Task<ResponseModel<object>> ForgetPassword(DTOForgatePassword user);

        Task<ResponseModel<DTOUserResponse>> Login(DTOUserLogin user);
        }
    }
