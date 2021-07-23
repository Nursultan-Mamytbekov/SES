using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using SES.Data.Entities;
using SES.Data.Repo.Abstract;
using SES.Models;
using SES.Services;
using SES.Services.Abstract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SES.Controllers
{
    [Authorize]
    public class PermissionsController : Controller
    {
        private readonly ILogger<PermissionsController> logger;
        private readonly IPermissionsService service;
        private readonly ILogsRepository repo;
        private readonly IConfiguration configuration;
        
        public PermissionsController(
            ILogger<PermissionsController> logger,
            IPermissionsService service,
            ILogsRepository repo,
            IConfiguration configuration
        )
        {
            this.logger = logger;
            this.service = service;
            this.repo = repo;
            this.configuration = configuration;
        }

        public IActionResult SendPermissionRequest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendPermissionRequest(RequestPermissionModel model)
        {
            if (!ModelState.IsValid) return NotFound("Ошибка заполнения формы");

            model.OrganizationId = configuration["ClientOptions:UserId"];
            var result = await service.SendRequestForPermission(model);

            repo.AddLog(new LogEntity
            {
                Pin = model.Pin,
                Status = result.OperationResult,
                Message = result.Message,
                Method = MethodEnum.InitialyzeRequestForPermission    
            });

            if (result.OperationResult == false)
            {
                TempData["Message"] = result.Message;
                logger.LogInformation(result.Message);
                return RedirectToAction(nameof(ShowInfo));
            }
                
            return RedirectToAction(nameof(SendCodeForPermission), new { requestId = result.RequestId.ToString(), pin = model.Pin }); 
        }

        public IActionResult SendCodeForPermission(string requestId, string pin)
        {
            ViewData["RequestId"] = requestId;
            ViewData["Pin"] = pin;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCodeForPermission(CodeForPermissionModel model)
        {
            if (!ModelState.IsValid) return NotFound("Ошибка заполнения формы");

            var result = await service.SendCodeForPermission(model);

            repo.AddLog(new LogEntity
            {
                Pin = model.Pin,
                Status = result.OperationResult,
                Message = result.Message,
                Method = MethodEnum.SendCodeForPermission
            });

            if (result.OperationResult == false)
            {
                TempData["Message"] = result.Message;
                logger.LogError(result.Message);
                return RedirectToAction(nameof(ShowInfo));
            }

            TempData["Message"] = result.Message;

            return RedirectToAction(nameof(ShowInfo));
        }

        public IActionResult ShowInfo()
        {
            return View();
        }
    }
}
