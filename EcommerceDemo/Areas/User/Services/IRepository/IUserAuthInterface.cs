using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Models;
using EcommerceDemo.Areas.User.Models;
namespace EcommerceDemo.Areas.User.Services.IRepository
    {
    public interface IUserAuthInterface
        {
        Task<ResponseModel<object>> UserRegistration(VMRegisterUser user);
        Task<ResponseModel<object>> OtpVerification( VMOtpModel otp);
        Task<ResponseModel<object>> GenerateOtp ( VMGenerateOtp userdata);
        Task<ResponseModel<object>> ForgetPassword ( VMForgetPassword userdata);
        Task<ResponseModel<VMUserResponseData>> Login(VMUserLogin user);
        }
    }
