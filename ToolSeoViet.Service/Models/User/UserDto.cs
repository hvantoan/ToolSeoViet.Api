using Newtonsoft.Json;
using ToolSeoViet.Services.Models.Role;

namespace ToolSeoViet.Services.Models.User {

    public partial class UserDto {
        public string Id { get; set; }
        public string Username { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public RoleDto Role { get; set; }
    }

    public partial class UserDto {

        public static UserDto FromEntity(ToolSeoViet.Database.Models.User entity,
            ToolSeoViet.Database.Models.Role roleEntity = null) {
            if (entity == null) return default;
            entity.Role ??= roleEntity;

            return new UserDto {
                Id = entity.Id,
                Username = entity.Username,
                IsActive = entity.IsActive,
                IsAdmin = entity.IsAdmin,
                Role = RoleDto.FromEntity(entity.Role),
            };
        }
    }
}