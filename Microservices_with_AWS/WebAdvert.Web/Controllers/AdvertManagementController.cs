using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert.Web.Infrastructure;
using WebAdvert.Web.Models;
using WebAdvert.Web.Models.AdvertManagement;
using WebAdvert.Web.ServiceClients.Interface;
using WebAdvert.Web.Services;

namespace WebAdvert.Web.Controllers
{
    public class AdvertManagementController : Controller
    {
        private readonly IFileUploader fileUploader;
        private readonly IAdvertAPIClient advertAPIClient;
        private readonly IMapper mapper;

        public AdvertManagementController(IFileUploader fileUploader, IAdvertAPIClient advertAPIClient, IMapper mapper)
        {
            this.fileUploader = fileUploader;
            this.advertAPIClient = advertAPIClient;
            this.mapper = mapper;
        }

        public IActionResult Create(CreateAdvert model)
        {
            return View(model);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateAdvert model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {

                // calls AdvertAPI, create Ad, return Id
                var createAdvertModel = mapper.Map<AdvertAPI>(model);
                var apiCallResponse = await advertAPIClient.Create(createAdvertModel);
                var id = apiCallResponse.Id;

               var fileName = "";

                if(imageFile != null)
                {
                    // use id as the FileName in the absence of imageFile.FileName
                    fileName = !string.IsNullOrEmpty(imageFile.FileName) ? Path.GetFileName(imageFile.FileName) : id;
                    var filePath = $"{id}/{ fileName}";

                    try
                    {
                        // OpenReadStream() allows for data-reading
                        using (var readStream = imageFile.OpenReadStream())
                        {
                            var result = await fileUploader.UploadFileAsync(filePath, readStream)
                                .ConfigureAwait(false);
                            if (!result)
                                throw new Exception("Upload failed. Please check the log.");
                        }

                        // call AdvertAPI and confirm the Ad
                        // for BASE implementation
                        var confimModel = new ConfirmAdvert() { 
                            Id= id,
                            FilePath = filePath,
                            Status = AdvertStatus.Active
                        };
                        var canConfirm = await advertAPIClient.Confirm(confimModel);

                        if (!canConfirm)
                        {
                            throw new Exception($"Upload failed for : {id}");
                        }

                        return RedirectToAction("Index","Home");
                    }
                    catch (Exception exc)
                    {
                        // delete eveything in case of issue
                        var confimModel = new ConfirmAdvert()
                        {
                            Id = id,
                            FilePath = filePath,
                            Status = AdvertStatus.Pending
                        };
                        await advertAPIClient.Confirm(confimModel);

                        Console.WriteLine(exc);
                    }

                }
            }
            return View();
        }
    }
}
