using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using SES.Data.Entities;
using SES.Data.Repo.Abstract;
using SES.Models;
using SES.Services;
using SES.Services.Abstract;
using SES.Services.Soap.ServiceReference;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Controllers
{
    [Authorize]
    public class GetWorkPeriodsController : Controller
    {
        private readonly ILogger<GetWorkPeriodsController> logger;
        private readonly IPersonalAccountInfoWithSumService service;
        private readonly ILogsRepository repo;
        // GET: GetWorkPeriodsController

        public GetWorkPeriodsController(
            ILogger<GetWorkPeriodsController> logger, 
            IPersonalAccountInfoWithSumService service,
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
        public async Task<IActionResult> GetInformation(PinRequestModel model)
        {
            if (!ModelState.IsValid) return NotFound("error");

            var result = await service.GetPersonalAccountInfoAsync(model.Pin);

            repo.AddLog(new LogEntity
            {
                Pin = model.Pin,
                Status = result.OperationResult,
                Message = result.Message,
                Method = MethodEnum.GetWorkPeriodInfo
            });

            TempData["GetWorkPeriod"] = JsonConvert.SerializeObject(result);
            logger.LogInformation(result.Message);
            return RedirectToAction("Results");
        }

        public IActionResult Results()
        {
            var result = JsonConvert.DeserializeObject<PersonalAccountInfoWithSumResponse>(TempData["GetWorkPeriod"] as string);

            if (result.OperationResult) return View(result);

            TempData["Message"] = result.Message;

            return RedirectToAction("Index", "Errors");
        }

    }
}
