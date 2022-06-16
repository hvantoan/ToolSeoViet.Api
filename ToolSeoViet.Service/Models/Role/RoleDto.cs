using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ToolSeoViet.Services.Models.Role {

    public partial class RoleDto {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<PermissionDto> Permissions { get; set; }
    }

    public partial class RoleDto {

        public static RoleDto FromEntity(ToolSeoViet.Database.Models.Role entity, List<ToolSeoViet.Database.Models.Permission> permissions = null) {
            if (entity == null) return default;

            var result = new RoleDto {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name,
            };

            if (permissions != null) {
                result.Permissions = PermissionDto.FromEntities(permissions);
                result.Permissions = IncludePermissions(result.Permissions, entity);
            }

            return result;
        }

        private static List<PermissionDto> IncludePermissions(List<PermissionDto> permissions, ToolSeoViet.Database.Models.Role role, bool isEnable = true) {
            if (role == null || role.RolePermissions == null || !role.RolePermissions.Any()) return permissions;
            if (permissions == null || !permissions.Any()) return permissions;

            foreach (var item in permissions) {
                var rolePermission = role.RolePermissions.FirstOrDefault(o => o.PermissionId == item.Id);
                if (rolePermission == null) continue;
                item.IsEnable = isEnable && rolePermission.IsEnable;
                item.Items = IncludePermissions(item.Items, role, item.IsEnable);
            }
            return permissions;
        }
    }
}