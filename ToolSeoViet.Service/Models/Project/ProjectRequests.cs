using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Services.Models;

namespace ToolSeoViet.Service.Models.Project {
    public class GetProjectRequest {
        public string Id { get; set; }
    }
    public class SaveProjectRequest{
        public ProjectDto Project { get; set; }
    }
}
