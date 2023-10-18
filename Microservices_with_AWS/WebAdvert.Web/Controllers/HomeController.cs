﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.Search;
using WebAdvert.Web.Models.ViewModel;
using WebAdvert.Web.ServiceClients.Interface;

namespace WebAdvert.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchAPIClient searchAPIClient;
        private readonly IAdvertAPIClient advertAPIClient;

        public IMapper Mapper { get; }

        public HomeController(ILogger<HomeController> logger, ISearchAPIClient searchAPIClient, IMapper mapper, IAdvertAPIClient advertAPIClient)
        {
            _logger = logger;
            this.searchAPIClient = searchAPIClient;
            Mapper = mapper;
            this.advertAPIClient = advertAPIClient;
        }

        [Authorize]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> Index()
        {
            var allAds = await advertAPIClient.GetAllAsync().ConfigureAwait(false);
            var allViewModels = allAds.Select(x => Mapper.Map<IndexViewModel>(x));

            return View(allViewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string keyword)
        {
            var viewModel = new List<SearchViewModel>();
            var searchResult = await searchAPIClient.Search(keyword).ConfigureAwait(false);

            searchResult.ForEach(advertDoc =>
            {
                var viewModelItem = Mapper.Map<SearchViewModel>(advertDoc);
                viewModel.Add(viewModelItem);
            });
            return View("Search", viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
