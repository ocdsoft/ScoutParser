using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net.Http;
using HtmlAgilityPack;
using System.Net;

namespace WebParser
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            string url = tbUrl.Value;

            HttpUtility hUtil = new HttpUtility();
            Uri localUri = new Uri(url);

            using (HttpClient hp = new HttpClient())
            {
              HttpResponseMessage hr = hp.GetAsync(localUri).Result;

                if (hr.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string response = hr.Content.ReadAsStringAsync().Result;

                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(response);                    
                    
                    var rivals = document.DocumentNode.Descendants("div")
                        .Where(x => x.Attributes["class"] != null &&
                        x.Attributes["class"].Value == "story-detail premium-story")
                        .SelectMany(x => x.Descendants("p")).SelectMany(p => p.OuterHtml).ToArray();


                    if (rivals != null)                        
                        content.InnerHtml = String.Concat(rivals);                  
                   
                   var oregonlive = document.DocumentNode.Descendants("div")
                    .Where(x => x.Attributes["class"] != null &&
                    x.Attributes["class"].Value == "main-wrapper main-wrapper--article")
                    .SelectMany(x => x.Descendants("p")).SelectMany(p => p.OuterHtml).ToArray();

                    if (oregonlive != null)
                        content.InnerHtml = String.Concat(oregonlive);

                    var marinij = document.DocumentNode.Descendants("div")
                    .Where(x => x.Attributes["class"] != null &&
                    x.Attributes["class"].Value == "article-content")
                    .SelectMany(x => x.Descendants("div")).SelectMany(p => p.OuterHtml).ToArray();

                    if (marinij != null)
                        content.InnerHtml = String.Concat(marinij);

                    var metas = document.DocumentNode.Descendants("meta")
                            .Where(x => x.Attributes["property"] != null ||
                            x.Attributes["name"] != null);

                    title.InnerHtml = metas
                      .Where(x => x.Attributes["property"] != null &&
                      x.Attributes["property"].Value == "og:title")
                      .Select(x => x.Attributes["content"].Value).FirstOrDefault();

                    image.Src = metas
                      .Where(x => x.Attributes["property"] != null &&
                      x.Attributes["property"].Value == "og:image")
                      .Select(x => x.Attributes["content"].Value).FirstOrDefault();

                    string authorName = metas
                        .Where(x => x.Attributes["property"] != null &&
                        x.Attributes["property"].Value == "twitter:site")
                        .Select(x => x.Attributes["content"].Value).FirstOrDefault();

                    string publishDate = metas
                        .Where(x => x.Attributes["property"] != null &&
                        x.Attributes["property"].Value == "publish-date")
                        .Select(x => x.Attributes["content"].Value).FirstOrDefault();

                    author.InnerHtml = $"Author: {authorName}";

                    pubdate.InnerHtml = $"Publish Date: {publishDate}";
                }
            }
        }
    }
}