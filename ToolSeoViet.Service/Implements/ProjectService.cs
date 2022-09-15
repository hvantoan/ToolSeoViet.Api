using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Database;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Interfaces;
using ToolSeoViet.Service.Models.KeyWord;
using ToolSeoViet.Service.Models.Project;
using ToolSeoViet.Services.Common;
using ToolSeoViet.Services.Resources;
using TuanVu.Services.Exceptions;
using TuanVu.Services.Extensions;

namespace ToolSeoViet.Service.Implements {
    public class ProjectService : BaseService, IProjectService {
        public ProjectService(ToolSeoVietContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor) {
        }
        public async Task<GetProjectResponse> Get(string id) {
            var data = await db.Projects.Include(k => k.ProjectDetails).AsNoTracking().FirstOrDefaultAsync(o => o.UserId == this.currentUserId &&  o.Id == id);
            if (data == null) throw new ManagedException(Messages.Project.Get.Project_NotFound);
            var response = new ProjectDto() {
                Id = data.Id,
                Domain = data.Domain,
                Name = data.Name,
                UserId = data.UserId,
                KeyWords = data.ProjectDetails?.Select(o => KeyWordDto.FromEntity(o)).ToList(),
            };
            return new() {
                Data = response,
                Success = true,
                Message = "Lấy dữ liệu thành công."
            };
        }

        public async Task CreateOrUpdate(ProjectDto request) {
            var isExiting = await db.Projects.AnyAsync(o => o.Id == request.Id);
            if (!isExiting) {
                await Create(request);
            } else {
                await Update(request);
            }
        }
        public async Task Create(ProjectDto request) {
            Project project = new() {
                Id = Guid.NewGuid().ToStringN(),
                Domain = request.Domain,
                Name = request.Name,
                UserId = currentUserId
            };

            this.db.Projects.Add(project);
            await this.db.SaveChangesAsync();
        }
        public async Task Update(ProjectDto request) {
            var data = await db.Projects.FirstOrDefaultAsync(o => o.Id == request.Id);
            if (data == null) throw new ManagedException(Messages.Project.Get.Project_NotFound);
            data = new Project() {
                Id = data.Id ?? Guid.NewGuid().ToStringN(),
                Domain = request.Domain ?? data.Domain,
                Name = request.Name ?? data.Name,
                UserId = this.currentUserId,
            };

            var keyWords = new List<ProjectDetail>();
            var keyWordIds = request.KeyWords.Select(o => o.Id).ToList();
            var keyWordOnData = await db.ProjectDetails.Where(o => o.ProjectId == data.Id).ToListAsync();
            var keyWordDeletes = keyWordOnData.Where(o => !keyWordIds.Contains(o.Id));
            foreach (var item in keyWordDeletes) db.ProjectDetails.Remove(item);

            var keyWordUpdates = keyWordOnData.Where(o => keyWordIds.Contains(o.Id));
            foreach (var item in request.KeyWords) {
                var keyWord = keyWordUpdates.FirstOrDefault(o => o.Id == item.Id);
                keyWords.Add(new ProjectDetail() {
                    Id = keyWord.Id ?? Guid.NewGuid().ToStringN(),
                    Name = item.Name,
                    BestPosition = item.BestPosition < keyWord.BestPosition ? item.BestPosition : keyWord.BestPosition,
                    CurrentPosition = item.CurrentPosition,
                    ProjectId = data.Id,
                    Url = item.Url
                });
            }
            keyWordOnData = keyWords;
            await this.db.SaveChangesAsync();
        }

        public async Task<ListProjectResponse> All() {
            var projects = await this.db.Projects.Where(o => o.UserId == this.currentUserId).ToListAsync();
            
            return new ListProjectResponse() {
                Items = projects.Select(o => ProjectDto.FromEntity(o)).ToList(),
                Count = projects.Count,
            };
        }
    }
}
