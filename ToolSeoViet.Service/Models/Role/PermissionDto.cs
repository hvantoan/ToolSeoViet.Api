﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace ToolSeoViet.Services.Models.Role {

    public partial class PermissionDto {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsEnable { get; set; }
        public int OrderIndex { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public List<PermissionDto> Items { get; set; }
    }

    public partial class PermissionDto {

        public static List<PermissionDto> FromEntities(List<ToolSeoViet.Database.Models.Permission> permissions, string parentId = null) {
            var permissionDtos = permissions.Where(o => o.IsActive && o.ParentId == parentId).Select(o => new PermissionDto {
                Id = o.Id,
                Name = o.DisplayName,
                IsEnable = o.Default,
            }).OrderBy(o => o.OrderIndex).ToList();

            permissionDtos.ForEach(o => o.Items = FromEntities(permissions, o.Id));

            return permissionDtos;
        }
    }
}