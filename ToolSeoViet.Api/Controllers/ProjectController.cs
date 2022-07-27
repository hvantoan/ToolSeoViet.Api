using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToolSeoViet.Service.Interfaces;

namespace ToolSeoViet.Api.Controllers {
    [ApiController, Authorize, Route("api/project")]
    public class ProjectController : ControllerBase {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService) {
            this.projectService = projectService;
        }
    }
}
