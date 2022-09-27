using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ToolSeoViet.Database;
using ToolSeoViet.Database.Models;
using ToolSeoViet.Service.Extensions;
using ToolSeoViet.Service.Interfaces;
using ToolSeoViet.Service.Models.Project;
using ToolSeoViet.Service.Models.Seo;
using ToolSeoViet.Service.Utils;
using ToolSeoViet.Services.Common;
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
            string url = $"https://www.google.com.vn/search?q={request.KeyWord}&num={request.Num}&start=0&ie=utf-8&oe=utf-8&pws=0&hl=vi";
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

            List<SearchSLIKey> total = new();
            List<HeadingDto> headings = new();
            for (int i = 0; i < taskList.Count; i++) {
                HeadingDto temp = ((Task<HeadingDto>)taskList[i]).Result;
                headings.Add(temp);
                var keys = total.Select(o => o.KeyWord).ToList();

                List<SearchSLIKey> keyWordExisted;
                List<SearchSLIKey> keyWordNotExisted;

                keyWordExisted = temp.SLI?.Where(o => keys.Contains(o.KeyWord ?? ""))?.ToList() ?? new List<SearchSLIKey>();
                keyWordNotExisted = temp.SLI?.Where(o => !keys.Contains(o.KeyWord ?? ""))?.ToList() ?? new List<SearchSLIKey>();
                foreach (var item in keyWordExisted) {
                    var data = total.FirstOrDefault(o => o.KeyWord == item.KeyWord);
                    if (data == null) continue;
                    data.Count += item.Count;
                }
                if (keyWordNotExisted.Any()) {
                    total = total.Union(keyWordNotExisted).ToList();
                }

            }
            var searchContent = new SearchContentDto() {
                DateCreated = DateTimeOffset.Now,
                Headings = headings ?? new List<HeadingDto>(),
                SLI = total.Any() ? total.OrderByDescending(o => o.Count).Take(50).ToList() : null,
                Name = request.KeyWord.Replace("+", " ")
            };
            await searchContentService.Save(searchContent);

            return searchContent;
        }

        public static async Task<HeadingDto> GetScraping(string url, int position, string h1, Dictionary<string, ViDictionary> dictionaries) {

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

            var h2 = body.SelectNodes("//h2");
            var h3 = body.SelectNodes("//h3");
            List<Title> titles = new();
            List<SubTitle> subTitles = new();

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
            List<string> words = body.GetBodyStr();
            var totalSLI = await GetSLI(words, dictionaries);

            return new() {
                Name = h1,
                Href = url,
                SLI = totalSLI,
                Position = position,
                SubTitles = subTitles.Any() ? subTitles.Select(o => SubTitleDto.FromEntity(o)).ToList() : null,
                Titles = titles.Any() ? titles.Select(o => TitleDto.FromEntity(o)).ToList() : null,
            };
        }

        public static async Task<List<SearchSLIKey>> GetSLI(List<string> wordsList, Dictionary<string, ViDictionary> dictionaries) {
            Dictionary<string, int> wordDic = new();
            for (int i = 0; i < wordsList.Count; i++) {
                List<string> strTemp = wordsList[i].Split(new string[] { " ", "," }, StringSplitOptions.None).ToList();
                for (int j = 0; j < strTemp.Count - 1; j++) {
                    string str = strTemp[j].GetStr() + " " + strTemp[j + 1].GetStr();
                    if (wordDic.ContainsKey(str)) {
                        wordDic[str]++;
                    } else {
                        wordDic.Add(str, 1);
                    }
                }
            }
            List<SearchSLIKey> result = new();
            List<SearchSLIKey> keys = wordDic.Select(s => new SearchSLIKey { Count = s.Value, KeyWord = s.Key }).OrderByDescending(s => s.Count).ToList();

            foreach (var key in keys) {
                if (!dictionaries.ContainsKey(key.KeyWord.Trim())) continue;
                if (!dictionaries[key.KeyWord.Trim().ToLower()].IsMeaning) continue;
                if (key.Count > 2) result.Add(key);
            }
            return await Task.FromResult(result);
        }

        public async Task<SearchPosition> Position(SearchPositionRequest request) {

            request.Key = request.Key.Replace("  ", " ").Replace(" ", "+");
            string url = $"https://www.google.com.vn/search?q={request.Key}&num=10&start=0&ie=utf-8&oe=utf-8&pws=0&hl=vi";
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

                if (href.Contains(request.Domain, StringComparison.CurrentCulture)) {
                    return await Task.FromResult(new SearchPosition() {
                        Data = new ProjectDetailDto() {
                            Url = href.GetHref(),
                            Key = request.Key.Replace("+", " "),
                            Name = heading,
                            CurrentPosition = i + 1,
                            BestPosition = i + 1
                        }
                    });
                }
            }
            return await Task.FromResult(new SearchPosition() { Data = new ProjectDetailDto() { Key = request.Key } });
        }

        public async Task<SearchIndex> Index(SearchIndexRequest request) {
            string url = $"https://www.google.com/search?q=site:{request.Href}&oq=site&sourceid=chrome&ie=UTF-8";
            string result = url.GetHtmlPage();

            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(result);
            string divClass = configuration["SeoToolConfig:divCSS"];
            string devDescription = configuration["SeoToolConfig:divDescription"];
            var divList = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'" + devDescription + "')]");
            if (divList != null) {
                for (int i = 0; i < divList.Count; i++) {
                    var temp = divList[i].Descendants("div").Where(s => s.Attributes["class"] != null && s.Attributes["class"].Value.Contains(divClass)).FirstOrDefault();
                    if (temp == null)
                        continue;
                    var a = temp.Descendants("a").FirstOrDefault();
                    if (a == null)
                        continue;

                    string href = WebUtility.UrlDecode(a.GetAttributeValue("href", string.Empty));
                    if (string.IsNullOrEmpty(href)) continue;

                    if (href.Contains(request.Href, StringComparison.CurrentCulture)) {
                        return await Task.FromResult(new SearchIndex() {
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
