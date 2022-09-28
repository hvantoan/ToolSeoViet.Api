using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolSeoViet.Database;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Interfaces;
using ToolSeoViet.Service.Models.Project;
using ToolSeoViet.Services.Common;
using ToolSeoViet.Services.Resources;
using TuanVu.Services.Exceptions;
using TuanVu.Services.Extensions;

namespace ToolSeoViet.Service.Implements {
    public class ProjectService : BaseService, IProjectService {
        public ProjectService(ToolSeoVietContext db, IHttpContextAccessor httpContextAccessor) : base(db, httpContextAccessor) {
        }

        public async Task<ProjectDto> Get(string id) {
            var data = await db.Projects.Include(k => k.ProjectDetails).AsNoTracking().FirstOrDefaultAsync(o => o.UserId == this.currentUserId && o.Id == id);
            if (data == null) throw new ManagedException(Messages.Project.Get.Project_NotFound);

            return ProjectDto.FromEntity(data);
        }

        public async Task CreateOrUpdate(SaveProjectRequest request) {
            var isExiting = await db.Projects.AnyAsync(o => o.Id == request.Id);
            if (!isExiting) {
                await Create(request);
            } else {
                await Update(request);
            }
        }

        public async Task Create(SaveProjectRequest request) {
            request.Name = request.Name.Trim();
            if (string.IsNullOrEmpty(request.Name)) throw new ManagedException(Messages.Project.CreateOrUpdate.Project_NameNotNullOrEmplty);
            var existed = this.db.Projects.Where(o => o.UserId == this.currentUserId).Any(o => o.Name == (request.Name ?? ""));
            if (existed) throw new ManagedException(Messages.Project.CreateOrUpdate.Project_NameNotDuplicated);
            if (string.IsNullOrEmpty(request.Id)) request.Id = null;

            Project project = new() {
                Id = request.Id ?? Guid.NewGuid().ToStringN(),
                Domain = request.Domain,
                Name = request.Name,
                UserId = currentUserId,
            };

            project.ProjectDetails = request?.ProjectDetails?.Select(o => new ProjectDetail() {
                Id = Guid.NewGuid().ToStringN(),
                Key = o.Key,
                ProjectId = project.Id,
            }).ToList();

            this.db.Projects.Add(project);
            await this.db.SaveChangesAsync();
        }

        public async Task Update(SaveProjectRequest request) {
            var data = await db.Projects.FirstOrDefaultAsync(o => o.Id == request.Id);
            if (data == null) throw new ManagedException(Messages.Project.Get.Project_NotFound);

            request.Name = request.Name.Trim();
            if (string.IsNullOrEmpty(request.Name)) throw new ManagedException(Messages.Project.CreateOrUpdate.Project_NameNotNullOrEmplty);

            var existed = this.db.Projects.Where(o => o.UserId == this.currentUserId).Where(o => o.Id != data.Id).Any(o => o.Name == (request.Name ?? ""));
            if (existed) throw new ManagedException(Messages.Project.CreateOrUpdate.Project_NameNotDuplicated);

            data.Id ??= Guid.NewGuid().ToStringN();
            data.Domain = request.Domain ?? data.Domain;
            data.Name = request.Name ?? data.Name;
            data.UserId = currentUserId;

            var keyWords = new List<ProjectDetail>();
            var keyWordIds = request.ProjectDetails.Select(o => o.Id).ToList();
            var keyWordOnData = await db.ProjectDetails.Where(o => o.ProjectId == data.Id).ToListAsync();

            var keyWordDeletes = keyWordOnData.Where(o => !keyWordIds.Contains(o.Id));
            this.db.ProjectDetails.RemoveRange(keyWordDeletes);

            var keyWordUpdates = keyWordOnData.Where(o => keyWordIds.Contains(o.Id));
            foreach (var item in request.ProjectDetails) {
                var keyWord = keyWordUpdates.FirstOrDefault(o => o.Id == item.Id) ?? new();
                var temp = new ProjectDetail {
                    Id = keyWord.Id ?? Guid.NewGuid().ToStringN(),
                    Name = item.Name,
                    CurrentPosition = item.CurrentPosition,
                    ProjectId = data.Id,
                    Url = item.Url,
                    BestPosition = keyWord.BestPosition
                };
                if (item.CurrentPosition < keyWord.BestPosition && item.CurrentPosition > 0) temp.BestPosition = item.CurrentPosition;
                if (keyWord.BestPosition == 0 && item.CurrentPosition > 0) temp.BestPosition = item.CurrentPosition;

                keyWords.Add(temp);
            }
            data.ProjectDetails = keyWords;

            await this.db.SaveChangesAsync();
        }

        public async Task<ListProjectResponse> All() {
            var projects = await this.db.Projects.Where(o => o.UserId == this.currentUserId).ToListAsync();
            if (projects == null) throw new ManagedException(Messages.Project.Get.Project_NotFound);
            return new ListProjectResponse() {
                Items = projects.Select(o => ProjectDto.FromEntity(o)).ToList(),
                Count = projects.Count,
            };
        }

        public async Task Delete(string Id) {
            var project = await this.db.Projects.Include(o => o.ProjectDetails).FirstOrDefaultAsync(o => o.Id == Id && o.UserId == this.currentUserId);
            if (project == null) throw new ManagedException(Messages.Project.Project_NotFound);

            try {
                if (project.ProjectDetails.Any()) {
                    this.db.ProjectDetails.RemoveRange(project.ProjectDetails);
                }
                this.db.Projects.Remove(project);
                await this.db.SaveChangesAsync();
            } catch (Exception ex) {
                throw new ManagedException(ex.Message);
            }

        }
    }
}
