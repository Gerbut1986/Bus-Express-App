namespace BusExpress.BLL.Services
{
    using DAL.UOF;
    using System.Linq;
    using DAL.Interfaces;
    using BusExpress.BLL.Dto;
    using BusExpress.BLL.Helpers;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class BusExpressService : Interfaces.IBusExpressService
    {
        private readonly IUnitOfWork Db;

        public BusExpressService(string connectStr)
        {
            Db = new UnitOfWork(connectStr);
        }

        #region Order Info Service:
        public void Insert(OrderInfoDto dto)
        {
            var oi = FillObject.OrderInfo(dto);
            Db.OrderInfos.Create(oi);
        }

        public void Update(OrderInfoDto dto)
        {
            var oi = FillObject.OrderInfo(dto);
            Db.OrderInfos.Update(oi);
        }

        public void Delete(OrderInfoDto dto)
        {
            var oi = FillObject.OrderInfo(dto);
            Db.OrderInfos.Delete(oi);
        }

        public async Task DeleteOrderInfoAsync(int id)
        {
            await Db.OrderInfos.DeleteAsync(id);
        }

        public IEnumerable<OrderInfoDto> ReadOrderInfos()
        {
            var list = Db.OrderInfos.GetAll().ToList();
            return FillObject.OrderInfosList(list);
        }

        public async Task<IEnumerable<OrderInfoDto>> ReadOrderInfosAsync()
        {
            var list = await Db.OrderInfos.GetAllAsync();
            return FillObject.OrderInfosList(list);
        }
        #endregion

        #region Passengers Info Service:
        public void Insert(PassInfoDto dto)
        {
            var oi = FillObject.PassengerInfo(dto);
            Db.PassInfos.Create(oi);
        }

        public void Update(PassInfoDto dto)
        {
            var oi = FillObject.PassengerInfo(dto);
            Db.PassInfos.Update(oi);
        }

        public void Delete(PassInfoDto dto)
        {
            var oi = FillObject.PassengerInfo(dto);
            Db.PassInfos.Delete(oi);
        }

        public async Task DeletePassengerAsync(int id)
        {
            await Db.PassInfos.DeleteAsync(id);
        }

        public IEnumerable<PassInfoDto> ReadPassengers()
        {
            var list = Db.PassInfos.GetAll().ToList();
            return FillObject.PassInfosList(list);
        }

        public async Task<IEnumerable<PassInfoDto>> ReadPassengersAsync()
        {
            var list = await Db.PassInfos.GetAllAsync();
            return FillObject.PassInfosList(list);
        }
        #endregion

        #region Destination service:
        public void Insert(DestinationDto dto)
        {
            var oi = FillObject.Destination(dto);
            Db.Destinations.Create(oi);
        }

        public void Update(DestinationDto dto)
        {
            var oi = FillObject.Destination(dto);
            Db.Destinations.Update(oi);
        }

        public void Delete(DestinationDto dto)
        {
            var oi = FillObject.Destination(dto);
            Db.Destinations.Delete(oi);
        }

        public async Task DeleteDestinationAsync(int id)
        {
            await Db.Destinations.DeleteAsync(id);
        }

        public IEnumerable<DestinationDto> ReadDestinations()
        {
            var list = Db.Destinations.GetAll().ToList();
            return FillObject.DestinationList(list);
        }

        public async Task<IEnumerable<DestinationDto>> ReadDestinationsAsync()
        {
            var list = await Db.Destinations.GetAllAsync();
            return FillObject.DestinationList(list);
        }
        #endregion

        #region SchemaPlaces service:
        public IEnumerable<SchemaPlaceDto> ReadSchemaPlaces()
        {
            var list = Db.SchemaPlaces.GetAll().ToList();
            return FillObject.SchemaPlacesList(list);
        }
        #endregion
    }
}
