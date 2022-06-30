using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToolSeoViet.Services.Interfaces;
using ToolSeoViet.Services.Models;
using ToolSeoViet.Services.Models.Auth;

namespace ToolSeoViet.Api.Controllers {
    [ApiController, AllowAnonymous, Route("api/auth")]
    public class AuthorizeController : ControllerBase {
        private readonly IAuthService authService;

        public AuthorizeController(IAuthService authService) {
            this.authService = authService;
        }

        [HttpPost, Route("login")]
        public async Task<BaseResponse> Login(LoginRequest request) {
            try {
                var response = await this.authService.WebLogin(request);
                return BaseResponse<LoginResponse>.Ok(response);
            } catch (Exception ex) {
                return BaseResponse.Fail(ex.Message);
            }
        }
        [HttpPost, Route("login/google")]
        public async Task<BaseResponse> LoginGoogle(LoginGoogleRequest request) {
            try {
                var response = await this.authService.WebLoginGoogle(request);
                return BaseResponse<LoginResponse>.Ok(response);
            } catch (Exception ex) {
                return BaseResponse.Fail(ex.Message);
            }
        }
    }
}
