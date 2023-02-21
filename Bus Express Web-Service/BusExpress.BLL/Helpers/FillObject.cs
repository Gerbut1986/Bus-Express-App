namespace BusExpress.BLL.Helpers
{
    using Dto;
    using DAL.Entities;
    using System.Collections.Generic;

    public class FillObject
    {
        #region OrderInfo fill:
        public static IEnumerable<OrderInfoDto> OrderInfosList(List<OrderInfo> list)
        {
            var dto = new List<OrderInfoDto>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(new OrderInfoDto());
                dto[i].Id = list[i].Id;
                dto[i].From = list[i].From;
                dto[i].To = list[i].To;
                dto[i].PlaceNumber = list[i].PlaceNumber;
                dto[i].OrderNumber = list[i].OrderNumber;
                dto[i].MoneyAmount = list[i].MoneyAmount;
                dto[i].IsOrdered = list[i].IsOrdered;
                dto[i].Phone = list[i].Phone;
            }
            return dto;
        }

        public static OrderInfo OrderInfo(OrderInfoDto dto)
        {
            if (dto != null)
                return new OrderInfo
                {
                    Id = dto.Id,
                    From = dto.From,
                    To = dto.To,
                    LName_FName = dto.LName_FName,
                    MoneyAmount = dto.MoneyAmount,
                    OrderNumber = dto.OrderNumber,
                    Phone = dto.Phone,
                    IsOrdered = dto.IsOrdered,
                    PlaceNumber = dto.PlaceNumber
                };
            else return null;
        }
        #endregion

        #region PassInfo fill:
        public static IEnumerable<PassInfoDto> PassInfosList(List<PassInfo> list)
        {
            var dto = new List<PassInfoDto>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(new PassInfoDto());
                dto[i].Id = list[i].Id;
                dto[i].Booking_Date = list[i].Booking_Date;
                dto[i].Booking_Route = list[i].Booking_Route;
                dto[i].C_FName = list[i].C_FName;
                dto[i].C_LName = list[i].C_LName;
                dto[i].C_Email = list[i].C_Email;
                dto[i].C_Phone = list[i].C_Phone;
                dto[i].C_Notes = list[i].C_Notes;
                dto[i].Payment_Method = list[i].Payment_Method;
                dto[i].Qty = list[i].Qty;
                dto[i].Tax = list[i].Tax;
                dto[i].Total = list[i].Total;
            }
            return dto;
        }

        public static PassInfo PassengerInfo(PassInfoDto dto)
        {
            if (dto != null)
                return new PassInfo
                {
                    Id = dto.Id,
                    Booking_Date = dto.Booking_Date,
                    Booking_Route = dto.Booking_Route,
                    C_FName = dto.C_FName,
                    C_LName = dto.C_LName,
                    C_Email = dto.C_Email,
                    C_Phone = dto.C_Phone,
                    C_Notes = dto.C_Notes,
                    Payment_Method = dto.Payment_Method,
                    Qty = dto.Qty,
                    Tax = dto.Tax,
                    Total = dto.Total
                };
            else return null;
        }
        #endregion

        #region Destination Info fill:
        public static IEnumerable<DestinationDto> DestinationList(List<Destination> list)
        {
            var dto = new List<DestinationDto>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(new DestinationDto());
                dto[i].Id = list[i].Id;
                dto[i].Name = list[i].Name;
            }
            return dto;
        }

        public static Destination Destination(DestinationDto dto)
        {
            if (dto != null)
                return new Destination
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
            else return null;
        }
        #endregion

        #region SchemaPlaces fill:
        public static OrderInfo SchemaInfoInfo(SchemaPlaceDto dto)
        {
            return null;
        }
        public static IEnumerable<SchemaPlaceDto> SchemaPlacesList(List<SchemaPlace> list)
        {
            var dto = new List<SchemaPlaceDto>();
            for (int i = 0; i < list.Count; i++)
            {
                dto.Add(new SchemaPlaceDto());
                dto[i].Id = list[i].Id;
                dto[i].GoDate = list[i].GoDate;
                dto[i].BusNameNumber = list[i].BusNameNumber;
                dto[i].Is1Place = list[i].Is2Place;
                dto[i].Is2Place = list[i].Is2Place;
                dto[i].Is3Place = list[i].Is3Place;
                dto[i].Is4Place = list[i].Is4Place;
                dto[i].Is5Place = list[i].Is5Place;
                dto[i].Is6Place = list[i].Is6Place;
                dto[i].Is7Place = list[i].Is7Place;
                dto[i].Is8Place = list[i].Is8Place;
                dto[i].Is9Place = list[i].Is9Place;
                dto[i].Is10Place = list[i].Is10Place;
                dto[i].Is11Place = list[i].Is11Place;
                dto[i].Is12Place = list[i].Is12Place;
                dto[i].Is13Place = list[i].Is13Place;
                dto[i].Is14Place = list[i].Is14Place;
                dto[i].Is15Place = list[i].Is15Place;
                dto[i].Is16Place = list[i].Is16Place;
                dto[i].Is17Place = list[i].Is17Place;
                dto[i].Is18Place = list[i].Is18Place;
                dto[i].Is19Place = list[i].Is19Place;
                dto[i].Is20Place = list[i].Is20Place;
                dto[i].Is21Place = list[i].Is21Place;
                dto[i].Is22Place = list[i].Is22Place;
                dto[i].Is23Place = list[i].Is23Place;
                dto[i].Is24Place = list[i].Is24Place;
                dto[i].Is25Place = list[i].Is25Place;
                dto[i].Is26Place = list[i].Is26Place;
                dto[i].Is27Place = list[i].Is27Place;
                dto[i].Is28Place = list[i].Is28Place;
                dto[i].Is29Place = list[i].Is29Place;
                dto[i].Is30Place = list[i].Is30Place;
                dto[i].Is31Place = list[i].Is31Place;
                dto[i].Is32Place = list[i].Is32Place;
                dto[i].Is33Place = list[i].Is33Place;
                dto[i].Is34Place = list[i].Is34Place;
                dto[i].Is35Place = list[i].Is35Place;
                dto[i].Is36Place = list[i].Is36Place;
                dto[i].Is37Place = list[i].Is37Place;
                dto[i].Is38Place = list[i].Is38Place;
                dto[i].Is39Place = list[i].Is39Place;
                dto[i].Is40Place = list[i].Is40Place;
                dto[i].Is41Place = list[i].Is41Place;
                dto[i].Is42Place = list[i].Is42Place;
                dto[i].Is43Place = list[i].Is43Place;
                dto[i].Is44Place = list[i].Is44Place;
                dto[i].Is45Place = list[i].Is45Place;
                dto[i].Is46Place = list[i].Is46Place;
                dto[i].Is47Place = list[i].Is47Place;
                dto[i].Is48Place = list[i].Is48Place;
                dto[i].Is49Place = list[i].Is49Place;
                dto[i].Is49Place = list[i].Is49Place;
                dto[i].Is50Place = list[i].Is50Place;
                dto[i].Is51Place = list[i].Is51Place;
                dto[i].Is52Place = list[i].Is52Place;
                dto[i].Is53Place = list[i].Is53Place;
                dto[i].Is54Place = list[i].Is54Place;
                dto[i].Is55Place = list[i].Is55Place;
            }
            return dto;
        }
        #endregion
    }
}
