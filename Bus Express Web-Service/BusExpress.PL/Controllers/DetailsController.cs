namespace BusExpress.PL.Controllers
{
    using BusExpress.BLL.Interfaces;
    using BusExpress.BLL.Services;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Linq;

    public class DetailsController : Controller
    {
        private readonly IBusExpressService service;

        public DetailsController()
        {
            service = new BusExpressService(Models.Init.GetConnectStr);
        }

        public async Task<ActionResult> PassInfo(int? id)
        {
            if (id == null) return HttpNotFound();
            var found = await service.ReadPassengersAsync();
            return View(found.FirstOrDefault(i => i.Id == id));
        }

        public async Task<ActionResult> OrderInfo(int? id)
        {
            if (id == null) return HttpNotFound();
            var found = await service.ReadOrderInfosAsync();
            return View(found.FirstOrDefault(i => i.Id == id));
        }
    }
}