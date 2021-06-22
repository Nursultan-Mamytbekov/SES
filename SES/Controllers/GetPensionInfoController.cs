using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

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
    public class GetPensionInfoController : Controller
    {


        private readonly ILogger<GetPensionInfoController> logger;
        private GetPensionInfoWithSumService service = new GetPensionInfoWithSumService();
        // GET: GetWorkPeriodsController

        public GetPensionInfoController(ILogger<GetPensionInfoController> logger)
        {
            this.logger = logger;
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

            var result = await service.GetPensionInfoWithSumAsync(model.Pin);

            TempData["GetPension"] = JsonConvert.SerializeObject(result);

            logger.LogInformation(result.Message);
            return RedirectToAction("Results");
        }

        public IActionResult Results()
        {
            var result = JsonConvert.DeserializeObject<PensionInfoWithSumResponse>(TempData["GetPension"] as string);

            if (result.OperationResult) return View(result);

            TempData["Message"] = result.Message;

            return RedirectToAction("Index", "Errors");
        }
    }
}
