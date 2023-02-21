namespace BusExpress.PL.Controllers
{
    using System.Collections.Generic;
    using BusExpress.BLL.Interfaces;
    using BusExpress.BLL.Services;
    using System.Threading.Tasks;
    using BusExpress.BLL.Dto;
    using System.Web.Mvc;
    using OfficeOpenXml;
    using System.Linq;
    using System.Web;
    using System.IO;
    using System;

    public enum MsgType
    {
        Empty,
        Error,
        BusyPls,
        Success
    }

    public class SelectController : Controller
    {
        private readonly IBusExpressService service;
        private static bool IsEmpty { get; set; } = false;
        private static bool IsImport { get; set; } = false;
        public static bool IsBusyPlace { get; set; } = false;
        public static IEnumerable<IModel> FromEXCLToList { get; set; } = default;

        public SelectController()
        {
            service = new BusExpressService(Models.Init.GetConnectStr);
            if (FromEXCLToList != default) ImportExecute();
        }

        #region Passangers Info:
        public ActionResult PassInfo()
        {
            var lst = IsImport ? FromEXCLToList : service.ReadPassengers().ToList();
            if (!lst.Any())
            {
                IsEmpty = true;
                ViewBag.EmptyMsg = GetMessage(MsgType.Empty);
            }
            ViewBag.IsEmpty = IsEmpty;
            return View(lst);
        }

        [HttpPost]
        public ActionResult PassInfoView()
        {
            return View();
        }       
        #endregion

        #region Order Info:
        public ActionResult OrderInfo()
        {
            var lst = IsImport ? FromEXCLToList : service.ReadOrderInfos().ToList();
            if (!lst.Any())
            {
                IsEmpty = true;
                ViewBag.EmptyMsg = GetMessage(MsgType.Empty);
            }
            ViewBag.IsEmpty = IsEmpty;
            return View(lst);
        }

        [HttpPost]
        public ActionResult OrderInfo(OrderInfoDto oi)
        {
            return View();
        }
        #endregion

        #region Schema Places Info:
        public ActionResult SchemaPlacesInfo()
        {
            var spi = service.ReadSchemaPlaces().ToList();
            return View(spi[0]);
        }

        [HttpPost]
        public ActionResult SchemaPlacesInfo(SchemaPlaceDto o)
        {
            var button =
               Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First().Remove(0, 1);
            if (button == "busy") 
            { IsBusyPlace = true; ViewBag.BusyMsg = GetMessage(MsgType.BusyPls); }
            else
            {
                return RedirectToAction("OrderInfo", "Create", new { fillPlace = button });
                    //RedirectToAction($"../Create/OrderInfo/fillPlace={button}");
            }
            var spi = service.ReadSchemaPlaces().ToList();
            return View(spi[0]);
        }
        #endregion

        #region Destinations Info
        public ActionResult DestinationInfo()
        {
            var lst = IsImport ? FromEXCLToList : service.ReadDestinations().ToList();
            if (!lst.Any())
            {
                IsEmpty = true;
                ViewBag.EmptyMsg = GetMessage(MsgType.Empty);
            }
            ViewBag.IsEmpty = IsEmpty;
            return View(lst);
        }

        [HttpPost]
        public ActionResult DestinationInfo(DestinationDto di)
        {
            return View();
        }
        #endregion

        public ActionResult CloseAlerts()
        {
            IsBusyPlace = false;
            return RedirectToAction("../Select/SchemaPlacesInfo");
        }

        private string GetMessage(MsgType type)
        {
            switch (type)
            {
                case MsgType.Empty:
                    return "The table list is empty... Try to fill it in!";
                case MsgType.Success:
                    return "";
                case MsgType.Error:
                    return "";
                case MsgType.BusyPls:
                    return "This place are Busy...";
                default:
                    return "NaN";
            }
        }

        private ActionResult ImportExecute()
        {
            IsImport = true;
            var entityType = FromEXCLToList.GetType().GetGenericArguments()[0];
            switch (entityType.Name)
            {
                case nameof(PassInfoDto):
                    return RedirectToAction("PassInfo");
                case nameof(OrderInfoDto):
                    return RedirectToAction("OrderInfo");
                case nameof(DestinationDto):
                    return RedirectToAction("DestinationInfo");
                default: return RedirectToAction("../Shared/Error");
            }
        }
    }
}