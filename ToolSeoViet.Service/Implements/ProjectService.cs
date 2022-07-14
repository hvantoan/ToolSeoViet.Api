using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolSeoViet.Database;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Exceptions;
using ToolSeoViet.Service.Interfaces;
using ToolSeoViet.Service.Models.KeyWord;
using ToolSeoViet.Service.Models.Project;
using ToolSeoViet.Services.Common;
using ToolSeoViet.Services.Resources;
using TuanVu.Services.Extensions;

namespace ToolSeoViet.Service.Implements
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(ToolSeoVietContext db, IHttpContextAccessor httpContextAccessor)
            : base(db, httpContextAccessor)
        {
        }
        /// Đầu vào là Id Project
        public async Task<ProjectDto> Get(GetProjectRequest request)
        {

            var data = await db.Projects.AsNoTracking().Include(k => k.KeyWords).FirstOrDefaultAsync(o => o.UserId == request.Id);
            if (data == null) throw new ProjectException(Messages.Project.CheckProject.Project_NotFound);

            var response = new ProjectDto()
            {
                Id = data.Id,
                Domain = data.Domain,
                Name = data.Name,
                UserId = data.UserId,
                KeyWords = data.KeyWords.Select(o => KeyWordDto.FromEntity(o)).ToList(),
            };
            return response;
        }

        public async Task CreateOrUpdate(ProjectDto request)
        {
            var isExiting = await db.Projects.AnyAsync(o => o.Id == request.Id);
            if (!isExiting)
            {
                await Create(request);
            }
            else
            {
                await Update(request);
            }
        }
        public async Task Create(ProjectDto request)
        {
            Project project = new Project()
            {
                Id = Guid.NewGuid().ToStringN(),
                Domain = request.Domain,
                Name = request.Name,
                UserId = currentUserId
            };

            this.db.Projects.Add(project);
            await this.db.SaveChangesAsync();
        }
        public async Task Update(ProjectDto request)
        {
            // kiểm tra và lấy ra project đang có
            var data = await db.Projects.FirstOrDefaultAsync(o => o.Id == request.Id);

            data = new Project()
            {
                Id = data.Id ?? Guid.NewGuid().ToStringN(),
                Domain = request.Domain ?? data.Domain,
                Name = request.Name ?? data.Name,
                UserId = currentUserId,
            };

            var keyWords = new List<KeyWord>();

            var keyWordIds = request.KeyWords.Select(o => o.Id).ToList();

            var keyWordOnData = await db.KeyWords.Where(o => o.ProjectId == data.Id).ToListAsync();

            var keyWordDeletes = keyWordOnData.Where(o => !keyWordIds.Contains(o.Id));

            foreach (var item in keyWordDeletes) db.KeyWords.Remove(item);


            var keyWordUpdates = keyWordOnData.Where(o=> keyWordIds.Contains(o.Id));
            foreach (var item in request.KeyWords)
            {
                var keyWord = keyWordUpdates.FirstOrDefault(o=> o.Id == item.Id);
                keyWords.Add(new KeyWord()
                {
                    Id = keyWord.Id ?? Guid.NewGuid().ToStringN(),
                    Name = item.Name,
                    BestPosition = item.BestPosition < keyWord.BestPosition ? item.BestPosition : keyWord.BestPosition,
                    CurrentPosition = item.CurrentPosition,
                    ProjectId = data.Id,
                    Url = item.Url
                });
            }
            keyWordOnData = keyWords;
            this.db.SaveChanges();
        }
    }
}
