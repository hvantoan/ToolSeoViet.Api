using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Database;
using ToolSeoViet.Database.Enums;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Services.Common;
using ToolSeoViet.Services.Exceptions;
using ToolSeoViet.Services.Hashers;
using ToolSeoViet.Services.Interfaces;
using ToolSeoViet.Services.Models.Auth;
using ToolSeoViet.Services.Resources;

namespace ToolSeoViet.Services.Implements {

    public class AuthService : BaseService, IAuthService {
        private readonly IConfiguration config;

        public AuthService(ToolSeoVietContext db, IHttpContextAccessor httpContextAccessor, IConfiguration config)
            : base(db, httpContextAccessor) {
            this.config = config;
        }

        //public async Task<LoginResponse> WebLogin(LoginRequest request)
        //{

        //    var user = await this.db.Users.AsNoTracking().FirstOrDefaultAsync(o => o.MerchantId == merchant.Id && o.Username == request.Username.ToLower().Trim());
        //    if (user == null)
        //        throw new UserException(Messages.Auth.Login.User_NotFound);
        //    if (!user.IsAdmin && !user.IsActive)
        //        throw new UserException(Messages.Auth.Login.User_Inactive);
        //    if (!PasswordHashser.Verify(request.Password, user.Password))
        //        throw new UserException(Messages.Auth.Login.User_IncorrectPassword);

        //    var permissions = await this.db.Permissions.Where(o => o.Type == EPermission.Web).AsNoTracking().ToListAsync();
        //    Role role = null;
        //    if (!string.IsNullOrWhiteSpace(user.RoleId))
        //    {
        //        role = await this.db.Roles.Include(o => o.RolePermissions).AsNoTracking()
        //            .FirstOrDefaultAsync(o => o.Id == user.RoleId && o.MerchantId == merchant.Id);
        //    }

        //    var userPermissions = UserPermissionDto.MapFromEntities(permissions, role?.RolePermissions?.ToList(), user.IsAdmin);
        //    var expiredAt = this.GetTokenExpiredAt();
        //    var claims = this.GetClaimPermissions(userPermissions);

        //    return new()
        //    {
        //        Token = this.GenerateToken(merchant.Id, user.Id, claims, expiredAt),
        //        ExpiredTime = new DateTimeOffset(expiredAt).ToUnixTimeMilliseconds(),
        //        MerchantCode = merchant.Code,
        //        Username = user.Username,
        //    };
        //}

        private DateTime GetTokenExpiredAt() {
            var now = DateTime.Now;
            var midnight = now.AddDays(1).Date;
            return midnight.Subtract(now).TotalMinutes > 60 ? midnight : midnight.AddDays(1);
        }

        private string GenerateToken(string userId, List<Claim> roleClaims, DateTime expiredAt) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["JwtSecret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim("UserId", userId),
            };

            claims.AddRange(roleClaims);

            var token = new JwtSecurityToken(
              claims: claims,
              expires: expiredAt,
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetClaimPermissions(List<UserPermissionDto> permissions) {
            List<Claim> claims = new List<Claim>();
            foreach (var item in permissions) {
                if (!item.IsEnable) continue;
                claims.Add(new Claim(ClaimTypes.Role, item.ClaimName));

                if (item.Items != null && item.Items.Any()) {
                    claims.AddRange(this.GetClaimPermissions(item.Items));
                }
            }
            return claims;
        }
    }
}