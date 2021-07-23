using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using SES.Data.Repo.Abstract;
using SES.Models;
using SES.Services.Abstract;
using SES.Services.Soap.BankPinService;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Controllers
{
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
            if (!ModelState.IsValid) return NotFound("error");

            var result = await service.GetPinAsync(model);

            TempData["GetBankPin"] = JsonConvert.SerializeObject(result);
            logger.LogInformation($"GetBankPinService success received on {model.Pin}");

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
