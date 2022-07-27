using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ToolSeoViet.Database;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Exceptions;
using ToolSeoViet.Service.Interfaces;
using ToolSeoViet.Service.Models.SearchContent;
using ToolSeoViet.Service.Models.Seo;
using ToolSeoViet.Services.Common;
using ToolSeoViet.Services.Resources;
using TuanVu.Services.Extensions;

namespace ToolSeoViet.Service.Implements {
    public class SearchContentService : BaseService, ISearchContentService {
        public SearchContentService(ToolSeoVietContext db, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(db, httpContextAccessor, configuration) {
        }

        public async Task<ListSearchContentResponse> All() {

            /// Không có nội dung xuất ra message
            List<SearchContent> searchContents = this.db.SearchContents.AsNoTracking().Where(o => o.UserId == this.currentUserId).OrderByDescending(o => o.DateCreated).ToList();

            if (searchContents == null) throw new SearchContentException(Messages.SearchContent.SearchContent_IsEmpty);

            List<SearchContentDto> searchContentsDto = new();

            foreach (var item in searchContents) searchContentsDto.Add(SearchContentDto.FromEntity(item));

            return await Task.FromResult(new ListSearchContentResponse() {
                Count = searchContentsDto.Count(),
                Items = searchContentsDto
            });
        }
        public async Task<SearchContentDto> Get(GetSearchContent request) {
            var data = this.db.SearchContents.AsNoTracking().Where(o => o.Id == request.Id && o.UserId == currentUserId).FirstOrDefault();
            if (data == null) throw new SearchContentException(Messages.SearchContent.SearchContent_NotFound);
            if (data == null) throw new SearchContentException(Messages.SearchContent.SearchContent_NotFound);
            var headings =  this.db.Headings.Include(o => o.Titles).Include(o => o.SubTitles).AsNoTracking().Where(o => o.SearchContentId == data.Id);
            return await Task.FromResult(new SearchContentDto() {
                DateCreated = data.DateCreated,
                Id = data.Id,
                Headings = headings.Select(o => HeadingDto.FromEntity(o, o.Titles.ToList(), o.SubTitles.ToList())).ToList(),
                Name = data.Name
            });
        }
        public async Task Save(SearchContentDto searchContent) {
            var data = new SearchContent() {
                Id = Guid.NewGuid().ToStringN(),
                DateCreated = DateTimeOffset.Now,
                Name = searchContent.Name,
                UserId = this.currentUserId,
            };
            this.db.SearchContents.Add(data);
            SaveHeading(searchContent.Headings, data.Id);
            await this.db.SaveChangesAsync();
        }

        public void SaveHeading(List<HeadingDto> headings, string searchContentId) {
            foreach (var heading in headings) {

                var tmp = new Heading() {
                    Id = Guid.NewGuid().ToStringN(),
                    Href = heading.Href,
                    Name = heading.Name,
                    Position = heading.Position,
                    SearchContentId = searchContentId,
                };
                this.db.Headings.Add(tmp);
                SaveTitle(heading.Titles, tmp.Id);
                SaveSubTitle(heading.SubTitles, tmp.Id);
            }
        }
        public void SaveTitle(List<TitleDto> titles, string headingId) {
            var mTitles = titles.Select(o => new Title() {
                Id = Guid.NewGuid().ToStringN(),
                HeadingId = headingId,
                Name = o.Name,
                Position = o.Position,
            }).ToList();
            foreach (var title in mTitles) this.db.Titles.Add(title);
        }
        public void SaveSubTitle(List<SubTitleDto> subTitles, string headingId) {
            var mSubTitles = subTitles.Select(o => new SubTitle() {
                Id = Guid.NewGuid().ToStringN(),
                HeadingId = headingId,
                Name = o.Name,
                Position = o.Position,
            }).ToList();
            foreach (var subTitle in mSubTitles) this.db.SubTitles.Add(subTitle);
        }

    }
}
