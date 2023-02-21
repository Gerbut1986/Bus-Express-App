namespace BusExpress.BLL.Interfaces
{
    using BusExpress.BLL.Dto;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IBusExpressService
    {
        #region Order Info:
        void Insert(OrderInfoDto model);
        void Update(OrderInfoDto model);
        void Delete(OrderInfoDto model);
        Task DeleteOrderInfoAsync(int id);
        IEnumerable<OrderInfoDto> ReadOrderInfos();
        Task<IEnumerable<OrderInfoDto>> ReadOrderInfosAsync();
        #endregion
        #region Passangers Info:
        void Insert(PassInfoDto model);
        void Update(PassInfoDto model);
        void Delete(PassInfoDto model);
        Task DeletePassengerAsync(int id);
        IEnumerable<PassInfoDto> ReadPassengers();
        Task<IEnumerable<PassInfoDto>> ReadPassengersAsync();
        #endregion
        #region Destination Info:
        void Insert(DestinationDto model);
        void Update(DestinationDto model);
        void Delete(DestinationDto model);
        Task DeleteDestinationAsync(int id);
        IEnumerable<DestinationDto> ReadDestinations();
        Task<IEnumerable<DestinationDto>> ReadDestinationsAsync();
        #endregion
        #region Schema Places:
        //void Insert(SchemaPlaceDto model);
        //void Update(SchemaPlaceDto model);
        //void Delete(SchemaPlaceDto model);
        //Task DeleteSchemaPlaceAsync(int id);
        IEnumerable<SchemaPlaceDto> ReadSchemaPlaces();
        //Task<IEnumerable<SchemaPlaceDto>> ReadSchemaPlacesAsync();
        #endregion
    }
}
