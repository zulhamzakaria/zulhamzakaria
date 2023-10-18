using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.SearchAPI.Messages;
using WebAdvert.SearchAPI.Services;

namespace WebAdvert.SearchAPI.Controllers
{
    [Route("search/v1")]
    [ApiController]
    [Produces("application/json")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService;
        private readonly ILogger<SearchController> logger;

        public SearchController(ISearchService searchService, ILogger<SearchController> logger)
        {
            this.searchService = searchService;
            this.logger = logger;
        }

        [HttpGet]
        // /search/v1/[keyword]
        [Route("{keyword}")]
        public async Task<List<AdvertType>> Get(string keyword)
        {
            logger.LogInformation("Search Method was called");
            return await searchService.Search(keyword);
        }
    }
}
