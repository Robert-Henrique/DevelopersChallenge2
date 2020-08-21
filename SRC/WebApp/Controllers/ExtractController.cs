using Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NToastNotify;

namespace WebApp.Controllers
{
    public class ExtractController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IExtractManager _extractManager;
        private readonly IToastNotification _toastNotification;

        public ExtractController(IHostingEnvironment hostingEnvironment, 
            IExtractManager extractManager, 
            IToastNotification toastNotification)
        {
            _hostingEnvironment = hostingEnvironment;
            _extractManager = extractManager;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(List<IFormFile> files)
        {
            if (!ThereIsFileToImport(files))
            {
                _toastNotification.AddErrorToastMessage("Arquivo(s) não selecionado(s)");
                return RedirectToAction("Index", "Extract");
            }

            foreach (var file in files)
            {
                var fileName = "extract" + DateTime.Now.Millisecond + ".ofx";
                var pathWebRoot = _hostingEnvironment.WebRootPath;
                var pathOfxFile = pathWebRoot + "\\Extracts\\" + fileName;
                
                //copies the file to the destination location
                using (var fileStream = new FileStream(pathOfxFile, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                _extractManager.ManageExtract(pathOfxFile);
            }

            _toastNotification.AddSuccessToastMessage("Transações importadas com sucesso");
            return RedirectToAction("Index", "Transaction");
        }

        private static bool ThereIsFileToImport(ICollection files)
        {
            return files != null && files.Count > 0;
        }
    }
}