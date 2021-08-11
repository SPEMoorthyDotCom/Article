using Markdig;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPEMoorthy.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly IHttpClientFactory _clientFactory;
        public ArticleController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        // GET: api/<ArticleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ArticleController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            // Pull markdown file from the specified url
            //HttpClient client = _clientFactory.CreateClient();
            //string strmd = await client.GetStringAsync("https://raw.githubusercontent.com/hey-red/Markdown/master/README.md");

            string strMarkdown = "Welcome";
            Dictionary<int, String> myArticles = new Dictionary<int, string>();
            myArticles.Add(1, "001_SPEMoorthy.md");
            myArticles.Add(2, "002_Startup_Onboarding_Attrition.md");
            myArticles.Add(3, "003_Fix_SQL_Database_CRC_Error.md");
            myArticles.Add(4, "004_Mini_Project_GST_Invoicing.md");


            var gitHubClient = new GitHubClient(new ProductHeaderValue("SPEMoorthy"));
            var tokenAuth = new Credentials("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");
            gitHubClient.Credentials = tokenAuth;

            string articleName = "";

            if (myArticles.TryGetValue(id, out articleName))
            {
                try
                {
                    byte[] articleContent = await gitHubClient.Repository.Content.GetRawContent("SPEMoorthyDotCom", "ArticleContent", articleName);
                    strMarkdown = Encoding.Default.GetString(articleContent);
                }
                catch (Exception e)
                {
                    strMarkdown = e.Message;
                }
            }
            else
            {
                strMarkdown = "The requested article with id " + id + "is not found!";
            }


            // Configure the pipeline with all advanced extensions active
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var result = Markdown.ToHtml(strMarkdown, pipeline);

            return result;
        }

        // POST api/<ArticleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
