using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace ToolSeoViet.Service.Extensions {
    public static class StringExtention { 
        private static int block;
        private static string content;
        public static string GetGoogleResearch(this string url) {

            WebClient webClient = new();
            block = 1;
            webClient.DownloadStringCompleted += (sender, e) => {
                block = 0;
                content = e.Result;
            };
            webClient.DownloadStringAsync(new Uri(url));
            while (block == 1) {

                Thread.Sleep(10);
            }
            return content;
        }

        public static string GetHtmlPage(this string strURL) {
            String strResult;
            WebRequest objRequest = WebRequest.Create(strURL);
            objRequest.Timeout = 2000;
            WebResponse objResponse = objRequest.GetResponse();

            using (var sr = new System.IO.StreamReader(objResponse.GetResponseStream())) {
                strResult = sr.ReadToEnd();
                sr.Close();
            }
            return strResult;
        }

        public static string GetHref(this string href) {
            if (!href.Contains("url?q=")) {
                return href;
            }
            return href.Split(new string[] { "url?q=" }, StringSplitOptions.None)[1].Split(new string[] { "&" }, StringSplitOptions.None)[0];
        }

        public static List<string> SplitGotoRow(this string content) {
            List<string> result = new List<string>();
            if (!content.Contains("\r\n")) {
                if (!string.IsNullOrEmpty(content))
                    result.Add(content.Trim());
                return result;
            }
            List<string> hList = new List<string>();
            hList = content.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            for (int i = 0; i < hList.Count; i++) {
                if (string.IsNullOrEmpty(hList[i]))
                    continue;
                if (hList[i].Contains("\r") || hList[i].Contains("\n")) {
                    List<string> MTemp1 = hList[i].Split(new string[] { "\n" }, StringSplitOptions.None).ToList();
                    for (int j = 0; j < MTemp1.Count; j++) {
                        if (string.IsNullOrEmpty(MTemp1[j]))
                            continue;
                        if (MTemp1.Contains("\r")) {
                            string[] MArray = MTemp1[j].Trim().Split(new string[] { "\r" }, StringSplitOptions.None);
                            for (int h = 0; h < MArray.Length; h++) {
                                if (string.IsNullOrEmpty(MArray[h]))
                                    continue;
                                result.Add(MArray[h].Trim());
                            }

                        } else {
                            result.Add(MTemp1[j].Trim());
                        }
                    }
                } else {
                    result.Add(hList[i].Trim());
                }
            }
            return result;
        }
        public static string GetHtmlDetail(this string url) {
            try {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 5000; //Timeout after 1000 ms
                using (var stream = request.GetResponse().GetResponseStream())
                using (var reader = new System.IO.StreamReader(stream)) {
                    return reader.ReadToEnd();
                }
            } catch (Exception) {
                return "";
            }
        }
        public static string getStr(this string str) {
            string strTemp = (str.EndsWith("”") || str.EndsWith("\"") || str.EndsWith("?") || str.EndsWith(".") || str.EndsWith(":")) ? str.Substring(0, str.Length - 1) : str;
            return strTemp.ToLower().Trim();
        }

        public static List<string> GetBodyStr(this HtmlNode body) {
            foreach (var script in body.Descendants("script").ToArray())
                script.Remove();
            foreach (var item in body.Descendants("style").ToArray())
                item.Remove();
            foreach (var item in body.Descendants("iframe").ToArray())
                item.Remove();
            foreach (var item in body.Descendants("Iframe").ToArray())
                item.Remove();
            foreach (var item in body.Descendants("video").ToArray())
                item.Remove();
            foreach (var item in body.Descendants("videoitem").ToArray())
                item.Remove();

            List<string> strList = new List<string>();
            var temp = body.DescendantNodesAndSelf().ToList();

            for (int i = 0; i < temp.Count; i++) {
                if (!temp[i].HasChildNodes) {
                    string text = WebUtility.HtmlDecode(temp[i].InnerText);
                    if (!string.IsNullOrEmpty(text)) {
                        StringBuilder sb = new StringBuilder(text.Trim());
                        sb.Replace("\n\n", "\n");
                        sb.Replace("\t\t", "\t");
                        sb.Replace("\r\r", "\r");
                        sb.Replace("-", "");
                        sb.Replace("–", "");
                        sb.Replace("\"", "");
                        sb.Replace("'", "");
                        sb.Replace(">", "");
                        sb.Replace("<", "");
                        sb.Replace("!", "");
                        sb.Replace("#", "");
                        sb.Replace("”", "");
                        sb.Replace("*", "");
                        sb.Replace("*", "");
                        sb.Replace("…", "");
                        text = sb.ToString();
                        if (text == "\n" || text == "\n\n" || text == "\n\t" || text == "\n\r" || text == "\r" || text == "\t" || text == ""
                            || text == "." || text == "," || text == "|" ||
                            text.StartsWith("{\""))
                            continue;

                        string[] M = text.Split(new string[] { ".", ",", "?", ":", "-", "–", "", "\\", "/", "&", "(", ")", "[", "]", "'", "+", "...", "_" }, StringSplitOptions.None);
                        for (int h = 0; h < M.Length; h++) {
                            if (M[h].All(char.IsDigit)) {
                                continue;
                            }
                            string strTemp = M[h].Trim();
                            if (!string.IsNullOrEmpty(strTemp))
                                strList.Add(M[h].Trim());
                        }

                    }
                }
            }
            return strList;
        }
    }
}
