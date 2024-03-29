﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using SES.Data.Repo.Abstract;
using SES.Models;
using SES.Services.Abstract;
using SES.Services.Soap.ServiceReference1;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Controllers
{
    [Authorize]
    public class GetBankPinsController : Controller
    {
        private readonly ILogger<GetBankPinsController> logger;
        private readonly IGetBankPinsService service;
        private readonly ILogsRepository repo;

        public GetBankPinsController(
            ILogger<GetBankPinsController> logger,
            IGetBankPinsService service,
            ILogsRepository repo
        )
        {
            this.logger = logger;
            this.service = service;
            this.repo = repo;
        }
        public ActionResult GetInformation()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetInformation(BankPinRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid) return NotFound("error");

                var result = await service.GetPinAsync(model);


                TempData["GetBankPin"] = JsonConvert.SerializeObject(result);
            } 
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                TempData["Message"] = "Ошибка паспортных данных";
                return RedirectToAction("Index", "Errors");
            }
                
            return RedirectToAction("Results");
        }

        public IActionResult Results()
        {
            var result = JsonConvert.DeserializeObject<bankPinServiceResponse>(TempData["GetBankPin"] as string);

            if (result != null) return View(result);

            TempData["Message"] = "Error!!!";

            return RedirectToAction("Index", "Errors");
        }
    }
}
