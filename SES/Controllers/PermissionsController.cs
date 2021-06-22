using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SES.Models;
using SES.Services;

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
        public PermissionsController(ILogger<PermissionsController> logger)
        {
            this.logger = logger;
        }
        private readonly PermissionsService service = new PermissionsService();

        public IActionResult SendPermissionRequest()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendPermissionRequest(RequestPermissionModel model)
        {
            if (!ModelState.IsValid) return NotFound("Ошибка заполнения формы");

            var result = await service.SendRequestForPermission(model);

            if (result.OperationResult == false)
            {
                TempData["Message"] = result.Message;
                logger.LogError(result.Message);
                return RedirectToAction(nameof(ShowInfo));
            }
                
            return RedirectToAction(nameof(SendCodeForPermission), new { requestId = result.RequestId.ToString() }); 
        }

        public IActionResult SendCodeForPermission(string requestId)
        {
            ViewData["requestId"] = requestId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendCodeForPermission(CodeForPermissionModel model)
        {
            if (!ModelState.IsValid) return NotFound("Ошибка заполнения формы");

            var result = await service.SendCodeForPermission(model);

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
