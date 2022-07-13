using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToolSeoViet.Service.Interfaces;
using ToolSeoViet.Service.Models.Project;
using ToolSeoViet.Services.Models;

namespace ToolSeoViet.Api.Controllers {
    [ApiController, Authorize, Route("api/project")]
    public class ProjectController : ControllerBase {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService) {
            this.projectService = projectService;
        }

        [HttpPost, Route("all")]
        public async Task<BaseResponse> All(GetProjectRequest request) {
            try {
                var response = await this.projectService.All(request);
                return BaseResponse<ListProjectResponse>.Ok(response);
            } catch (Exception ex) {
                return BaseResponse.Fail(ex.Message);
            }
        }
        [HttpPost, Route("Get")]
        public async Task<BaseResponse> Get(GetProjectRequest request1)
        {
            try
            {
                var response = await this.projectService.Get(request1);
                return BaseResponse<ListProjectResponse>.Ok(response);
            }
            catch (Exception ex)
            {
                return BaseResponse.Fail(ex.Message);
            }
        }
        [HttpGet, Route("Save")]
        public async Task<BaseResponse> Save(SaveProjact request2)
        {
            try
            {
                var response2 = await this.projectService.Get(request2);
                return BaseResponse<ListProjectResponse>.Ok(response2);
            }
            catch (Exception ex)
            {
                return BaseResponse.Fail(ex.Message);
            }
        }

    }
}
