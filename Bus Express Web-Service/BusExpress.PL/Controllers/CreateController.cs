namespace BusExpress.PL.Controllers
{
    using BLL.Dto;
    using System.Linq;
    using BLL.Services;
    using System.Web.Mvc;
    using BusExpress.PL.Models;
    using BusExpress.BLL.Interfaces;
    using System.Collections.Generic;

    public class CreateController : Controller
    {
        private static SelectList destSelectL;
        private readonly BusExpressService svc;
        private readonly IADOService pService, oService, dService;

        public CreateController()
        {
            pService = new PassInfoService();
            oService = new OrderInfoService();
            dService = new DestinationService();
            svc = new BusExpressService(Init.GetConnectStr);
            destSelectL = new SelectList(GetDestinations());
        }
        public ActionResult PassInfo()
        {
            ViewBag.Destinations = destSelectL;
            return View();
        }

        [HttpPost]
        public ActionResult PassInfo(PassInfoDto model)
        {
            if (ModelState.IsValid)
            {
               var msg = pService.Create(model, Init.GetConnectStr);
                if (msg == "..Faild..")
                    return View();
                return RedirectToAction($"../Select/{nameof(PassInfo)}");
            }
            ViewBag.Destinations = destSelectL; 
            return View();
        }

        public ActionResult OrderInfo(string fillPlace)
        { 
            if(fillPlace != null)
            {
                ViewBag.Places = new SelectList(new string[] { fillPlace });
            }
            else
            {
                ViewBag.Places = new SelectList(GetFreePlaces(svc.ReadOrderInfos().ToList()));
            }             

            return View();
        }

        [HttpPost]
        public ActionResult OrderInfo(OrderInfoDto model)
        {
            if (ModelState.IsValid)
            {
                var msg = oService.Create(model, Init.GetConnectStr);
                if (msg == "..Faild..")
                    return View();
                return RedirectToAction($"../Select/{nameof(OrderInfo)}");
            }
            ViewBag.Places = new SelectList(GetFreePlaces(svc.ReadOrderInfos().ToList()));
            return View();
        }

        public ActionResult DestinationInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DestinationInfo(FromToModel model)
        {
            if (ModelState.IsValid)
            {
                var fromTo = $"{model.From} - {model.To}";
                var destModel = new DestinationDto
                {
                    Name = fromTo
                };
                var msg = dService.Create(destModel, Init.GetConnectStr);
                if (msg == "..Faild..")
                    return View();
                return RedirectToAction($"../Select/{nameof(DestinationInfo)}");
            }
            return View();
        }

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
            return svc.ReadDestinations().Select(n => n.Name).ToArray();
        }
        #endregion
    }
}