using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using ToolSeoViet.Database;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Extensions;
using ToolSeoViet.Service.Interfaces;
using ToolSeoViet.Service.Models.Seo;
using ToolSeoViet.Service.Utils;
using ToolSeoViet.Services.Common;
using ToolSeoViet.Services.Resources;
using TuanVu.Services.Exceptions;
using TuanVu.Services.Extensions;
using static ToolSeoViet.Service.Enums;

namespace ToolSeoViet.Service.Implements {
    public class SeoService : BaseService, ISeoService {

        public readonly IViDictionaryService viDictionaryService;
        public readonly ISearchContentService searchContentService;
        public readonly CacheManager cacheManager;

        public SeoService(ToolSeoVietContext db, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IViDictionaryService viDictionaryService, IMemoryCache memoryCache, ISearchContentService searchContentService)
            : base(db, httpContextAccessor, configuration) {
            this.viDictionaryService = viDictionaryService;
            this.cacheManager = new CacheManager(memoryCache, viDictionaryService);
            this.searchContentService = searchContentService;
        }

        public async Task<SearchContentDto> GetContennt(SearchContentRequest request) {

            request.KeyWord = request.KeyWord.Replace("  ", " ").Replace(" ", "+");
            string url = "https://www.google.com.vn" + "/search?q=" + request.KeyWord + "&ie=utf-8&num=" + request.Num;
            string result = url.GetHtmlPage();


            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(result);
            string divClass = configuration["SeoToolConfig:divCSS"];
            string devDescription = configuration["SeoToolConfig:divDescription"];
            var divList = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'" + devDescription + "')]");
            List<Task> taskList = new();
            Dictionary<string, ViDictionary> dictionaries = cacheManager.GetViDictionaries;
            for (int i = 0; i < divList.Count; i++) {
                var temp = divList[i].Descendants("div").Where(s => s.Attributes["class"] != null && s.Attributes["class"].Value.Contains(divClass)).FirstOrDefault();
                if (temp == null)
                    continue;
                var a = temp.Descendants("a").FirstOrDefault();
                if (a == null)
                    continue;
                var h3 = temp.Descendants("h3").FirstOrDefault();
                if (h3 == null)
                    continue;

                string href = WebUtility.UrlDecode(a.GetAttributeValue("href", string.Empty));
                string description = WebUtility.HtmlDecode(divList[i].InnerHtml);
                string heading = System.Web.HttpUtility.HtmlDecode(h3.InnerText.Trim()).SplitGotoRow().FirstOrDefault(o => !string.IsNullOrEmpty(o));
                int index = i + 1;
                Task<HeadingDto> taskTemp = Task.Run(() => {
                    return GetScraping(href.GetHref(), index, heading, dictionaries);
                });
                taskList.Add(taskTemp);
            }
            await Task.WhenAll(taskList);
            string path = System.IO.Directory.GetCurrentDirectory();

            List<HeadingDto> headings = new();
            for (int i = 0; i < taskList.Count; i++) {
                HeadingDto temp = ((Task<HeadingDto>)taskList[i]).Result;
                headings.Add(temp);
            }

            var searchContent = new SearchContentDto() {
                DateCreated = DateTimeOffset.Now,
                Headings = headings ?? new List<HeadingDto>(),
                Name = request.KeyWord.Replace("+", " ")
            };
            await searchContentService.Save(searchContent);

            return searchContent;
        }

        public static HeadingDto GetScraping(string url, int position, string h1, Dictionary<string, ViDictionary> dictionaries) {

            HtmlDocument htmlDocument = new();
            string htmlDetail = url.GetHtmlDetail();
            if (htmlDetail == "") {
                return new HeadingDto() {
                    Href = url.GetHref(),
                    Name = h1,
                    Position = position,
                    Id = Guid.NewGuid().ToStringN()
                };
            }

            htmlDocument.LoadHtml(htmlDetail);
            var body = htmlDocument.DocumentNode.SelectSingleNode("//body");
            if (body == null) return new HeadingDto() {
                Href = url.GetHref(),
                Name = h1,
                Position = position,
                Id = Guid.NewGuid().ToStringN()
            };
            List<string> wordList = body.Get
            var totalSLI = GetSLI()

            var h2 = htmlDocument.DocumentNode.SelectNodes("//body//h2");
            var h3 = htmlDocument.DocumentNode.SelectNodes("//body//h3");
            var titles = new List<Title>();
            var subTitles = new List<SubTitle>();

            if (h2 != null) {
                foreach (var item1 in h2) {
                    if (item1 != null && !string.IsNullOrEmpty(item1.InnerText)) {
                        titles = titles.Concat(WebUtility.HtmlDecode(item1.InnerText).SplitGotoRow().Where(s => !string.IsNullOrEmpty(s)).Select(o => new Title() {
                            Name = o,
                            Position = position
                        })).ToList();
                    }
                }
            }
            if (h3 != null) {
                foreach (var item1 in h3) {
                    if (item1 != null && !string.IsNullOrEmpty(item1.InnerText)) {
                        subTitles = subTitles.Concat(WebUtility.HtmlDecode(item1.InnerText).SplitGotoRow().Where(s => !string.IsNullOrEmpty(s)).Select(o => new SubTitle() {
                            Name = o,
                            Position = position
                        })).ToList();
                    }
                }
            }
            return new() {
                Name = h1,
                Href = url,
                Position = position,
                SubTitles = subTitles?.Select(o => SubTitleDto.FromEntity(o)).ToList() ?? new List<SubTitleDto>(),
                Titles = titles?.Select(o => TitleDto.FromEntity(o)).ToList() ?? new List<TitleDto>(),
            };
        }

        public async Task<List<string>> GetSLI(List<string> wordsList, Dictionary<string, ViDictionary> dictionaries, List<string> totalSLI) {
            Dictionary<string, int> wordDic = new();
            for (int i = 0; i < wordsList.Count; i++) {
                List<string> strTemp = wordsList[i].Split(new string[] { " ", "," }, StringSplitOptions.None).ToList();
                for (int j = 0; j < strTemp.Count - 1; j++) {
                    string str = strTemp[j].getStr() + " " + strTemp[j + 1].getStr();
                    if (wordDic.ContainsKey(str)) {
                        wordDic[str]++;
                    } else {
                        wordDic.Add(str, 1);
                    }
                }
            }
            List<string> tmp = new();
            List<SearchSLIKey> keyModelList = wordDic.Select(s => new SearchSLIKey { Count = s.Value, KeyWord = s.Key }).OrderByDescending(s => s.Count).ToList();

            for (int i = 0; i < keyModelList.Count; i++) {
                if (!dictionaries.ContainsKey(keyModelList[i].KeyWord.Trim())) continue;
                if (dictionaries[keyModelList[i].KeyWord.Trim().ToLower()].IsMeaning != true) continue;
                
                totalSLI.Add(keyModelList[i].KeyWord);
                if (i < 50 || keyModelList[i].Count > 2)
                    tmp.Add(keyModelList[i].KeyAndCount);
            }


            return await Task.FromResult(tmp);
        }

        public async Task<SearchPosition> Position(SearchPositionRequest request) {

            request.Key = request.Key.Replace("  ", " ").Replace(" ", "+");
            string url = "https://www.google.com.vn" + "/search?q=" + request.Key + "&ie=utf-8&num=100";
            string result = url.GetHtmlPage();

            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(result);
            string divClass = configuration["SeoToolConfig:divCSS"];
            string devDescription = configuration["SeoToolConfig:divDescription"];
            var divList = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'" + devDescription + "')]");

            for (int i = 0; i < divList.Count; i++) {
                var temp = divList[i].Descendants("div").Where(s => s.Attributes["class"] != null && s.Attributes["class"].Value.Contains(divClass)).FirstOrDefault();
                if (temp == null)
                    continue;
                var a = temp.Descendants("a").FirstOrDefault();
                if (a == null)
                    continue;
                var h3 = temp.Descendants("h3").FirstOrDefault();
                if (h3 == null)
                    continue;

                string href = WebUtility.UrlDecode(a.GetAttributeValue("href", string.Empty));
                string heading = System.Web.HttpUtility.HtmlDecode(h3.InnerText.Trim()).SplitGotoRow().FirstOrDefault(o => !string.IsNullOrEmpty(o));
                if (string.IsNullOrEmpty(href) || string.IsNullOrEmpty(heading)) continue;

                if (href.IndexOf(request.Domain) >= 0) {
                    return await Task.FromResult(new SearchPosition() {
                        Href = href.GetHref(),
                        Key = request.Key.Replace("+", " "),
                        Name = heading,
                        Position = i + 1
                    });
                }
            }
            return await Task.FromResult(new SearchPosition() { Key = request.Key });
        }

        public async Task<SearchIndex> Index(SearchIndexRequest request) {
            string url = $"https://www.google.com/search?q=site:{request.Href}&oq=site&sourceid=chrome&ie=UTF-8";
            string result = url.GetHtmlPage();

            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(result);
            string divClass = configuration["SeoToolConfig:divCSS"];
            string devDescription = configuration["SeoToolConfig:divDescription"];
            var divList = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'" + devDescription + "')]");
            if (divList != null)
            {
                for (int i = 0; i < divList.Count; i++)
                {
                    var temp = divList[i].Descendants("div").Where(s => s.Attributes["class"] != null && s.Attributes["class"].Value.Contains(divClass)).FirstOrDefault();
                    if (temp == null)
                        continue;
                    var a = temp.Descendants("a").FirstOrDefault();
                    if (a == null)
                        continue;

                    string href = WebUtility.UrlDecode(a.GetAttributeValue("href", string.Empty));
                    if (string.IsNullOrEmpty(href)) continue;

                    if (href.IndexOf(request.Href) >= 0)
                    {
                        return await Task.FromResult(new SearchIndex()
                        {
                            Href = href.GetHref(),
                            Status = ECheckIndex.Done
                        });
                    }
                }
            }
            
            return await Task.FromResult(new SearchIndex() { 
                Href = request.Href,
                Status = ECheckIndex.None
            });
        }
    }
}
