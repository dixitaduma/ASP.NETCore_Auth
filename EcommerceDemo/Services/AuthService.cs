using Azure;
using EcommerceDemo._commanmethods;
using EcommerceDemo.ApplicationDbContext;
using System.Net;
using EcommerceDemo.Models;
using EcommerceDemo.Models.DTO_Models;
using EcommerceDemo.Repository.Interface;

namespace EcommerceDemo.Services
{
    public class AuthService
    {
        private readonly CommanMethods _commanMethods;
        private readonly IAuthInterface _authrepo;
        public AuthService(CommanMethods commanMethods, IAuthInterface authrepo) 
        {
            _commanMethods = commanMethods;
            _authrepo = authrepo;

        }
        //public ResponseModel<object> response = new ResponseModel<object>();



        public async Task<ResponseModel<DTOUserResponse>> Login(DTOUserLogin user)
        {
            ResponseModel<DTOUserResponse> response = new ResponseModel<DTOUserResponse>();
            try
            {
                user.UserEmail = user.UserEmail.ToLower();
                user.Password = _commanMethods.Encrypt(user.Password);

                var userDataResponce = await _authrepo.Login(user);

               
                var userData = userDataResponce.data;

                //Check Valid UserName
                if (userData == null)
                {
                    response.statusCode = (int)HttpStatusCode.BadRequest;
                    response.messages = _commanMethods.invalidUserNameOrEmail;
                    return response;
                }
                else
                {
                    //Match password 
                    if ( userData.Password == user.Password)
                    {
                        //Check user is active or not 
                        if (userData.IsDeleted)
                        {
                            response.statusCode = (int)HttpStatusCode.BadRequest;
                            response.messages = _commanMethods.userNotActive;
                            return response;
                        }

                        //Check User otp verified
                        if (!userData.IsOtpVerified)
                        {
                            response.statusCode = (int)HttpStatusCode.BadRequest;
                            response.messages = _commanMethods.otpNotVerified;
                            return response;
                        }

                        response.statusCode = (int)HttpStatusCode.OK;
                        response.messages = _commanMethods.loginSuccess;
                        response.data = userData;
                    }
                    else
                    {
                        response.statusCode = (int)HttpStatusCode.BadRequest;
                        response.messages = _commanMethods.incorrectPassword;
                        response.data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                response.statusCode = (int)HttpStatusCode.InternalServerError;
                response.messages = $"An error occurred: {ex.Message}";
                response.data = null;

            }

            return response;
        }





    }
}
