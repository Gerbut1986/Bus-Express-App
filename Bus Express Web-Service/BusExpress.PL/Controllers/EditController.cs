namespace BusExpress.PL.Controllers
{
    using System;
    using BLL.Dto;
    using System.Linq;
    using System.Web.Mvc;
    using BusExpress.PL.Models;
    using System.Threading.Tasks;
    using BusExpress.BLL.Services;
    using BusExpress.BLL.Interfaces;
    using System.Collections.Generic;
    using System.Web.Services.Description;

    public class EditController : Controller
    {
        public static bool IsError = false;
        private static SelectList destSelectL;
        private readonly IBusExpressService service;
        private readonly IADOService passSvc, orderSvc, destSvc;

        public EditController()
        {
            passSvc = new PassInfoService();
            orderSvc = new OrderInfoService();
            destSvc = new DestinationService();
            service = new BusExpressService(Init.GetConnectStr);
            destSelectL = new SelectList(GetDestinations());
        }

        #region Passangers Info:
        public async Task<ActionResult> PassInfo(int? id)
        {
            if (id == null) return HttpNotFound();
            var found = await service.ReadPassengersAsync();
            ViewBag.Destinations = destSelectL;
            return View(found.FirstOrDefault(i => i.Id == id));
        }

        [HttpPost]
        public ActionResult PassInfo(PassInfoDto pi)
        {
            var resMsg = string.Empty;
            try
            {
                ViewBag.Destinations = destSelectL;
                resMsg = passSvc.Update(pi, Init.GetConnectStr);
                return RedirectToAction($"../Select/{nameof(PassInfo)}");
            }
            catch (Exception ex) { IsError = true; ViewBag.Error = $"{resMsg}\n{ex.Message}"; return View(); }
        }
        #endregion

        #region Order Info:
        public async Task<ActionResult> OrderInfo(int? id)
        {
            if (id == null) return HttpNotFound();
            var found = await service.ReadOrderInfosAsync();
            ViewBag.Places = new SelectList(GetFreePlaces(service.ReadOrderInfos().ToList()));
            return View(found.FirstOrDefault(i => i.Id == id));
        }

        [HttpPost]
        public ActionResult OrderInfo(OrderInfoDto oi)
        {
            var resMsg = string.Empty;
            try
            {
                ViewBag.Destinations = destSelectL;
                resMsg = orderSvc.Update(oi, Init.GetConnectStr);
                return RedirectToAction($"../Select/{nameof(OrderInfo)}");
            }
            catch (Exception ex) { IsError = true; ViewBag.Error = $"{resMsg}\n{ex.Message}"; return View(); }
        }
        #endregion

        #region Destinations Info:
        public async Task<ActionResult> DestinationInfo(int? id)
        {
            if (id == null) return HttpNotFound();
            var found = await service.ReadDestinationsAsync();
            var dest = found.FirstOrDefault(i => i.Id == id);
            var splt = dest.Name.Split(new char[] { '-' });
            return View(new FromToModel { Id = (int)id, From = splt[0], To = splt[1] });
        }

        [HttpPost]
        public ActionResult DestinationInfo(FromToModel model)
        {
            var resMsg = string.Empty;
            try
            {
                resMsg = destSvc.Update(new DestinationDto { Id = model.Id, Name = $"{model.From} - {model.To}" }, Init.GetConnectStr);
                return RedirectToAction($"../Select/{nameof(DestinationInfo)}");
            }
            catch (Exception ex) { IsError = true; ViewBag.Error = $"{resMsg}\n{ex.Message}"; return View(); }
        }
        #endregion

        #region Auxiliary methods:
        private List<int> GetFreePlaces(List<OrderInfoDto> orders)
        {
            bool isAlready = false;
            var list = new List<int>();
            for (var i = 1; i <= 55; i++)
            {
                for (var l = 0; l < orders.Count; l++)
                    if (orders[l].PlaceNumber == i)
                        isAlready = true;
                if (!isAlready) list.Add(i);
                else isAlready = false;
            }
            return list;
        }

        public string[] GetDestinations()
        {
            return service.ReadDestinations().Select(n => n.Name).ToArray();
        }
        #endregion
    }
}