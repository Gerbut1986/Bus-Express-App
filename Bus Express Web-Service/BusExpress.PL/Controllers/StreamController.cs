namespace BusExpress.PL.Controllers
{
    using Models;
    using System;
    using System.IO;
    using System.Web;
    using System.Linq;
    using OfficeOpenXml;
    using System.Web.Mvc;
    using BusExpress.BLL.Dto;
    using BusExpress.BLL.Interfaces;
    using System.Web.UI.WebControls;
    using System.Collections.Generic;

    public class StreamController : Controller
    {
        private string model;
        private readonly ExcelLogic exl; 
        private string Path { get; set; }

        public StreamController()
        {
            exl = new ExcelLogic();
        }

        [HttpGet]
        public ActionResult Import(string model)
        {
            this.model = model;
            ViewBag.Entity = GetEntityName(model);
            return View();
        }

        [HttpPost]
        public ActionResult Import()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            HttpPostedFileBase file = Request.Files[0];
            UploadToMainDir(file);
            using (ExcelPackage package = new ExcelPackage(new FileInfo(Path)))
            {
                var sheet = package.Workbook.Worksheets.FirstOrDefault();
                SelectController.FromEXCLToList = GetList(sheet, model);
            }
            return RedirectToAction($"../Select/PassInfo");
        }


        [HttpGet]
        public ActionResult Uploded(string view)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Uploded()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        Path = System.IO.Path.Combine(Server.MapPath($"~/Uploaded_Files/{fname}"));
                        file.SaveAs(Path);
                    }
                    // Returns message that successfully uploaded  
                    return Json("Your file was uploaded successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("There was no file selected. Please try again.");
            }
        }

        #region Show All Files & Delete selected file:
        public ActionResult AllFiles()
        {
            var names = new List<string>();
            Path = Server.MapPath("~/Uploaded_Files");
            string[] files_names = Directory.GetFiles(Path, "*", SearchOption.AllDirectories);
            for (int i = 0; i < files_names.Length; i++)
                names.Add(System.IO.Path.GetFileName(files_names[i]));

            return View(names);
        }
        #endregion

        #region Delete file:
        public ActionResult DeleteFile(string item)
        {
            var flag = false;
            if (item != null)
            {
                flag = true;
                var path = System.IO.Path.Combine(Server.MapPath($"~/Uploaded_Files/{item}"));
                System.IO.File.Delete(path);
            }
            if (!flag) return Json("There is nothing to delete...Please upload a file first.");
            else return RedirectToAction("../Stream/AllFiles");
        }
        #endregion

        private void UploadToMainDir(HttpPostedFileBase file)
        {
            string fname;
            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
            {
                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                fname = testfiles[testfiles.Length - 1];
            }
            else fname = file.FileName;
            Path = System.IO.Path.Combine(Server.MapPath($"~/Uploaded_Files/{fname}"));
            file.SaveAs(Path);
        }

        #region Auxiliary Methods:
        private IEnumerable<IModel> GetList(ExcelWorksheet sheet, string model)
        {
            switch (model)
            {
                case nameof(PassInfoDto):
                    return exl.GetList<PassInfoDto>(sheet).ToList();
                case nameof(OrderInfoDto):
                    return exl.GetList<OrderInfoDto>(sheet).ToList();
                case nameof(DestinationDto):
                    return exl.GetList<DestinationDto>(sheet).ToList();
                default: return new List<IModel>();
            }
        }

        private string GetEntityName(string model)
        {
            switch (model)
            {
                case nameof(PassInfoDto):
                    return "Passenger Info";
                case nameof(OrderInfoDto):
                    return "Order Info";
                case nameof(DestinationDto):
                    return "Destination Info";
                default: return string.Empty;
            }
        }
        #endregion
    }
}