namespace BusExpress.PL.Controllers
{
    using BusExpress.BLL.Interfaces;
    using BusExpress.BLL.Services;
    using BusExpress.BLL.Dto;
    using System.Web.Mvc;
    using System.Linq;

    public class DeleteController : Controller
    {
        private readonly IBusExpressService service;
        private readonly IADOService passSvc, orderSvc, destSvc;

        public DeleteController()
        {
            passSvc = new PassInfoService();
            orderSvc = new OrderInfoService();
            destSvc = new DestinationService();
            service = new BusExpressService(Models.Init.GetConnectStr);
        }

        #region Passangers Info:
        public async System.Threading.Tasks.Task<ActionResult> PassInfo(int? id)
        {
            if (id == null) return HttpNotFound();
            var found = await service.ReadPassengersAsync();
            return View(found.FirstOrDefault(i => i.Id == id));
        }

        [HttpPost]
        public ActionResult PassInfo(PassInfoDto pi)
        {
            try
            {
                passSvc.Delete(pi.Id, Models.Init.GetConnectStr);
                return RedirectToAction($"../Select/{nameof(PassInfo)}");
            }
            catch { return View(); }
        }
        #endregion

        #region Order Info:
        public async System.Threading.Tasks.Task<ActionResult> OrderInfo(int? id)
        {
            if (id == null) return HttpNotFound();
            var found = await service.ReadOrderInfosAsync();
            return View(found.FirstOrDefault(i => i.Id == id));
        }

        [HttpPost]
        public ActionResult OrderInfo(OrderInfoDto oi)
        {
            try
            {
                orderSvc.Delete(oi.Id, Models.Init.GetConnectStr);
                return RedirectToAction($"../Select/{nameof(OrderInfo)}");
            }
            catch { return View(); }
        }
        #endregion

        #region Destinations Info
        public async System.Threading.Tasks.Task<ActionResult> DestinationInfo(int? id)
        {
            if (id == null) return HttpNotFound();
            var found = await service.ReadDestinationsAsync();
            return View(found.FirstOrDefault(i => i.Id == id));
        }

        [HttpPost]
        public ActionResult DestinationInfo(DestinationDto di)
        {
            try
            {
                destSvc.Delete(di.Id, Models.Init.GetConnectStr);
                return RedirectToAction($"../Select/{nameof(DestinationInfo)}");
            }
            catch { return View(); }
        }
        #endregion
    }
}