using System.Threading.Tasks;
using ToolSeoViet.Services.Models.User;

namespace ToolSeoViet.Services.Interfaces {
    public interface IUserService {
        Task ChangePassword(string oldPassword, string newPassword);
        Task<UserDto> CreateOrUpdate(UserDto model);
        //Task<UserDto> Get(string id);
        //Task<ListUserData> List(ListUserRequest request);
        Task ResetPassword(string userId, string password);
    }
}