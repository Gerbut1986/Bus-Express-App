namespace BusExpress.PL.Controllers
{
    using System;
    using System.Web.Mvc;
    using BusExpress.BLL.Dto;
    using BusExpress.BLL.Services;
    using BusExpress.BLL.Interfaces;
    using System.Collections.Generic;

    public class HomeController : Controller
    {
        #region Fields:
        //PassInfoDto selPassInfo;
        //OrderInfoDto selOrderInfo;
        //DestinationDto selDestination;
        //static bool notSelected = true; 
        //const string _Title = "BUSS EXPRESS";
        //private readonly BusExpressService db;
        //public static string selectedPlace = null;
        //List<string> SchemaPlaceProps { get; set; }
        //public static string SelectDest { get; set; }
        //public static DateTime SelectDate { get; set; }
        //static IEnumerable<PassInfoDto> PassInfos { get; set; }
        //static IEnumerable<SchemaPlaceDto> SchemaPlaces { get; set; }
        //public static IEnumerable<OrderInfoDto> OrderInfos { get; set; }
        //public static IEnumerable<DestinationDto> Destinations { get; set; }
        //private readonly IADOService pService, oService, sService, dService;
        #endregion

        #region Constructor:
        public HomeController()
        {
            //pService = new PassInfoService();
            //oService = new OrderInfoService();
            //sService = new SchemaPlaceService();
            //dService = new DestinationService();
        }
        #endregion

        public ActionResult Index(string topClick)
        {

            return View();
        }

        #region Auxiliary methods:
        //private string GetPartView(string type)
        //{
        //    switch (type)
        //    {
        //        case ""
        //    }
        //}
        #endregion
    }
}