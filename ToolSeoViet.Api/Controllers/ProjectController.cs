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

        
        //[HttpPost, Route("Get")]
        //public async Task<BaseResponse> Get(GetProjectRequest request)
        //{
        //    //try
        //    //{
        //    //    var response = await this.projectService.Get(request.Id);
        //    //    return BaseResponse<ListProjectResponse>.Ok(a);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return BaseResponse.Fail(ex.Message);
        //    //}
        //}
    }
}
