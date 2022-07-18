﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToolSeoViet.Service.Interfaces;
using ToolSeoViet.Service.Models.SearchContent;
using ToolSeoViet.Services.Models;

namespace ToolSeoViet.Api.Controllers {
    [ApiController, Authorize, Route("api/search-content/")]
    public class SearchContentController : ControllerBase {
        private readonly ISearchContentService searchContentService;

        public SearchContentController(ISearchContentService searchContentService) {
            this.searchContentService = searchContentService;
        }

        [HttpGet, Route("all")]
        public async Task<BaseResponse> All() {
            try {
                var response = await this.searchContentService.All();
                return BaseResponse<ListSearchContentResponse>.Ok(response);
            } catch (Exception ex) {
                return BaseResponse.Fail(ex.Message);
            }
        }
    }
}
